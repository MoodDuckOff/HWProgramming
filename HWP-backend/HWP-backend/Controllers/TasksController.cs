using System.Collections.Generic;
using AutoMapper;
using HWP_backend.Entities;
using HWP_backend.Helpers;
using HWP_backend.Models.DTO.Tasks;
using HWP_backend.Services.TaskServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HWP_backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly TaskService _taskService;

        public TasksController(
            TaskService taskService,
            IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        [Authorize(Roles = Role.Admin + "," + Role.Teacher)]
        [HttpPost("create/{authorId}")]
        public IActionResult Create(int authorId, [FromBody] CreateTaskModelDTO model)
        {
            var task = _mapper.Map<Task>(model);
            try
            {
                _taskService.Create(task, authorId);
                return Ok($"Created, id - {task.Id}");
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var tasks = _taskService.GetAll();
            if (tasks == null)
                return NotFound("Users not found");

            var model = _mapper.Map<IList<TaskModelDTO>>(tasks);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var task = _taskService.GetById(id);
            if (task == null)
                return NotFound("User id not found");
            var model = _mapper.Map<TaskModelDTO>(task);
            return Ok(model);
        }

        [Authorize(Roles = Role.Admin + "," + Role.Teacher)]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateTaskModelDTO model)
        {
            var task = _mapper.Map<Task>(model);
            task.Id = id;
            try
            {
                _taskService.Update(task);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        [Authorize(Roles = Role.Admin + "," + Role.Teacher)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _taskService.Delete(id);
            return Ok();
        }
    }
}