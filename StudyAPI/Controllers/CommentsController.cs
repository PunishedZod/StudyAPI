using StudyAPI.Models;
using StudyAPI.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace StudyAPI.Controllers
{
    //Controller for the Comments table in the database
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly Service _service;
        private readonly string collection = "Comments"; //Readonly string set with collection name to use throughout the controller

        public CommentsController(Service service)
        {
            _service = service;
        }

        //Gets all items from the collection
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comments>>> GetAll()
        {
            return Ok(await _service.Get<Comments>(collection));
        }

        //Gets an item by post id from the collection
        [HttpGet("PostId={PostId:length(24)}")]
        public async Task<ActionResult<IEnumerable<Comments>>> GetCommentsByPost(string postid)
        {
            return Ok(await _service.GetCommentsByPost<Comments>(postid, collection));
        }

        //Gets an item by user id from the collection
        [HttpGet("UserId={UserId:length(24)}")]
        public async Task<ActionResult<IEnumerable<Comments>>> GetCommentsByUser(string userid)
        {
            return Ok(await _service.GetCommentsByUser<Comments>(userid, collection));
        }

        //Gets an item by item id from the collection
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

        //Posts an item to the collection
        [HttpPost]
        public async Task<IActionResult> Insert(Comments comment)
        {
            await _service.Insert(collection, comment);

            return new ObjectResult(comment);
        }

        //Updates an existing item in the collection by id
        [HttpPut]
        public async Task<IActionResult> Update(Comments comments)
        {
            await _service.Update(comments.Id, comments, collection);

            return new OkObjectResult(comments);
        }

        //Deletes an existing item in the collection by id, returns an OkResult response
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.Delete<Comments>(id, collection);

            return Ok();
        }
    }
}