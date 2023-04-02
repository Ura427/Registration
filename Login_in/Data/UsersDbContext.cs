using Login_in.Models;
using Microsoft.EntityFrameworkCore;

namespace Login_in.Data
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
