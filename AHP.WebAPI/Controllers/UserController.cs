using AHP.Model;
using AHP.Model.Common;
using AHP.Service;
using AHP.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace AHP.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        IUserLogin _userLogin;
        
        public UserController(IUserLogin userLogin)
        {
            _userLogin = userLogin;
        }
        [System.Web.Http.Route("User/Index")]
        public async Task<bool> Get()
        {
           
           return await _userLogin.Check("Tomljanovic", "123");
        }
        //public async Task<IUserModel> Post(UserModel user)
        //{
            
        //}
       
    }
}
