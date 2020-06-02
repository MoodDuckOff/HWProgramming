using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HWP_backend.Entities;
using HWP_backend.Helpers;
using HWP_backend.Models.DTO.SolvedTasks;
using HWP_backend.Services.SolvedTaskServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HWP_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolvedTasksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SolvedTaskService _solvedTaskService;

        public SolvedTasksController(
            IMapper mapper,
            SolvedTaskService solvedTaskService)
        {
            _mapper = mapper;
            _solvedTaskService = solvedTaskService;
        }

        //Student section

        [Authorize(Roles = Role.User)]
        [HttpPost("solve")]
        public IActionResult Create([FromBody] CreateSolvedTaskDTO model)
        {
            var solvedTask = _mapper.Map<SolvedTask>(model);
            try
            {
                _solvedTaskService.Create(solvedTask, solvedTask.UserId, solvedTask.TaskId);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        [Authorize(Roles = Role.Admin + "," + Role.Teacher)]
        [HttpGet]
        public IActionResult GetAllSolvedTasks()
        {
            var solvedTasks = _solvedTaskService.GetAllSolvedTasks();
            if (solvedTasks == null || !solvedTasks.Any())
                return NotFound("Solved tasks not found");

            var model = _mapper.Map<IList<SolvedTaskDTO>>(solvedTasks);
            return Ok(model);
        }


        [Authorize]
        [HttpGet("user/{userId}")]
        public IActionResult GetAllSolvedTasksByUser(int userId)
        {
            var solvedTasks = _solvedTaskService.GetSolvedTasksByUserId(userId);
            if (solvedTasks == null || !solvedTasks.Any())
                return NotFound("Solved tasks not found");

            var model = _mapper.Map<IList<SolvedTaskDTO>>(solvedTasks);
            return Ok(model);
        }

        [Authorize(Roles = Role.Admin + "," + Role.Teacher)]
        [HttpGet("task/{taskId}")]
        public IActionResult GetAllSolvedTasksByTask(int taskId)
        {
            var solvedTasks = _solvedTaskService.GetSolvedTasksByTaskId(taskId);
            if (solvedTasks == null || !solvedTasks.Any())
                return NotFound("Solved tasks not found");
            var model = _mapper.Map<IList<SolvedTaskDTO>>(solvedTasks);
            return Ok(model);
        }

        [Authorize]
        [HttpGet("user/{userId}/task/{taskId}")]
        public IActionResult GetSolvedTask(int userId, int taskId)
        {
            var solvedTask = _solvedTaskService.GetSolvedTask(userId, taskId);
            if (solvedTask == null)
                return NotFound("Solved task not found");

            var model = _mapper.Map<SolvedTaskDTO>(solvedTask);
            return Ok(model);
        }

        [Authorize]
        [HttpPut("update")]
        public IActionResult Update([FromBody] UpdateSolvedTaskDTO model)
        {
            var solvedTask = _mapper.Map<SolvedTask>(model);
            try
            {
                _solvedTaskService.Update(solvedTask);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        [Authorize(Roles = Role.Admin + "," + Role.Teacher)]
        [HttpPut("rate")]
        public IActionResult Rate([FromBody] RateSolvedTaskDTO model)
        {
            var solvedTask = _mapper.Map<SolvedTask>(model);
            try
            {
                _solvedTaskService.RateSolvedTask(solvedTask);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        [Authorize(Roles = Role.Admin + "," + Role.Teacher)]
        [HttpDelete("delete")]
        public IActionResult Delete([FromBody] UpdateSolvedTaskDTO model)
        {
            _solvedTaskService.Delete(model.UserId, model.TaskId);
            return Ok();
        }
    }
}