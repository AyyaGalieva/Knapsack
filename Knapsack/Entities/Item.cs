using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knapsack
{
    [Table("items")]
    public class Item
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("item_id")]
        public int item_id { get; set; }
        [Required, MaxLength(50)]
        public string name { get; set; }
        [Required]
        public decimal worth { get; set; }
        [Required]
        public decimal weight { get; set; }
    }
}