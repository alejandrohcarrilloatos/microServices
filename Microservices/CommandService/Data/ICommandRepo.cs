using CommandService.Models;
using System.Collections.Generic;

namespace CommandService.Data
{
    public interface ICommandRepo
    {
        bool SaveChanges();
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform plat);
        bool PlatformExists(int platformId);

        IEnumerable<Command> GetAllCommands();
        IEnumerable<Command> GetCommandsForPlatform(int platformId);
        Command GetCommandForPlatform(int platformId, int commandId);
        Command GetCommandById(int id);
        Command GetCommand(int platformId, int commandId);
        void CreateCommand(int platformId, Command cmd);
        void UpdateCommand(int platformId, Command cmd);
        void DeleteCommand(Command cmd);


    }
}
