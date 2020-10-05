using blogApi.Context;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogApi.Models;


namespace blogApi.repositories.comment.PostComment{
    public class PostComment : IPostComment{
        private MainContext _mainContext; 

        public PostComment (MainContext mainContext){
           _mainContext = mainContext;
        }
        public ActionResult<IEnumerable<Comment>> Get(int id){

            var comments = _mainContext.comments.Where(x => x.postId == id).ToList();
            return comments;
        }

        

        public async Task Update(Comment comment,int id){
            
            Comment existingComment= _mainContext.comments.FirstOrDefault(c=>c.id==id);

            existingComment.commentBody = comment.commentBody;
            existingComment.userId = comment.userId;
            existingComment.postId = comment.postId;
    
            _mainContext.Attach(existingComment).State= Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _mainContext.SaveChangesAsync();

        }
        public async Task Create(Comment comment){
            await _mainContext.comments.AddAsync(comment);
            await _mainContext.SaveChangesAsync();
        }

        public async Task Delete(int id){
            Comment existingComment= _mainContext.comments.FirstOrDefault(p=>p.id==id);
            _mainContext.comments.Remove(existingComment);
            await _mainContext.SaveChangesAsync();
        }

        
    }
}