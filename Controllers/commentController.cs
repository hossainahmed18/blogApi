using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using blogApi.repositories.comment;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace blogApi.Controllers
{
   
    [Route("api/comments/")]
    [ApiController]
    public class CommentController: ControllerBase{
        private  IPostComment _repo;
        public CommentController(IPostComment repo){
            _repo = repo;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("get/{id}")]
        public ActionResult<IEnumerable<Comment>> Get(int id){
            var allComments = _repo.Get(id);
            return allComments;
        }

        

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Comment>> CreateComment(Comment comment){
            await _repo.Create(comment);
            return Ok(comment);
        }

        [HttpPut]
        [Authorize]
        [Route("change/{id}")]
        public async Task<ActionResult<Comment>>UpdatePost(Comment comment,int id){
            await _repo.Update(comment,id);
            return Ok(comment);
        }

        [HttpDelete]
        [Authorize]
        [Route("change/{id}")]
        public async Task<ActionResult<Comment>>DeletePost(int id){
            await _repo.Delete(id);
            return Ok("deleted");
        }

      
        
    }
}