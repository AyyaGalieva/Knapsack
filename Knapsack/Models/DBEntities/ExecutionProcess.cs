using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knapsack
{
    [Table("execution_processes")]
    public class ExecutionProcess
    {
        [Key]
        [ForeignKey("Task")]
        public int TaskId { get; set; }
        public Task Task { get; set; }
        [Required]
        public int CurrentMaxWorth { get; set; }
        [Required]
        public string BestCombination { get; set; }
        [Required]
        public string CurrentItemsCombination { get; set; }
        public int CurCombSize { get; set; }
        public int CurCombEnd { get; set; }
        public int CheckedCombCount { get; set; }
        [Required]
        public string AllItems { get; set; }
    }
}