using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TokoSepatuApp.View.FormBrands;
using TokoSepatuApp.View.FormCustomers;
using TokoSepatuApp.View.FormOrders;
using TokoSepatuApp.View.FormProducts;
using TokoSepatuApp.View.FormUsers;

namespace TokoSepatuApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var formUser = new FormUsers();
            formUser.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var formCustomers = new FormCustomers();
            formCustomers.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var formProducts = new FormProducts();
            formProducts.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var formBrand = new FormBrands();
            formBrand.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var formOrders = new FormOrders();
            formOrders.ShowDialog();
        }
    }
}
