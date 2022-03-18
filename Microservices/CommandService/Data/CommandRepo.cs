using CommandService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDBContext _context;

        public CommandRepo(AppDBContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command cmd)
        {
            if(cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.Commands.Add(cmd);
        }

        public void CreateCommand(int platformId, Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            cmd.PlatformId = platformId;
            _context.Commands.Add(cmd);
        }

        public void CreatePlatform(Platform plat)
        {
            if (plat == null)
            {
                throw new ArgumentNullException(nameof(plat));
            }
            _context.Platforms.Add(plat);
        }

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Commands.Remove(cmd);

        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.Commands.ToList();
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return _context.Commands.Where(c =>  c.PlatformId == platformId && c.Id == commandId).FirstOrDefault();
        }

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _context.Commands
                .Where(c => c.PlatformId == platformId)
                .OrderBy(c=> c.Platform.Name);
        }

        public Command GetCommandForPlatform(int platformId, int commandId)
        {
            return _context.Commands
                .Where(c => c.PlatformId == platformId && c.Id == commandId).FirstOrDefault();
        }

        public bool PlatformExists(int platformId)
        {
            return _context.Platforms.Any(p => p.Id == platformId);
        }

        public void UpdateCommand(Command cmd)
        {
            //Nothing
        }

        public void UpdateCommand(int platformId, Command cmd)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);

        }


    }
}
