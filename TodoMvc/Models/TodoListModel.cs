using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoMvc.Models
{
    public class TodoListModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public List<TodoItemModel> Items { get; set; }
        public string ListName { get; set; }
    }
}
