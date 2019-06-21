using AHP.Service;
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
        public async Task<string> Get()
        {
           return await _userLogin.Check("Mario", "123");

        }
    }
}
