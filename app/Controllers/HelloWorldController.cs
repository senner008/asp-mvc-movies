using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using asp_mvc.Models;

namespace asp_mvc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        private readonly ILogger<HelloWorldController> _logger;

        public HelloWorldController(ILogger<HelloWorldController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public IActionResult ActionMethod()
        {
            return Ok("Default");
        }

        [HttpGet]
        [Route("getname")]
        public IActionResult ActionMethodQuery([FromQuery] string name)
        {
            var n = "<a href='javascript:alert('Something Evil!')'>Mr. Hacker</a>";
            return Ok(n);
        }

    }
}
