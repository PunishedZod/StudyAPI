using StudyAPI.Models;
using StudyAPI.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace StudyAPI.Controllers
{
    //Controller for the Posts table in the database
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly Service _service;
        private readonly string collection = "Posts"; //Readonly string set with collection name to use throughout the controller

        public PostsController(Service service)
        {
            _service = service;
        }

        //Gets all items from the collection
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Posts>>> GetAll()
        {
            return Ok(await _service.Get<Posts>(collection));
        }

        //Gets all items sorted in recent order from the collection
        [HttpGet("RecentPosts")]
        public async Task<IActionResult> GetRecent()
        {
            return new ObjectResult(await _service.GetRecent<Posts>(collection));
        }

        //Gets an item by user id from the collection
        [HttpGet("UserId={UserId:length(24)}")]
        public async Task<ActionResult<IEnumerable<Posts>>> GetPostsByUser(string userid)
        {
            return Ok(await _service.GetPostsByUser<Posts>(userid, collection));
        }

        //Gets an item by item id from the collection
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var post = await _service.Get<Posts>(id, collection);

            if (post == null)
            {
                return NotFound();
            }

            return new ObjectResult(post);
        }

        //Posts an item to the collection
        [HttpPost]
        public async Task<IActionResult> Insert(Posts post)
        {
            await _service.Insert(collection, post);

            return new ObjectResult(post);
        }

        //Updates an existing item in the collection by id
        [HttpPut]
        public async Task<IActionResult> Update(Posts post)
        {
            await _service.Update(post.Id, post, collection);

            return new OkObjectResult(post);
        }

        //Deletes an existing item in the collection by id, returns an OkResult response
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.Delete<Posts>(id, collection);

            return Ok();
        }
    }
}