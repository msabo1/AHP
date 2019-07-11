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
        /// <summary>
        /// Post method,
        /// /api/user/register/
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns created UserControllerModel</returns>
        [HttpPost]
        [Route("api/user/register")]
        public async Task<IHttpActionResult> PostRegister(UserControllerModel user)
        {
            if (user==null)
            {
                return BadRequest();
            }
            var _user = _mapper.Map<UserControllerModel, IUserModel>(user);
            var status = await _userService.RegisterAsync(_user);

            return Ok(status);
                
        }
        /// <summary>
        /// Post method,
        /// /api/user/login
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns UserControllerModel</returns>
        [HttpPost]
        [Route("api/user/login")]
        public async Task<IHttpActionResult> PostLogin(UserControllerModel user)
        {
            if (user==null)  
            {
                return BadRequest();
            }

            var _user = _mapper.Map<UserControllerModel, IUserModel>(user);
            var status = await _userService.GetAsync(_user);

            return Ok(_mapper.Map<IUserModel, UserControllerModel>(status));
        }
        /// <summary>
        /// Put method,
        /// /api/user/
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns updated UserControllerModel</returns>
        public async Task<IHttpActionResult> Put(UserControllerModel user)
        {
            if (user==null)
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
        /// <summary>
        /// Delete method,
        /// /api/user/
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns bool</returns>
        public async Task<IHttpActionResult> Delete(UserControllerModel user)
        {
            if (user==null)
            {
                return BadRequest();
            }
            var _user = _mapper.Map<UserControllerModel, IUserModel>(user);
            var status =  await _userService.DeleteAsync(_user);        
            return Ok(status);     
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
