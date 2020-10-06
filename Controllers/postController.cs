using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using blogApi.repositories.post;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace blogApi.Controllers
{
   
    [Route("api/posts/")]
    [ApiController]
    public class PostController: ControllerBase{
        private  IPostRepository _repo;
        public PostController(IPostRepository repo){
            _repo = repo;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("get/{role}")]
        public ActionResult<IEnumerable<Post>> Get(string role){
            var allPosts = _repo.Get(role);
            return allPosts;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("singlepost/{id}")]
        public ActionResult<Post> SinglePost(int id){
            var singlePost = _repo.singleGet(id);
            return singlePost;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Post>> CreatePost(Post post){
            await _repo.Create(post);
            return Ok(post);
        }

        [HttpPut]
        [Authorize]
        [Route("singlepost/{id}")]
        public async Task<ActionResult<Post>>UpdatePost(Post post,int id){
            await _repo.Update(post,id);
            return Ok(post);
        }

        [HttpDelete]
        [Authorize]
        [Route("singlepost/{id}")]
        public async Task<ActionResult<Post>>DeletePost(int id){
            await _repo.Delete(id);
            return Ok("post deleted ");
        }

      
        
    }
}