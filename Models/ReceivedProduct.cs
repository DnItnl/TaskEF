using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEF.Models
{
    [Table("ReceivedProducts")]
    public class ReceivedProduct
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public DateTime? Date { get; set; }

        [Required]
        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
