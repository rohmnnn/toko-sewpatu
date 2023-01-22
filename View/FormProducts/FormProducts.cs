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

namespace TokoSepatuApp.View.FormProducts
{
    public partial class FormProducts : Form
    {
        private List<Products> listOfUsers = new List<Products>();
        private ProductsController controller;
        public FormProducts()
        {
            InitializeComponent();
            InisialisasiListView();
            controller = new ProductsController();

            LoadProducts();
        }

        public void LoadProducts()
        {
            listView.Items.Clear();
            listOfUsers = controller.ReadAll();

            foreach (var product in listOfUsers)
            {
                var noUrut = listView.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());

                if(!string.IsNullOrEmpty(product.Photo))
                {
                    ImageList images = new ImageList();
                    images.Images.Add(product.Id.ToString(), Image.FromFile(product.Photo.ToString()));
                    listView.LargeImageList = images;
                }

                item.Tag = product.Id;
                item.SubItems.Add(product.Name);
                item.SubItems.Add(product.Brand);
                item.SubItems.Add(product.Price.ToString("C", CultureInfo.CurrentCulture));
                item.ImageKey = product.Id.ToString();
                item.ImageIndex = noUrut - 1;

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
            listView.Columns.Add("Brand", 120, HorizontalAlignment.Left);
            listView.Columns.Add("Price", 120, HorizontalAlignment.Left);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var formAddProduct = new FormAddProducts();
            formAddProduct.onCreate += onCreate;

            formAddProduct.ShowDialog();
        }

        private void onCreate(Products users)
        {
            controller.Create(users);
            LoadProducts();
        }

        private void onUpdate(Products users)
        {
            string id = listView.SelectedItems[0].Tag.ToString();

            controller.Update(users, id);
            LoadProducts();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                var konfirmasi = MessageBox.Show("Data akan dihapus? ", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (konfirmasi == DialogResult.Yes)
                {
                    string id = listView.SelectedItems[0].Tag.ToString();
                    var result = controller.Delete(id);
                    if (result > 0) LoadProducts();
                }
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data tidak ditemukan!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                Products users = listOfUsers[listView.SelectedIndices[0]];

                var formUpdateProduct = new FormUpdateProducts(users);
                formUpdateProduct.onUpdate += onUpdate;

                formUpdateProduct.ShowDialog();
            }
            else
            {
                MessageBox.Show("Data tidak ditemukan!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            listView.Items.Clear();
            listOfUsers = controller.Search(textCari.Text);

            foreach (var product in listOfUsers)
            {
                var noUrut = listView.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());

                item.Tag = product.Id;
                item.SubItems.Add(product.Name);
                item.SubItems.Add(product.Brand);
                item.SubItems.Add(String.Format(CultureInfo.CreateSpecificCulture("id-id"), "{0:N}", product.Price.ToString()));

                listView.Items.Add(item);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }
    }
}
