using System.Collections.Generic;
using Streamish.Models;

namespace Streamish.Repositories
{
    public interface IUserProfileRepository
    {
        List<UserProfile> GetAll();
        UserProfile Get(int id);
        UserProfile GetWithVideos(int id);
        void Update(UserProfile profile);
        void Delete(int id);
        void Add(UserProfile profile);
    }
}
