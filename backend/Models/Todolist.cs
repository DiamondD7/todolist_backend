using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Todolist
    {
        [Key]
        public int TableId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
