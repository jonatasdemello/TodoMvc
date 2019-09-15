using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoMvc.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace TodoMvc.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public IEnumerable<TodoItemModel> GetAll()
        {
            var result = _context.TodoItems.ToList();
            return result;
        }

        public TodoItemModel Get(int id)
        {
            var result = _context.TodoItems.Find(id);
            return result;
        }

        public int Create(TodoItemModel model)
        {
            var res = _context.TodoItems.Add(model);
            var result = _context.SaveChanges();
            return result;
        }

        public bool Complete(int id, bool status)
        {
            var item = _context.TodoItems.Find(id);
            if (item != null)
            {
                item.IsCompleted = status;
                _context.Entry(item).State = EntityState.Modified;
                var result = _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var item = _context.TodoItems.Find(id);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
                var result = _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Edit(int id, TodoItemModel model)
        {
            var item = _context.TodoItems.Find(id);
            if (item != null)
            {
                item.IsCompleted = model.IsCompleted;
                item.ItemName = model.ItemName;
                item.Priority = model.Priority;

                _context.Entry(item).State = EntityState.Modified;
                var result = _context.SaveChanges();
                return true;
            }
            return false;
        }


        // Async Methods
        public async Task<IList<TodoItemModel>> GetAllAsync()
        {
            var result = await _context.TodoItems.ToListAsync();
            return result;
        }

        public async Task<TodoItemModel> GetAsync(int id)
        {
            var result = await _context.TodoItems.FindAsync(id);
            return result;
        }

        public async Task<int> CreateAsync(TodoItemModel model)
        {
            var res = await _context.TodoItems.AddAsync(model);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<bool> EditAsync(int id, TodoItemModel model)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item != null)
            {
                item.IsCompleted = model.IsCompleted;
                item.ItemName = model.ItemName;
                item.Priority = model.Priority;

                _context.Entry(item).State = EntityState.Modified;
                var result = await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
                var result = await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> CompleteAsync(int id, bool status)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item != null)
            {
                item.IsCompleted = status;
                _context.Entry(item).State = EntityState.Modified;
                var result = await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
