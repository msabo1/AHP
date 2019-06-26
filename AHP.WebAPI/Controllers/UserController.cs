﻿using AHP.Model;
using AHP.Model.Common;
using AHP.Service;
using AHP.Service.Common;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace AHP.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        IUserLoginService _userLogin;
        IUserRegisterService _userRegister;
        IUserUpdateService _userUpdate;
        IUserDeleteService _userDelete;
        IMapper _mapper;
        public UserController(
            IMapper mapper,
            IUserLoginService userLogin, 
            IUserRegisterService userRegister,
            IUserUpdateService userUpdate,
            IUserDeleteService userDelete)
        {
            _mapper = mapper;
            _userRegister = userRegister;
            _userLogin = userLogin;
            _userUpdate = userUpdate;
            _userDelete = userDelete;
        }

       
        public async Task<IHttpActionResult> Post(UserControllerModel user)
        {
            var _user = _mapper.Map<UserControllerModel, IUserModel>(user);
            var status = await _userRegister.Check(_user);

            if (status != null)
                return Ok(_mapper.Map<IUserModel, UserControllerModel>(status));
            else
                return NotFound();
        }
        [Route("User/Login")]
        public async Task<IHttpActionResult> Get(UserControllerModel user)
        {
            var _user = _mapper.Map<UserControllerModel, IUserModel>(user);
            var status = await _userLogin.Check(_user.Username, _user.Password);
            if (status != null)
                return Ok(_mapper.Map<IUserModel, UserControllerModel>(status));
            else
                return NotFound();
        }

      
        public async Task<IHttpActionResult> Put(UserControllerModel user)
        {
            var _user = _mapper.Map<UserControllerModel, IUserModel>(user);
            var status = await _userUpdate.Update(_user);
            if (status != null)
                return Ok(_mapper.Map<IUserModel, UserControllerModel>(status));
            else
                return NotFound();
        }


        public async Task<IHttpActionResult> Delete(UserControllerModel user)
        {
            var _user = _mapper.Map<UserControllerModel, IUserModel>(user);
            var status =  await _userDelete.Delete(_user);
            if (status)
                return Ok();
            else
                return NotFound();
        }
    }
    public class UserControllerModel
    {
        public System.Guid UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<IChoiceModel> Choices { get; set; }
    }
}
