using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для WNewReceived.xaml
    /// </summary>
    public partial class WNewReceived : Window
    {
        public WNewReceived()
        {
            InitializeComponent();
            CallProduct callproducts = new CallProduct();
            CBProducts.ItemsSource = callproducts.SelectName();
            CBProducts.DisplayMemberPath = "Value";
            CBProducts.SelectedValuePath = "Key";
        }

        private void Add_Click_1(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(Count.Text, out int number) && CBProducts.SelectedItem != null)
            {
                Context context = new Context();
                int pid = Convert.ToInt32(CBProducts.SelectedValue);
                ReceivedProduct receivedProduct = new ReceivedProduct
                {
                    ProductId = pid,
                    Date = DateTime.Now,
                    Quantity = number
                };
                context.Add(receivedProduct);
                var productToUpdate = context.Products.FirstOrDefault(p => p.Id == pid);
                productToUpdate.StockQuantity += number;
                context.SaveChanges();
                if (cb.IsChecked.Value)
                {
                    Send.Call(pid);
                }
               

                this.Close();
            }
        }
    }
}
