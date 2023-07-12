using Microsoft.AspNetCore.Mvc;
using Streamish.Repositories;
using Streamish.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Streamish.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;
        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }
        // GET: api/<UserProfileController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userProfileRepository.GetAll());
        }

        // GET api/<UserProfileController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var video = _userProfileRepository.Get(id);
            if(video == null)
            {
                return NotFound();
            }
            return Ok(video);
        }

        [HttpGet("GetUserProfileWithVideos{id}")]
        public IActionResult GetWithVideos(int id)
        {
            var video = _userProfileRepository.GetWithVideos(id);
            if (video == null)
            {
                return NotFound();
            }
            return Ok(video);
        }

        // POST api/<UserProfileController>
        [HttpPost]
        public IActionResult Post(UserProfile profile)
        {
            _userProfileRepository.Add(profile);
            return CreatedAtAction("Get", new { id = profile.Id }, profile);
        }

        // PUT api/<UserProfileController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, UserProfile profile)
        {
            if (id != profile.Id)
            {
                return BadRequest();
            }
            else
            {
                _userProfileRepository.Update(profile);
                return NoContent();
            }
        }

        // DELETE api/<UserProfileController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userProfileRepository.Delete(id);
            return NoContent();
        }
    }
}
