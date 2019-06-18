using AHP.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AHP.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        IUserLogin _userLogin;
        
        public UserController(IUserLogin userLogin)
        {
            _userLogin = userLogin;
        }
        public async void Post([FromBody]string[] value)
        {
            
            await _userLogin.Check(value[0],value[1]);

            
        }
    }
}
