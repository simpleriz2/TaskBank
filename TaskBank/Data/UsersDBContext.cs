using Microsoft.EntityFrameworkCore;
using TaskBank.Data.Entities;

namespace TaskBank.Entities
{
    public class UsersDBContext : DbContext
    {
        public UsersDBContext (DbContextOptions<UsersDBContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }
}
