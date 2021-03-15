using Restaurant.Models;
using System;
using System.Windows;

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Server server = new Server();
        public MainWindow()
        {
            InitializeComponent();
            drinks.Items.Add(Drinks.Tea);
            drinks.Items.Add(Drinks.Juice);
            drinks.Items.Add(Drinks.RC_Cola);
            drinks.Items.Add(Drinks.Coca_Cola);           
        }
        private void getOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var quantityChicken = int.Parse(chickenQuantity.Text);

                if (quantityChicken < 0)
                {
                    throw new Exception("Quantity can't to be less 0!");
                }

                var quantityEgg = int.Parse(eggQuantity.Text);

                if (quantityEgg < 0)
                {
                    throw new Exception("Quantity can't to be less 0!");
                }

                var drink = drinks.SelectedItem;
                var customer = customerName.Text;

                if (customer.Length == 0)
                {
                    throw new Exception("customerName Error!");
                }

                server.Receive(customer, quantityChicken, quantityEgg, drink);
            }
            catch (Exception ex)
            {
                Results.Items.Add(ex.Message);
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var resultOfCooks = server.SendToCook();
                foreach (var result in resultOfCooks)
                {
                    Results.Items.Add(result);
                }
                Results.Items.Add("Please enjoy your food!");
            }
            catch (Exception ex)
            {
                Results.Items.Add(ex.Message);
            }
            server.Clear();
        }
    }
}
