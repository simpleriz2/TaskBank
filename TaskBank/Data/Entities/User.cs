using Microsoft.EntityFrameworkCore;

namespace TaskBank.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        public required int Sub {  get; set; }

        public required ICollection<Level> Levels { get; set; }
    }

}
