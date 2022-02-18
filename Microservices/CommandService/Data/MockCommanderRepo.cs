using Commander.Models;
using System.Collections.Generic;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command> {
                new Command { Id = 0, HowTo = "Hieve un huevo", Line="Hieve agua", Platform = "olla y sarten" },
                new Command { Id = 1, HowTo = "Cortar Pan", Line="Conseguir un cochillo", Platform = "Cuchillo y tabla de picar" },
                new Command { Id = 2, HowTo = "Hacer una taza de té", Line="Poner un sobre de the en la taza", Platform = "olla y taza" }
            };
            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id = 0, HowTo = "Hieve un huevo", Line="Hieve agua", Platform = "olla y sarten" };
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            //Nothing
        }
    }
}
