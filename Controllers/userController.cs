using System.Collections.Generic;
using blogApi.Models;
using Microsoft.AspNetCore.Mvc;
using blogApi.repositories.user;
using Microsoft.AspNetCore.Authorization;


namespace blogApi.Controllers
{
   
    [Route("api/users/")]
    [ApiController]
    public class UserController: ControllerBase{
        private  IUserRepository _repo;
        public UserController(IUserRepository repo){
            _repo = repo;
        }

        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        [Route("get")]
        public ActionResult<IEnumerable<User>> Get(){
            var allusers = _repo.Get();
            return allusers;
        }

      
        
    }
}