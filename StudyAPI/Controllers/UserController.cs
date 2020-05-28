using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using StudyAPI.Services;
using StudyAPI.Models;

namespace StudyAPI.Controllers
{
    //Controller for the User table in the database
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Service _service;
        private readonly string collection = "User";

        public UserController(Service service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return Ok(await _service.Get<User>(collection));
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _service.Get<User>(id, collection);

            if (user == null)
            {
                return NotFound();
            }

            return new ObjectResult(user);
        }

        [HttpGet("Username={Uname}&Password={Pword}")]
        public async Task<IActionResult> Get(string uname, string pword)
        {
            var user = await _service.GetLogin<User>(uname, pword, collection);

            if (user == null)
            {
                return NotFound();
            }

            return new ObjectResult(user);
        }

        [HttpPost]
        public async Task Insert(User user)
        {
            await _service.Insert(collection, user);
        }

        [HttpPut]
        public async Task<IActionResult> Update(User user)
        {
            await _service.Update(user.Id, user, collection);

            return new OkObjectResult(user);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.Delete<User>(id, collection);

            return Ok();
        }
    }
}