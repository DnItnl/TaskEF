using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.ConstrainedExecution;
using Microsoft.EntityFrameworkCore;

namespace TaskEF.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }
    }
    public class CallProduct
    {
        Context context;
        public CallProduct()
        {
            context = new Context();
        }
        public string Insert(Product model)
        {
            context.Products.AddRange(model);
            context.SaveChanges();
            return "ok";
        }
        public Dictionary<int, string> SelectName()
        {
            Context db = new Context();
            db.Products.Load();
            var query = db.Products.Local
           .ToDictionary(customer => customer.Id, customer => customer.Name);
            return query;
        }
    }
}
