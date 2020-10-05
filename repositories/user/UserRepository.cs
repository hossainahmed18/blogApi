using blogApi.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using blogApi.Models;
using blogApi.repositories.user;


namespace blogApi.repositories.user.UserRepository{
    public class UserRepository : IUserRepository{
        private MainContext _mainContext; 

        public UserRepository (MainContext mainContext){
           _mainContext = mainContext;
        }
        public async Task Registration(User user){
            if(user == null){
                throw new ArgumentNullException(nameof(user));
            }
            await _mainContext.users.AddAsync(user);
            await _mainContext.SaveChangesAsync();

        }
        public ActionResult<IEnumerable<User>> Get(){
            var book = _mainContext.users.ToList();
            return book;
        }
        public User singleGet(String email, String password){
              return _mainContext.users.FirstOrDefault(u=>u.email==email && u.password == u.password );
        }

        
    }
}