using blogApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blogApi.repositories.post
{
    public interface IPostRepository
    {
        public ActionResult<IEnumerable<Post>> Get(string role);

        public ActionResult<Post> singleGet(int id);

        public Task Create(Post Post);

        public Task Update(Post post,int id);

        public Task Delete(int id);
        
    }
}