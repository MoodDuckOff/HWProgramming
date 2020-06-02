using System;
using System.Linq;
using HWP_backend.Models.DTO.Builds;
using HWP_backend.Services.BuilderServices;
using Microsoft.AspNetCore.Mvc;

namespace HWP_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildController : ControllerBase
    {
        private readonly string _name;
        private readonly string _path;
        private readonly BuildService _service;

        public BuildController(BuildService service)
        {
            _service = service;
            _path = "C:\\Compiler";
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            _name = new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // POST: api/Build

        [HttpPost("run")]
        public IActionResult Run([FromBody] BuildModelDTO model)
        {
            _service.MakeFile(_path, _name, model.Code);
            if (string.IsNullOrEmpty(model.Input)) return Ok(_service.BuildAndRun(_path, _name, model.Code));
            var inputs = model.Input.Split("\n");
            return Ok(_service.BuildAndRun(_path, _name, inputs));
        }
    }
}