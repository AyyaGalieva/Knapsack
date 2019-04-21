using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knapsack
{
    [Table("execution_processes")]
    public class ExecutionProcess
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("execution_process_id")]
        public int execution_process_id { get; set; }
        [Required]
        public int current_max_worth { get; set; }
        [Required]
        public int current_items_combination { get; set; }
    }
}