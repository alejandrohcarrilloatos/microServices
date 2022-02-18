using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Source to Read DTO
            CreateMap<Command, CommandReadDto>();

            // Create DTO to Source
            CreateMap<CommandCreateDto, Command>();

            // Update DTO to Source
            CreateMap<CommandUpdateDto, Command>();

            // Update DTO to Source
            CreateMap<Command, CommandUpdateDto>();
        }
    }
}
