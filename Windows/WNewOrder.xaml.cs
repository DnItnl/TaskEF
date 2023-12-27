using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskEF.Models;

namespace TaskEF.Windows
{
    /// <summary>
    /// Логика взаимодействия для WNewOrder.xaml
    /// </summary>
    public partial class WNewOrder : Window
    {
        Dictionary<int, int> cart = new Dictionary<int, int>();
        //cart 
        public WNewOrder()
        {
            InitializeComponent();
            CallCustomer callcustomers = new CallCustomer();
            CBCustomers.ItemsSource = callcustomers.SelectName();
            CBCustomers.DisplayMemberPath = "Value";
            CBCustomers.SelectedValuePath = "Key";


            CallProduct callproducts = new CallProduct();
            CBProducts.ItemsSource = callproducts.SelectName();
            CBProducts.DisplayMemberPath = "Value";
            CBProducts.SelectedValuePath = "Key";


        }

      
        private void Add_Click_1(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(Count.Text, out int number) && CBProducts.SelectedItem != null && number>0)
                MyLV.Items.Add(Cart(number));
        }
        private Grid Cart(int number)
        {
            KeyValuePair<int, string> selectedProduct = (KeyValuePair<int, string>)CBProducts.SelectedItem;
            if (cart.ContainsKey(selectedProduct.Key))
            {
                cart[selectedProduct.Key] += number;

                Label existingLabel = MyLV.FindName($"l{selectedProduct.Key}") as Label;
                

                if (existingLabel != null)
                {
                    existingLabel.Content = $"{selectedProduct.Value} - {cart[selectedProduct.Key]}";
                    return null;
                }
            }
            else
            {
                cart.Add(selectedProduct.Key, number);
            }
            Grid grid = new Grid();
            //grid.Name= $"g{selectedProduct.Key}";
            grid.Width = 375;
            grid.Background = new SolidColorBrush(Colors.SeaShell);
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            Label label = new Label();
            label.Name= $"l{selectedProduct.Key}";
            label.Content = $"{selectedProduct.Value} - {cart[selectedProduct.Key]}";
            grid.Children.Add(label);

            Button btn = new Button { HorizontalAlignment = HorizontalAlignment.Right, Content = "remove" };
            btn.Click += delete;
            grid.Children.Add(btn);
            btn.Name = $"_{selectedProduct.Key}";
            return grid;



        }
        private void delete(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                string buttonName = btn.Name.TrimStart('_');
                
                cart.Remove(Convert.ToInt32( buttonName));


                Grid grid = btn.Parent as Grid;
                if (grid != null)
                {
                    MyLV.Items.Remove(grid);
                }
            }
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            if (cart.Count == 0 || CBCustomers.SelectedItem == null)
                return;

            Context context = new Context();


                var newOrder = new Order
                {
                    CustomerId = Convert.ToInt32(CBCustomers.SelectedValue),
                    Date = DateTime.Now,
                    TotalAmount = 0, 
                    Status = 1,
                    need=0,
                    done=0
                };

                context.Orders.Add(newOrder);
                context.SaveChanges();

                List<OrderDetail> orderDetails = new List<OrderDetail>();
                decimal total = 0;
                
                foreach (var key in cart.Keys)
                {
                var product = context.Products.FirstOrDefault(p => p.Id == key);
                var totaltmp = product.Price * cart[key];
                orderDetails.Add(new OrderDetail
                {
                    OrderId = newOrder.Id,
                    ProductId = key,
                    Quantity = cart[key],
                    Price = product.Price* cart[key],
                    Sent = 0
                });
                newOrder.need++;
                total += totaltmp;
                }

                context.OrderDetails.AddRange(orderDetails);

                var orderToUpdate = context.Orders.FirstOrDefault(o => o.Id == newOrder.Id);
                orderToUpdate.TotalAmount = total;

                context.SaveChanges();


            if (cb.IsChecked.Value)
            {
                foreach (var key in cart.Keys) 
                {
                    Send.Call(key);
                }
            }


            this.Close();
        }
    }
}
