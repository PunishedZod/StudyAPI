using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using StudyAPI.Services;
using StudyAPI.Models;

namespace StudyAPI.Controllers
{
    //Controller for the Comments table in the database
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly Service _service;
        private readonly string collection = "Comments";

        public CommentsController(Service service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comments>>> GetAll()
        {
            return Ok(await _service.Get<Comments>(collection));
        }

        [HttpGet("PostId={PostId:length(24)}")]
        public async Task<ActionResult<IEnumerable<Comments>>> GetCommentsByPost(string postid)
        {
            return Ok(await _service.GetCommentsByPost<Comments>(postid, collection));
        }

        [HttpGet("UserId={UserId:length(24)}")]
        public async Task<ActionResult<IEnumerable<Comments>>> GetCommentsByUser(string userid)
        {
            return Ok(await _service.GetCommentsByUser<Comments>(userid, collection));
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var comment = await _service.Get<Comments>(id, collection);

            if (comment == null)
            {
                return NotFound();
            }

            return new ObjectResult(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Comments comment)
        {
            await _service.Insert(collection, comment);

            return new ObjectResult(comment);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Comments comments)
        {
            await _service.Update(comments.Id, comments, collection);

            return new OkObjectResult(comments);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.Delete<Comments>(id, collection);

            return Ok();
        }
    }
}