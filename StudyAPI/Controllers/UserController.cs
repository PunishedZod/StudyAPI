using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudyAPI.Models;
using StudyAPI.Models.Interfaces;
using StudyAPI.Services;

namespace StudyAPI.Controllers
{
    //Controller for the User table in the database
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Service _service;

        public UserController(Service service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new ObjectResult(await _service.Get<User>("User"));
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _service.Get<User>(id, "User");

            if (user == null)
            {
                return NotFound();
            }

            return new ObjectResult(user);
        }

        [HttpGet("Username={Uname}&Password={Pword}")]
        public async Task<IActionResult> Get(string uname, string pword)
        {
            var user = await _service.GetLogin<User>(uname, pword, "User");

            if (user == null)
            {
                return NotFound();
            }

            return new ObjectResult(user);
        }

        [HttpPost]
        public async Task<ActionResult> Insert(User user)
        {
           await _service.Insert("User", user);

            return CreatedAtRoute("PostUser", new { id = user.Id.ToString() }, user);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, User user)
        {
            var userDB = await _service.Get<User>(id, "User");

            if (userDB == null)
            {
                return NotFound();
            }

            user.Id = userDB.Id;

            await _service.Update(user.Id, user, "User");

            return new OkObjectResult(user);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userDB = await _service.Get<User>(id, "User");

            if (userDB == null)
                return new NotFoundResult();

            await _service.Delete<User>(id, "User");

            return new OkResult();
        }
    }
}