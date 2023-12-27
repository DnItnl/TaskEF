using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEF.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime? Date { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; }

        public int done { get; set; }
        public int need { get; set; }

        public int Status { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }

   
    
}
