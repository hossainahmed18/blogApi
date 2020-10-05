using blogApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blogApi.repositories.user
{
    public interface IUserRepository
    {
        public Task  Registration(User user);
        public ActionResult<IEnumerable<User>> Get();
        public User singleGet(String email, String password);
        
    }
}