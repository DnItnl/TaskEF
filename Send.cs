using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEF.Models;

namespace TaskEF
{
    internal class Send
    {
        //неоходимо добавить статус и order by статус
        public static void Call(int product_id)
        {
            using (var context = new Context())
            {
                ////wrong
                var orderDetails = context.OrderDetails
                    .Where(o => o.ProductId == product_id && o.Sent < o.Quantity)
                    .Join(context.Orders, orderDetail => orderDetail.OrderId, order => order.Id, (orderDetail, order) => new { OrderDetail = orderDetail, Order = order })
                    .Join(
                        context.Customers, combined => combined.Order.CustomerId, customer => customer.Id, 
                        (combined, customer) => new { OrderDetail = combined.OrderDetail, Order = combined.Order, Customer = customer })
                    .OrderByDescending(c => c.Customer.Vip) 
                    .ThenBy(o => o.Order.Date) 
                    .Select(o => o.OrderDetail)
                    .ToList();


                Product product = context.Products.Where(p => p.Id == product_id).FirstOrDefault();

                int stockQuantity = product.StockQuantity;
                if (stockQuantity < 1)
                    return;

                foreach (var orderDetail in orderDetails)
                {
                    Order order = context.Orders.FirstOrDefault(o => o.Id == orderDetail.OrderId);
                    if (order.Status != 1)
                        continue;




                    int remainingQuantity = orderDetail.Quantity - orderDetail.Sent;
                    int quantityToSend = Math.Min(remainingQuantity, stockQuantity);
                    if (quantityToSend > stockQuantity)
                    {
                        quantityToSend = stockQuantity;
                    }

                    orderDetail.Sent += quantityToSend;
                    stockQuantity -= quantityToSend;
                    if (orderDetail.Sent==orderDetail.Quantity)
                        order.done++;
                    if (stockQuantity < 0)
                    {
                        orderDetail.Sent -= Math.Abs(stockQuantity);
                        stockQuantity = 0;
                        order.done--;
                        break;
                    }
                    if (order.done == order.need)
                        order.Status = 2;





                    if (stockQuantity == 0)
                    {
                        break;
                    }


                    
                }


                //foreach (var orderDetail in orderDetails)
                //{
                //    int remainingQuantity = orderDetail.Quantity - orderDetail.Sent; 
                //    int quantityToSend = Math.Min(remainingQuantity, stockQuantity);

                //    if (quantityToSend > 0)
                //    {
                //        int updatedStockQuantity = stockQuantity - quantityToSend;
                //        if (updatedStockQuantity >= 0)
                //        {
                //            orderDetail.Sent += quantityToSend;
                //            stockQuantity = updatedStockQuantity; 
                //        }
                //        else
                //        {    
                //            quantityToSend = stockQuantity;
                //            orderDetail.Sent += quantityToSend;
                //            stockQuantity = 0; 
                //        }
                //    }
                //    else
                //    {
                //        break; 
                //    }
                //}













                product.StockQuantity = stockQuantity;
                context.SaveChanges(); 

            }


        }
    }
}
