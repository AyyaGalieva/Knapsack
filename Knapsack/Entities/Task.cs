using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knapsack
{
    public class Task
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("task_id")]
        public int task_id { get; set; }
        [Required, MaxLength(50)]
        public string name { get; set; }
        [Required, MaxLength(50)]
        public string status { get; set; }
        [Required]
        public Decimal capacity { get; set; }
        [Required]
        public Decimal percent_complete { get; set; }
        public int details_id { get; set; }
        [ForeignKey("details_id")]
        public Details details { get; set; }
        public int user_id { get; set; }
        [ForeignKey("user_id")]
        public User user { get; set; }
        public int execution_process_id { get; set; }
        [ForeignKey("execution_process_id")]
        public ExecutionProcess executionProcess { get; set; }
    }
}