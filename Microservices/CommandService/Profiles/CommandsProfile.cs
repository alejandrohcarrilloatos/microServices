using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;

namespace CommandService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Source -> Target
            CreateMap<Platform, PlatformReadDto>();

            CreateMap<PlatformPublishedDto, Platform>()
                .ForMember( dest => dest.ExternalId, 
                            opt => opt.MapFrom(src => src.Id) 
                          );

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
