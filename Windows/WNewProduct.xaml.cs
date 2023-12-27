using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
using System.Xml.Linq;
using TaskEF.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TaskEF.Windows
{
    /// <summary>
    /// Логика взаимодействия для WNewProduct.xaml
    /// </summary>
    public partial class WNewProduct : Window
    {
        public WNewProduct()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(price.Text, out decimal number))
            {
                CallProduct call = new CallProduct();
                var p = new Product()
                {
                    Name = name.Text,
                    Price = number,
                    StockQuantity=0
                };

                call.Insert(p);                
                this.Close();
            }
        }
    }
   
}
