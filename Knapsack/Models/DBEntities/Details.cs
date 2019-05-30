using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knapsack
{
    [Table("details")]
    public class Details
    {
        [Key]
        [ForeignKey("Task")]
        public int TaskId { get; set; }
        public Task Task { get; set; }
        [Required]
        public int MaxWorth { get; set; }
        [Required]
        public string ExecutionTime { get; set; }
    }
}