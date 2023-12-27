using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;

namespace TaskEF.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        public bool Vip { get; set; }
    }
    public class CallCustomer
    {
        Context context;
        public CallCustomer()
        {
            context = new Context();
        }
        public void Insert(Customer model)
        {
            context.Customers.AddRange(model);
            context.SaveChanges();
        }

        public List<Customer> Select()
        {
            Context db = new Context();
            db.Customers.Load();
            var query = db.Customers.Local.ToBindingList();
            return query.ToList();
        }
        public Dictionary<int,string> SelectName()
        {
            Context db = new Context();
            db.Customers.Load();
            var query = db.Customers.Local
           .ToDictionary(customer => customer.Id, customer => $"{customer.FirstName} {customer.LastName}");
            return query;
        }
    }
}
