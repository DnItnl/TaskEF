using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskEF.Models;
using TaskEF.Windows;

namespace TaskEF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            

        
        }
      




        private void NewProduct_Click(object sender, RoutedEventArgs e)
        {
            WNewProduct w = new WNewProduct();
            w.ShowDialog();
        }

        private void NewCustomer_Click(object sender, RoutedEventArgs e)
        {
            WNewCustomer w = new WNewCustomer();
            w.ShowDialog();
        }

        private void NewOrder_Click(object sender, RoutedEventArgs e)
        {
            WNewOrder w = new WNewOrder();
            w.ShowDialog();
        }

        private void NewReceived_Click(object sender, RoutedEventArgs e)
        {
            WNewReceived w = new WNewReceived();
            w.ShowDialog();
        }

        private void Sending_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Context())
            {
                var query = context.OrderDetails
                    .Where(o => o.Sent < o.Quantity)
                    .OrderByDescending(o => o.Order.Customer.Vip)
                    .ThenBy(o => o.Order.Date)
                    .Select(o => o.ProductId)
                    .ToList();
                List<int> listProductId = query;
                foreach (var item in listProductId)
                    Send.Call(item);
            }

           
        }

        private void PProducts_Click(object sender, RoutedEventArgs e)
        {
            using(var context = new Context())
            {
                List<Product> list = context.Products.ToList();
                dataGrid.ItemsSource = list;
            }
        }

        private void PCustomers_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Context())
            {
                List<Customer> list = context.Customers.ToList();
                dataGrid.ItemsSource = list;
            }
        }

        private void POrders_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Context())
            {
                dataGrid.ItemsSource = Views.GetPrintOrdersViewData();
            }
        }

        private void POrderD_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Context())
            {
                dataGrid.ItemsSource = Views.GetOrderDetailsViewData();
            }
        }
    }
}