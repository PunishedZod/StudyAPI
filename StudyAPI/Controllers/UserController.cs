using StudyAPI.Models;
using StudyAPI.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace StudyAPI.Controllers
{
    //Controller for the User table in the database
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Service _service;
        private readonly string collection = "User"; //Readonly string set with collection name to use throughout the controller

        public UserController(Service service)
        {
            _service = service;
        }

        //Gets all items from the collection
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return Ok(await _service.Get<User>(collection));
        }

        //Gets an item by id from the collection
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

        //Gets an item by uname and pword from the collection
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

        //Posts an item to the collection
        [HttpPost]
        public async Task Insert(User user)
        {
            await _service.Insert(collection, user);
        }

        //Updates an existing item in the collection by id
        [HttpPut]
        public async Task<IActionResult> Update(User user)
        {
            await _service.Update(user.Id, user, collection);

            return new ObjectResult(user);
        }

        //Deletes an existing item in the collection by id, returns an OkResult response
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.Delete<User>(id, collection);

            return Ok();
        }
    }
}