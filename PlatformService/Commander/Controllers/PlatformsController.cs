using Microsoft.AspNetCore.Mvc;
using System;

namespace CommandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController: ControllerBase
    {
        public PlatformsController()
        {

        }

        [HttpPost]
        public IActionResult TestInboundConnection()
        {
            Console.WriteLine("->> Inbound POST #Command Service");
            return Ok("Inbound test of from Platforms Controller");
        }
    }
}
