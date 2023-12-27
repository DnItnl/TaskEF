using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEF.Models
{
    internal class Views
    {
        public class PrintOrdersView
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Status { get; set; }
            public string NeedDone { get; set; }
            public DateTime Date { get; set; }
            public bool Vip { get; set; }
            public decimal TotalAmount { get; set; }
        }

        public class OrderDetailsView
        {
            public int OrderId { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public int Sent { get; set; }
        }
        static public List<PrintOrdersView> GetPrintOrdersViewData()
        {
            List<PrintOrdersView> printOrdersViews = new List<PrintOrdersView>();

            using (var dbContext = new Context()) 
            {
                printOrdersViews = (from order in dbContext.Orders
                                    join customer in dbContext.Customers on order.CustomerId equals customer.Id
                                    select new PrintOrdersView
                                    {
                                        Id = order.Id,
                                        Name = customer.FirstName + " " + customer.LastName,
                                        Status = order.Status,
                                        NeedDone = order.need + "/" + order.done,
                                        Date = order.Date ?? DateTime.MinValue,
                                        Vip = customer.Vip,
                                        TotalAmount = order.TotalAmount
                                    }).ToList();
            }

            return printOrdersViews;
        }

        static public List<OrderDetailsView> GetOrderDetailsViewData()
        {
            List<OrderDetailsView> orderDetailsViews = new List<OrderDetailsView>();

            using (var dbContext = new Context())
            {
                orderDetailsViews = dbContext.OrderDetails.Select(od => new OrderDetailsView
                {
                    OrderId = od.OrderId,
                    ProductId = od.ProductId,
                    ProductName = dbContext.Products.Where(p => p.Id == od.ProductId).Select(p => p.Name).FirstOrDefault(),
                    Quantity = od.Quantity,
                    Price = od.Price,
                    Sent = od.Sent
                }).ToList();
            }

            return orderDetailsViews;
        }
    }
}
