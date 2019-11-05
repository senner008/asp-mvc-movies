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
            return Ok("Hello World");
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult ActionMethod([FromBody] MyClass myclass)
        {
            System.Console.WriteLine("action method");
            return Ok(myclass);
        }
    }

    public class MyClass
    {
        public int Prop { get; set; }
    }
}
