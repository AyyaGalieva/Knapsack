using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knapsack
{
    [Table("details")]
    public class Details
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("details_id")]
        public int details_id { get; set; }
        [Required]
        public Decimal max_Worth { get; set; }
        [Required]
        public int execution_time { get; set; }
    }
}