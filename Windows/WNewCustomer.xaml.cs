using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для WNewCustomer.xaml
    /// </summary>
    public partial class WNewCustomer : Window
    {
        public WNewCustomer()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Regex remail = new Regex(@"^[-\w.]+@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,4}$");
            Regex rphone = new Regex(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$");

            if ((remail.IsMatch(Email.Text) || Email.Text=="") && (rphone.IsMatch(Phone.Text) || Phone.Text == "") && fname.Text!="" && lname.Text!="")
            {
                CallCustomer call = new CallCustomer();
                Customer customer = new Customer()
                {
                    FirstName = fname.Text,
                    LastName = lname.Text,
                    Email = Email.Text,
                    Phone = Phone.Text,
                    Address = Address.Text,
                    Vip = vip.IsChecked ?? false
                };
                call.Insert(customer);
                this.Close();
            }
        }
    }
}
