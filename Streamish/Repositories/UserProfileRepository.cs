using Microsoft.Extensions.Configuration;
using Streamish.Models;
using Streamish.Utils;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;

namespace Streamish.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public void Add(UserProfile profile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO UserProfile ([Name], Email, ImageUrl, DateCreated)
                    OUTPUT INSERTED.Id
                    VALUES (@name, @email, @imageUrl, @dateCreated)";
                    DbUtils.AddParameter(cmd, "@name", profile.Name);
                    DbUtils.AddParameter(cmd, "@email", profile.Email);
                    DbUtils.AddParameter(cmd, "@imageUrl", profile.ImageUrl);
                    DbUtils.AddParameter(cmd, "@dateCreated", profile.DateCreated);

                    profile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM UserProfile WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public UserProfile Get(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT Id, [Name], Email, ImageUrl, DateCreated
                    FROM UserProfile
                    WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    using(var reader = cmd.ExecuteReader())
                    {
                        var profile = new UserProfile();
                        if (reader.Read())
                        {
                            profile.Id = id;
                            profile.Name = DbUtils.GetString(reader, "Name");
                            profile.Email = DbUtils.GetString(reader, "Email");
                            profile.ImageUrl = DbUtils.GetString(reader, "ImageUrl");
                            profile.DateCreated = DbUtils.GetDateTime(reader, "DateCreated");
                        }
                        return profile;
                    }
                }
            }
        }

        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT Id, [Name], Email, ImageUrl, DateCreated
                    FROM UserProfile";
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<UserProfile> profiles = new List<UserProfile>();
                        while (reader.Read())
                        {
                            var profile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated")
                            };
                            profiles.Add(profile);
                        }
                        return profiles;
                    }
                }
            }
        }

        public UserProfile GetWithVideos(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT u.Id as UserId, u.[Name], u.Email, u.ImageUrl as UserUrl, u.DateCreated as UserDateCreated,
                    v.Id as VideoId, v.Title, v.Description, v.Url as VideoUrl, v.DateCreated as VideoDateCreated
                    FROM UserProfile u
                    LEFT JOIN Video v ON v.UserProfileId = u.Id
                    WHERE u.Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    using(var reader = cmd.ExecuteReader())
                    {
                        var profile = new UserProfile();
                        while (reader.Read())
                        {
                            if(profile.Id != DbUtils.GetInt(reader, "UserId"))
                            {
                                profile.Id = DbUtils.GetInt(reader, "UserId");
                                profile.Name = DbUtils.GetString(reader, "Name");
                                profile.Email = DbUtils.GetString(reader, "Email");
                                profile.ImageUrl = DbUtils.GetString(reader, "UserUrl");
                                profile.DateCreated = DbUtils.GetDateTime(reader, "UserDateCreated");
                                profile.Videos = new List<Video>();
                                profile.Videos.Add(new Video() {
                                    Id = DbUtils.GetInt(reader, "VideoId"),
                                    Title = DbUtils.GetString(reader, "Title"),
                                    Description = DbUtils.GetString(reader, "Description"),
                                    Url = DbUtils.GetString(reader, "VideoUrl"),
                                    DateCreated = DbUtils.GetDateTime(reader, "VideoDateCreated")
                                });
                            }
                            else
                            {
                                profile.Videos.Add(new Video()
                                {
                                    Id = DbUtils.GetInt(reader, "VideoId"),
                                    Title = DbUtils.GetString(reader, "Title"),
                                    Description = DbUtils.GetString(reader, "Description"),
                                    Url = DbUtils.GetString(reader, "VideoUrl"),
                                    DateCreated = DbUtils.GetDateTime(reader, "VideoDateCreated")
                                });
                            }
                        }
                        return profile;
                    }
                }
            }
        }

        public void Update(UserProfile profile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    UPDATE UserProfile
                    SET [Name] = @name, Email = @email, ImageUrl = @imageUrl
                    WHERE UserProfile.Id = @id";
                    DbUtils.AddParameter(cmd, "@name", profile.Name);
                    DbUtils.AddParameter(cmd, "@email", profile.Email);
                    DbUtils.AddParameter(cmd, "@imageUrl", profile.ImageUrl);
                    DbUtils.AddParameter(cmd, "@id", profile.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
