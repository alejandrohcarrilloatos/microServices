using CommandService.Models;
using System.Collections.Generic;

namespace CommandService.Data
{
    public class MockCommanderRepo : ICommandRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void CreateCommand(int platformId, Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void CreatePlatform(Platform plat)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public bool ExternalPlatformExists(int externalPlatformId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command> {
                new Command { Id = 0, HowTo = "Hieve un huevo", Commandline="Hieve agua", PlatformId = 1 },
                new Command { Id = 1, HowTo = "Cortar Pan", Commandline="Conseguir un cochillo", PlatformId = 1 },
                new Command { Id = 2, HowTo = "Hacer una taza de té", Commandline="Poner un sobre de the en la taza", PlatformId = 1 }
            };
            return commands;
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            throw new System.NotImplementedException();
        }

        public Command GetCommand(int platformId, int commandId)
        {
            throw new System.NotImplementedException();
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id = 0, HowTo = "Hieve un huevo", Commandline="Hieve agua", PlatformId = 1 };
        }

        public Command GetCommandForPlatform(int platformId, int commandId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            throw new System.NotImplementedException();
        }

        public bool PlatformExists(int platformId)
        {
            throw new System.NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            //Nothing
        }

        public void UpdateCommand(int platformId, Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}
