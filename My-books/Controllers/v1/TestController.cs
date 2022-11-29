using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_books.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiVersion("1.2")]
    [ApiVersion("1.3")]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-data"), MapToApiVersion("1.1")]
        public IActionResult Getv1()
        {
            return Ok("This is version V1");
        }
        [HttpGet("get-test-data"), MapToApiVersion("1.2")]
        public IActionResult Getv12()
        {
            return Ok("This is version V1.2");
        }
        [HttpGet("get-test-data"), MapToApiVersion("1.3")]
        public IActionResult Getv13()
        {
            return Ok("This is version V1.3");
        }

    }
}
