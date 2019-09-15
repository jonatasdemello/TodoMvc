using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoMvc.Models;

namespace TodoMvc.Repository
{
    public class TodoContext : DbContext
    {
        public TodoContext (DbContextOptions<TodoContext> options) : base(options)
        {
        }
        public TodoContext()
        {

        }

        public DbSet<TodoItemModel> TodoItems { get; set; }
        public DbSet<TodoListModel> TodoLists { get; set; }
    }
}
