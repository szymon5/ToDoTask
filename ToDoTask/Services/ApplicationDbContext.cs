using Microsoft.EntityFrameworkCore;
using ToDoTask.Models;

namespace ToDoTask.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<SingleTask> SingleTasks { get; set; }
    }
}
