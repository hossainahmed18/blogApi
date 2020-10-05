using blogApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blogApi.repositories.comment
{
    public interface IPostComment
    {
        public ActionResult<IEnumerable<Comment>> Get(int id);

        public Task Create(Comment comment);

        public Task Update(Comment comment,int id);

        public Task Delete(int id);
        
    }
}
