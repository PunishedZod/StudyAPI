using Microsoft.AspNetCore.Mvc;
using StudyAPI.Models;
using StudyAPI.Models.Interfaces;
using StudyAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyAPI.Controllers
{
    //Controller for the Posts table in the database
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly Service _service;

        public PostsController(Service service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new ObjectResult(await _service.Get<Posts>("Posts"));
        }

        [HttpGet("RecentPosts")]
        public async Task<IActionResult> GetRecent()
        {
            return new ObjectResult(await _service.GetRecent<Posts>("Posts"));
        }

        [HttpGet("PopularPosts")]
        public async Task<IActionResult> GetPopular()
        {
            return new ObjectResult(await _service.GetPopular<Posts>("Posts"));
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var post = await _service.Get<Posts>(id, "Posts");

            if (post == null)
            {
                return NotFound();
            }

            return new ObjectResult(post);
        }

        [HttpPost]
        public async Task<ActionResult> Insert(Posts post)
        {
            await _service.Insert("Posts", post);

            return CreatedAtRoute("PostPosts", new { id = post.Id.ToString() }, post);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Posts post)
        {
            var postDB = await _service.Get<Posts>(id, "Posts");

            if (postDB == null)
            {
                return NotFound();
            }

            post.Id = postDB.Id;

            await _service.Update(post.Id, post, "Posts");

            return new OkObjectResult(post);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var postDB = await _service.Get<Posts>(id, "Posts");

            if (postDB == null)
                return new NotFoundResult();

            await _service.Delete<Posts>(id, "Posts");

            return new OkResult();
        }
    }
}