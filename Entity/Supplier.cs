using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PurchaseManagement.Entity
{
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(15)]
        public string Tel { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }
    }
}
