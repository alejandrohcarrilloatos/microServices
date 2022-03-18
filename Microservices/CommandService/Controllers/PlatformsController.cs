using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CommandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController: ControllerBase
    {
        private readonly ICommandRepo _repository;
        public IMapper _mapper { get; }


        public PlatformsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //GET api/c/platforms
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms()
        {
            Console.WriteLine("Getting all Platforms");
            var Items = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(Items));
        }

        [HttpPost]
        public IActionResult TestInboundConnection()
        {
            Console.WriteLine("->> Inbound POST #Command Service");
            return Ok("Inbound test of from Platforms Controller");
        }
    }
}
