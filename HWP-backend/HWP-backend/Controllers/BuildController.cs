using System;
using System.Linq;
using System.Runtime.InteropServices;
using HWP_backend.Helpers;
using HWP_backend.Models.DTO.Builds;
using HWP_backend.Services.BuilderServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HWP_backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BuildController : ControllerBase
    {
        private string _name;
        private readonly string _path;
        private readonly string _linuxPath;
        private readonly BuildService _service;

        public BuildController(
            BuildService service,
            IOptions<AppSettings> appSettings)
        {
            _service = service;
            _path = appSettings.Value.WorkDir;
            _linuxPath = appSettings.Value.LinuxDir;
            _name = "default";
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Run([FromBody] BuildModelDTO model)
        {
            var isLinux = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            var path = isLinux ? _linuxPath : _path;


            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            _name = new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            _service.MakeFile(path, _name, model.Code, isLinux);
            if (string.IsNullOrEmpty(model.Input))
                return Ok(_service.BuildAndRun(path, _name, isLinux, null));
            var inputs = model.Input.Split("\n");
            return Ok(_service.BuildAndRun(path, _name, isLinux, inputs));
        }
    }
}