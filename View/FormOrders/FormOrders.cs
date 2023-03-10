using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TokoSepatuApp.Model.Entity;
using TokoSepatuApp.Controller;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Globalization;
using System.IO;

namespace TokoSepatuApp.View.FormOrders
{
    public partial class FormOrders : Form
    {
        private List<Orders> listOfUsers = new List<Orders>();
        private OrdersController controller;
        public FormOrders()
        {
            InitializeComponent();
            InisialisasiListView();
            controller = new OrdersController();

            LoadOrders();
        }

        public void LoadOrders()
        {
            listView.Items.Clear();
            listOfUsers = controller.ReadAll();

            foreach (var order in listOfUsers)
            {
                var noUrut = listView.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());

                item.Tag = order.Id;
                item.SubItems.Add(order.OrderNo);
                item.SubItems.Add(order.Date.ToString());
                item.SubItems.Add(order.Total.ToString("C", CultureInfo.CurrentCulture));
                listView.Items.Add(item);
            }

        }

        private void InisialisasiListView()
        {
            listView.View = System.Windows.Forms.View.Details;
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.Columns.Add("No.", 35, HorizontalAlignment.Center);
            listView.Columns.Add("No Order", 300, HorizontalAlignment.Left);
            listView.Columns.Add("Tanggal", 280, HorizontalAlignment.Left);
            listView.Columns.Add("Total", 120, HorizontalAlignment.Left);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var formAddOrder = new FormAddOrders();
            formAddOrder.onCreate += onCreate;

            formAddOrder.ShowDialog();
        }

        private void onCreate(Orders orders, OrderDetails orderDetails)
        {
            controller.Create(orders, orderDetails);
            LoadOrders();
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            listView.Items.Clear();
            listOfUsers = controller.Search(textCari.Text);

            foreach (var order in listOfUsers)
            {
                var noUrut = listView.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());

                item.Tag = order.Id;
                item.SubItems.Add(order.OrderNo);
                item.SubItems.Add(order.Date.ToString());
                item.SubItems.Add(order.Total.ToString("C", CultureInfo.CurrentCulture));

                listView.Items.Add(item);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            LoadOrders();
        }
    }
}
