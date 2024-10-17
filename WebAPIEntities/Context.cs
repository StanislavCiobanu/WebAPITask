using System.Data.Entity;
using WebAPITask.Models;

namespace WebAPIEntity
{
    class Context : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }

        public Context() { }    
    }
}
