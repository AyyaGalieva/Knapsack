using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knapsack
{
    public class TaskItem
    {
        public int task_id { get; set; }
        [ForeignKey("task_id")]
        public Task task { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")] 
        public Item item { get; set; }
        [Required]
        public bool isTaken { get; set; }
    }
}