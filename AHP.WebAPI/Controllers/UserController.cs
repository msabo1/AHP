using AHP.Model;
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
        IUserService _userService;
        IMapper _mapper;
        public UserController(
            IMapper mapper,
            IUserService userService
            )
        {
            _mapper = mapper;
            _userService = userService;
        }    
        
        public async Task<IHttpActionResult> Post(UserControllerModel user)
        {
            if (user.Equals(null))
            {
                return BadRequest();
            }
            var _user = _mapper.Map<UserControllerModel, IUserModel>(user);
            var status = await _userService.CheckAsync(_user);

            if (status != null)
            {
                return Ok(status);
            }
            else
            {
                return NotFound();
            }
                
        }    
        public async Task<IHttpActionResult> Get(UserControllerModel user)
        {
            if (user.Equals(null))
            {
                return BadRequest();
            }

            var _user = _mapper.Map<UserControllerModel, IUserModel>(user);
            var status = await _userService.GetAsync(_user);

            if (status != null)
                return Ok(_mapper.Map<IUserModel, UserControllerModel>(status));
            else
                return NotFound();
        }
        public async Task<IHttpActionResult> Put(UserControllerModel user)
        {
            if (user.Equals(null))
            {
                return BadRequest();
            }
            var _user = _mapper.Map<UserControllerModel, IUserModel>(user);
            var status = await _userService.UpdateAsync(_user);
            if (status != null)
                return Ok(_mapper.Map<IUserModel, UserControllerModel>(status));
            else
                return NotFound();
        }
        public async Task<IHttpActionResult> Delete(UserControllerModel user)
        {
            if (user.Equals(null))
            {
                return BadRequest();
            }
            var _user = _mapper.Map<UserControllerModel, IUserModel>(user);
            var status =  await _userService.DeleteAsync(_user);
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
