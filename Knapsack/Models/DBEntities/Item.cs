using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knapsack
{
    [Table("items")]
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        [Required, MaxLength(50)]
        public string ItemName { get; set; }
        [Required]
        public int Worth { get; set; }
        [Required]
        public int Weight { get; set; }
        public List<TaskItem> TaskItems { get; set; }
     
        public Item()
        {
            TaskItems = new List<TaskItem>();
        }
    }
}