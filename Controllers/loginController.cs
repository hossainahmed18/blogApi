using System.Threading.Tasks;
using blogApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using blogApi.repositories.user;



namespace blogApi.Controllers{
    [Route("api/user/")]
    [ApiController]
    
    public class LoginController : ControllerBase{
        private readonly IConfiguration _config;
        private  IUserRepository _repo;

        public LoginController(IConfiguration config, IUserRepository repo){
            _config=config;
            _repo = repo;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> RegisterUser(User user){
            await _repo.Registration(user);
            return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(AuthenticationModel user){
            var retUser=_repo.singleGet(user.email, user.password);
            if(retUser!=null){
              
                return Ok(new { userDetails = new{userName=retUser.userName,userRole=retUser.userRole,email= retUser.email}});
               
            }else{
                return Ok("Not found");
            }
        }

       


    }

}