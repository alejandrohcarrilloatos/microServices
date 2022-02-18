using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            // Source to Read DTO
            CreateMap<Platform, PlatformReadDto>();

            // Create DTO to Source
            CreateMap<PlatformCreateDto, Platform>();

            // Update DTO to Source
            //CreateMap<PlatformUpdateDto, Platform>();

            // Update DTO to Source
            //CreateMap<Platform, PlatformUpdateDto>();
        }
    }
}
