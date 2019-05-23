using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knapsack
{
    [Table("details")]
    public class Details
    {
        [Key]
        public int DetailsId { get; set; }
        [Required]
        public int MaxWorth { get; set; }
        [Required]
        public int ExecutionTime { get; set; }
    }
}