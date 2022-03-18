using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CommandService.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //private readonly MockCommanderRepo _repository =  new MockCommanderRepo();

        //GET api/c/platforms/{platformId}/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsFormPlatform(int platformId)
        {
            Console.WriteLine($"--> Getting Commands from the Platform: {platformId}");

            if (!_repository.PlatformExists(platformId))
            {
                return NotFound();
            }

            var commandItems = _repository.GetCommandsForPlatform(platformId);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        //GET api/commands/5
        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"--> Getting Command from the Platform: {platformId} / {commandId}");
            if (!_repository.PlatformExists(platformId))
            {
                return NotFound();
            }

            var commandItem = _repository.GetCommandForPlatform(platformId, commandId);

            if (commandItem != null) {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            return NotFound();
        }

        //POST "api/c/platforms/{platformId}/commands
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandCreateDto)
        {
            Console.WriteLine($"--> CREATING Command from the Platform: {platformId} ");
            if (!_repository.PlatformExists(platformId))
            {
                return NotFound();
            }
            Console.WriteLine($"Command TO CreateDto: ${commandCreateDto}");
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(platformId, commandModel);
            _repository.SaveChanges();
            
            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
            
            // Esta es la forma correcta de regresar el estatus 201 Created
            return CreatedAtRoute(nameof(GetCommandForPlatform), 
                new {platformId = platformId, commandId = commandReadDto.Id }, commandReadDto);
            //return Ok(commandModel);
        }

        // PUT api/commands/id
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto) 
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(commandUpdateDto, commandModelFromRepo);
            
            _repository.UpdateCommand(1, commandModelFromRepo);

            _repository.SaveChanges();
            
            return NoContent();
        }

        // PATCH api/commands/id
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);
            if (!TryValidateModel(commandToPatch)) {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandModelFromRepo);

            _repository.UpdateCommand(1, commandModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        // DELETE api/commands/id
        [HttpDelete("{id}")]
        public ActionResult DeleteUpdate(int id){
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteCommand(commandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }



    }
}
