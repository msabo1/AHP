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
        IUserLoginService _userLogin;
        IUserRegisterService _userRegister;
        IUserUpdateService _userUpdate;
        public UserController(
            IUserLoginService userLogin, 
            IUserRegisterService userRegister,
            IUserUpdateService userUpdate)
        {
            _userRegister = userRegister;
            _userLogin = userLogin;
            _userUpdate = userUpdate;
        }


        [System.Web.Http.Route("User/Register")]
        public async Task<IHttpActionResult> Post(UserModel user)
        {
            var status = await _userRegister.Check(user);

            if (status != null)
                return Ok(status);
            else
                return NotFound();
        }

        [System.Web.Http.Route("User/Login")]
        public async Task<IHttpActionResult> Get(UserModel user)
        {

            var status = await _userLogin.Check(user.Username, user.Password);
            if (status != null)
                return Ok(status);
            else
                return NotFound();
        }
        [System.Web.Http.Route("User/Update")]
        public async Task<IHttpActionResult> Put(UserModel user)
        {
            var status = await _userUpdate.Update(user);
            if (status != null)
                return Ok(status);
            else
                return NotFound();

        }
        //public async Task<bool> Delete(UserModel user)
        //{


        //}


    }
    public class UserControllerModel
    {
        public System.Guid UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
