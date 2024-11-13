using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventSourcing.Models
{
    public class Player
    {
        [Key]
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }

    }
}
