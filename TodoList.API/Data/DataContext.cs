using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}