using Microsoft.EntityFrameworkCore;

namespace MinimalApi_Training
{
    public class ToDoDb:DbContext
    {
        public ToDoDb(DbContextOptions<ToDoDb> options) : base(options)
        {

        }

        public DbSet<ToDoItem> DoItems { get; set; }

    }
}
