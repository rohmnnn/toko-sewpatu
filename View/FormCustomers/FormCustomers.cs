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

namespace TokoSepatuApp.View.FormCustomers
{
    public partial class FormCustomers : Form
    {
        private List<Customers> listOfCustomers = new List<Customers>();
        private CustomersController controller;
        public FormCustomers()
        {
            InitializeComponent();
            InisialisasiListView();
            controller = new CustomersController();

            LoadCustomers();
        }

        public void LoadCustomers()
        {
            listView.Items.Clear();
            listOfCustomers = controller.ReadAll();

            foreach (var customers in listOfCustomers)
            {
                var noUrut = listView.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());

                item.Tag = customers.Id;
                item.SubItems.Add(customers.Name);
                item.SubItems.Add(customers.Address);

                listView.Items.Add(item);
            }
        }

        private void InisialisasiListView()
        {
            listView.View = System.Windows.Forms.View.Details;
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.Columns.Add("No.", 35, HorizontalAlignment.Center);
            listView.Columns.Add("Nama", 250, HorizontalAlignment.Left);
            listView.Columns.Add("Alamat", 499, HorizontalAlignment.Left);
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            listView.Items.Clear();
            listOfCustomers = controller.Search(textCari.Text);

            foreach (var customers in listOfCustomers)
            {
                var noUrut = listView.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());

                item.Tag = customers.Id;
                item.SubItems.Add(customers.Name);
                item.SubItems.Add(customers.Address);

                listView.Items.Add(item);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            LoadCustomers();
        }
    }
}
