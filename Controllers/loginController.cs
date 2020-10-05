using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using blogApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
        [AllowAnonymous]
        [Route("register")]
        public async Task<ActionResult<User>> RegisterUser(User user){
            await _repo.Registration(user);
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public ActionResult Login(AuthenticationModel user){
            var retUser=_repo.singleGet(user.email, user.password);
            if(retUser!=null){
                var tokenString = GenerateJWTToken(retUser);
                return Ok(new { token = tokenString, userDetails = new{userName=retUser.userName,userRole=retUser.userRole,email= retUser.email}});
               
            }else{
                return Ok("Not found");
            }
        }

        string GenerateJWTToken(User userInfo){
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.userName),
                new Claim("userName", userInfo.userName.ToString()),
                new Claim("role",userInfo.userRole),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
               issuer: _config["Jwt:Issuer"],
               audience: _config["Jwt:Audience"],
               claims: claims,
               expires: DateTime.Now.AddMinutes(30),
               signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }



    }

}