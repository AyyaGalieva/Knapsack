using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knapsack
{
    [Table("execution_processes")]
    public class ExecutionProcess
    {
        [Key]
        public int ExecutionProcessId { get; set; }
        [Required]
        public int CurrentMaxWorth { get; set; }
        [Required]
        public string BestCombination { get; set; }
        [Required]
        public string CurrentItemsCombination { get; set; }
    }
}