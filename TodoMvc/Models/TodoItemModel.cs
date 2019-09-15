using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoMvc.Models
{
    public class TodoItemModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        [Required]
        public string ItemName { get; set; }
        public int Priority { get; set; }
    }
}