using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knapsack
{
    [Table("tasks")]
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        [Required, MaxLength(50)]
        public string TaskName { get; set; }
        [Required, MaxLength(50)]
        public string Status { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public int PercentComplete { get; set; }
        public int DetailsId { get; set; }
        [ForeignKey("DetailsId")]
        public Details Details { get; set; }
        public int ExecutionProcessId { get; set; }
        [ForeignKey("ExecutionProcessId")]
        public ExecutionProcess ExecutionProcess { get; set; }
        public List<TaskItem> TaskItems { get; set; }
     
        public Task()
        {
            TaskItems = new List<TaskItem>();
        }
    }
}