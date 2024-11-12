using EventSourcing.Models;

namespace EventSourcing.Database
{
    public static class ContextHelper
    {
        public static  List<Player> Players { get; set; } = new List<Player>();
    }
}
