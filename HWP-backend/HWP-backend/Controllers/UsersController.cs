using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using HWP_backend.Entities;
using HWP_backend.Helpers;
using HWP_backend.Models.DTO.Tasks;
using HWP_backend.Models.DTO.Users;
using HWP_backend.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HWP_backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public UsersController(
            UserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModelDTO model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new {message = "Username or password is incorrect"});

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                user.Id,
                user.Username,
                user.FirstName,
                user.LastName,
                user.Role,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModelDTO model)
        {
            // map model to entity
            var user = _mapper.Map<User>(model);
            if (user.Role == Role.Admin || string.IsNullOrWhiteSpace(user.Role)) user.Role = Role.User;
            try
            {
                // create user
                _userService.Create(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new {message = ex.Message});
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var model = _mapper.Map<IList<UserModelDTO>>(users);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();
            var model = _mapper.Map<UserModelDTO>(user);
            return Ok(model);
        }

        [HttpGet("{userId}/tasks")]
        public IActionResult GetUserTasks(int userId)
        {
            var tasks = _userService.UnsolvedTasks(userId);
            if (tasks == null || !tasks.Any())
                return NotFound("Tasks not found");
            var model = _mapper.Map<IList<TaskModelDTO>>(tasks);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateModelDTO model)
        {
            // map model to entity and set id
            var user = _mapper.Map<User>(model);
            user.Id = id;

            if (User.Identity.Name == null) return BadRequest("name == null");
            var currentUserId = int.Parse(User.Identity.Name);
            switch (user.Role)
            {
                case Role.Admin when !User.IsInRole(Role.Admin):
                    return Forbid();
                case Role.Teacher when User.IsInRole(Role.User):
                    return Forbid();
            }

            if (id != currentUserId && User.IsInRole(Role.User))
                return Forbid();

            try
            {
                // update user 
                _userService.Update(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new {message = ex.Message});
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (User.Identity.Name == null) return BadRequest("name == null");
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && User.IsInRole(Role.User))
                return Forbid();

            _userService.Delete(id);
            return Ok();
        }
    }
}