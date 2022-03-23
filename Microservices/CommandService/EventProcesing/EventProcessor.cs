using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace CommandService.EventProcesing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopedFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopedFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {

        }
    }

    enum EventType
    {
        PlatformPublished,
        Undeterminated
    }
}
