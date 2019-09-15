using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoMvc.Models;

namespace TodoMvc.Repository
{
    public interface ITodoRepository
    {
        IEnumerable<TodoItemModel> GetAll();
        TodoItemModel Get(int id);
        int Create(TodoItemModel model);
        bool Edit(int id, TodoItemModel model);
        bool Delete(int id);
        bool Complete(int id, bool status);

        Task<IList<TodoItemModel>> GetAllAsync();
        Task<TodoItemModel> GetAsync(int id);
        Task<int> CreateAsync(TodoItemModel model);
        Task<bool> EditAsync(int id, TodoItemModel model);
        Task<bool> DeleteAsync(int id);
        Task<bool> CompleteAsync(int id, bool status);
    }
}
