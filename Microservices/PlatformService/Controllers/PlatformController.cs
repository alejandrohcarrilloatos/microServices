using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlatformService.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformsController(
            IPlatformRepo repository, 
            IMapper mapper, 
            ICommandDataClient commandDataClient,
            IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
        }

        //GET api/Platforms
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms()
        {
            Console.WriteLine("--> Getting all Platforms.");
            var PlatformItems = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(PlatformItems));
        }

        //GET api/Platforms/5
        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var PlatformItem = _repository.GetPlatformById(id);
            if (PlatformItem != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(PlatformItem));
            }
            return NotFound();
        }

        //POST api/Platforms
        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto PlatformCreateDto)
        {
            var PlatformModel = _mapper.Map<Platform>(PlatformCreateDto);
            _repository.CreatePlatform(PlatformModel);
            _repository.SaveChanges();

            var PlatformReadDto = _mapper.Map<PlatformReadDto>(PlatformModel);

            // Send Sync Message
            try
            {
                await _commandDataClient.SendPlatformToCommand(PlatformReadDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> No se pudo enviar el mesaje SINCRONO: { ex.Message } \n { ex.InnerException.Message }" );
            }
            // Send Async Message
            try
            {
                var platformPublishedDto = _mapper.Map<PlatformPublishedDto>(PlatformReadDto);
                platformPublishedDto.Event = "Platform_Published";
                _messageBusClient.PublishNewPlatform(platformPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> No se pudo enviar el mesaje ASINCRONO: { ex.Message } \n { ex.InnerException.Message }");
            }

            // Esta es la forma correcta de regresar el estatus 201 Created
            return CreatedAtRoute(nameof(GetPlatformById), new { id = PlatformReadDto.Id }, PlatformReadDto);
            //return Ok(PlatformModel);
        }

        //// PUT api/Platforms/id
        //[HttpPut("id")]
        //public ActionResult UpdatePlatform(int id, PlatformUpdateDto PlatformUpdateDto)
        //{
        //    var PlatformModelFromRepo = _repository.GetPlatformById(id);
        //    if (PlatformModelFromRepo == null)
        //    {
        //        return NotFound();
        //    }
        //    _mapper.Map(PlatformUpdateDto, PlatformModelFromRepo);

        //    _repository.UpdatePlatform(PlatformModelFromRepo);

        //    _repository.SaveChanges();

        //    return NoContent();
        //}

        //// PATCH api/Platforms/id
        //[HttpPatch("{id}")]
        //public ActionResult PartialPlatformUpdate(int id, JsonPatchDocument<PlatformUpdateDto> patchDoc)
        //{
        //    var PlatformModelFromRepo = _repository.GetPlatformById(id);
        //    if (PlatformModelFromRepo == null)
        //    {
        //        return NotFound();
        //    }

        //    var PlatformToPatch = _mapper.Map<PlatformUpdateDto>(PlatformModelFromRepo);
        //    patchDoc.ApplyTo(PlatformToPatch, ModelState);
        //    if (!TryValidateModel(PlatformToPatch))
        //    {
        //        return ValidationProblem(ModelState);
        //    }

        //    _mapper.Map(PlatformToPatch, PlatformModelFromRepo);

        //    _repository.UpdatePlatform(PlatformModelFromRepo);

        //    _repository.SaveChanges();

        //    return NoContent();
        //}

        //// DELETE api/Platforms/id
        //[HttpDelete("{id}")]
        //public ActionResult DeleteUpdate(int id)
        //{
        //    var PlatformModelFromRepo = _repository.GetPlatformById(id);
        //    if (PlatformModelFromRepo == null)
        //    {
        //        return NotFound();
        //    }

        //    _repository.DeletePlatform(PlatformModelFromRepo);
        //    _repository.SaveChanges();

        //    return NoContent();
        //}
    }

}
