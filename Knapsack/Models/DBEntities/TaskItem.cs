using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knapsack
{
    [Table("taskItem")]
    public class TaskItem
    {
        public int TaskId { get; set; }
        public Task Task { get; set; }
        
        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}