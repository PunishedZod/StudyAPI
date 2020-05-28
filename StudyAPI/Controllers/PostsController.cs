using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using StudyAPI.Services;
using StudyAPI.Models;

namespace StudyAPI.Controllers
{
    //Controller for the Posts table in the database
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly Service _service;
        private readonly string collection = "Posts";

        public PostsController(Service service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Posts>>> GetAll()
        {
            return Ok(await _service.Get<Posts>(collection));
        }

        [HttpGet("RecentPosts")]
        public async Task<IActionResult> GetRecent()
        {
            return new ObjectResult(await _service.GetRecent<Posts>(collection));
        }

        [HttpGet("UserId={UserId:length(24)}")]
        public async Task<ActionResult<IEnumerable<Posts>>> GetPostsByUser(string userid)
        {
            return Ok(await _service.GetPostsByUser<Posts>(userid, collection));
        }

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

        [HttpPost]
        public async Task<IActionResult> Insert(Posts post)
        {
            await _service.Insert(collection, post);

            return new ObjectResult(post);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Posts post)
        {
            await _service.Update(post.Id, post, collection);

            return new OkObjectResult(post);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.Delete<Posts>(id, collection);

            return Ok();
        }
    }
}