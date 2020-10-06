using blogApi.Context;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogApi.Models;
using Microsoft.EntityFrameworkCore;


namespace blogApi.repositories.post.PostRepository{
    public class PostRepository : IPostRepository{
        private MainContext _mainContext; 

        public PostRepository (MainContext mainContext){
           _mainContext = mainContext;
        }
        public ActionResult<IEnumerable<Post>> Get(string role){
            if(role == "admin"){
                var posts = _mainContext.posts.ToList();
                return posts;
            }
            else{
                var posts = _mainContext.posts.Where(x => x.status =="approved" ).ToList();
                return posts;
            }

        }

        public ActionResult<Post> singleGet(int id){
            return _mainContext.posts.FirstOrDefault(u=>u.id==id);
        }

        public async Task Update(Post post,int id){
            
            Post existingPost= _mainContext.posts.FirstOrDefault(p=>p.id==id);

            existingPost.title = post.title;
            existingPost.userId = post.userId;
            existingPost.body = post.body;
            existingPost.status =post.status;
            _mainContext.Attach(existingPost).State= Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _mainContext.SaveChangesAsync();

        }
        public async Task Create(Post post){
            await _mainContext.posts.AddAsync(post);
            await _mainContext.SaveChangesAsync();
        }

        public async Task Delete(int id){
            Post existingPost= _mainContext.posts.FirstOrDefault(p=>p.id==id);
            _mainContext.posts.Remove(existingPost);
            await _mainContext.SaveChangesAsync();
        }

        
    }
}