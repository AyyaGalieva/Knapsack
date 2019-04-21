using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knapsack
{
    [Table("users")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public int user_id { get; set; }
        [Required, MaxLength(50)]
        public string name { get; set; }
        [Required, MaxLength(50)]
        public string pswd_hash { get; set; }
    }
}