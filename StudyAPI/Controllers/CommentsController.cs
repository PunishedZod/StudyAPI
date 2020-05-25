using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyAPI.Models;
using StudyAPI.Models.Interfaces;
using StudyAPI.Services;

namespace StudyAPI.Controllers
{
    //Controller for the Comments table in the database
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly Service _service;

        public CommentsController(Service service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new ObjectResult(await _service.Get<Comments>("Comments"));
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var comment = await _service.Get<Comments>(id, "Comments");

            if (comment == null)
            {
                return NotFound();
            }

            return new ObjectResult(comment);
        }

        [HttpPost]
        public async Task<ActionResult> Insert(Comments comment)
        {
            await _service.Insert("Comments", comment);

            return CreatedAtRoute("PostComments", new { id = comment.Id.ToString() }, comment);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Comments comment)
        {
            var commentDB = await _service.Get<Comments>(id, "Comments");

            if (commentDB == null)
            {
                return NotFound();
            }

            comment.Id = commentDB.Id;

            await _service.Update(comment.Id, comment, "Comments");

            return new OkObjectResult(comment);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var commentDB = await _service.Get<Comments>(id, "Comments");

            if (commentDB == null)
                return new NotFoundResult();

            await _service.Delete<Comments>(id, "Comments");

            return new OkResult();
        }
    }
}