using System.Collections.Generic;
using System.Text;

namespace DDD.EscapeRoom.Core.ApplicationLayer.Commands
{
    public enum PlayerStatusCommand
    {
        Active = 0,  // Gracz aktywny
        Banned = 1  // Gracz zbanowany
    }

    public class CreatePlayerCommand
    {
        public long PlayerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public PlayerStatusCommand Status { get; set; }
    }
}
