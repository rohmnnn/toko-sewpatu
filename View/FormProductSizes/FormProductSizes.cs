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

namespace TokoSepatuApp.View.FormProductSizes
{
    public partial class FormProductSizes : Form
    {
        private List<ProductSizes> listOfProductSizes = new List<ProductSizes>();
        private ProductSizesController controller;
        public FormProductSizes()
        {
            InitializeComponent();
            InisialisasiListView();
            controller = new ProductSizesController();

            LoadProductSizes();
        }

        public void LoadProductSizes()
        {
            listView.Items.Clear();
            listOfProductSizes = controller.ReadAll();

            foreach (var productSize in listOfProductSizes)
            {
                var noUrut = listView.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());

                item.Tag = productSize.Id;
                item.SubItems.Add(productSize.Product.ToString());
                item.SubItems.Add(productSize.Size.ToString());
                item.SubItems.Add(productSize.Stock.ToString());
                item.SubItems.Add(productSize.IsAvaliable.ToString() == "1" ? "Tersedia" : "Tidak Tersedia");

                listView.Items.Add(item);
            }
        }

        private void InisialisasiListView()
        {
            listView.View = System.Windows.Forms.View.Details;
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.Columns.Add("No.", 35, HorizontalAlignment.Center);
            listView.Columns.Add("Produk", 250, HorizontalAlignment.Left);
            listView.Columns.Add("Size", 150, HorizontalAlignment.Left);
            listView.Columns.Add("Stock", 150, HorizontalAlignment.Left);
            listView.Columns.Add("Available", 150, HorizontalAlignment.Left);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var formAddproductSize = new FormAddUpdateProductSizes();
            formAddproductSize.onCreate += onCreate;

            formAddproductSize.ShowDialog();
        }

        private void onCreate(ProductSizes productSizes)
        {
            controller.Create(productSizes);
            LoadProductSizes();
        }

        private void onUpdate(ProductSizes productSizes)
        {
            int id = Convert.ToInt32(listView.SelectedItems[0].Tag.ToString());

            controller.Update(productSizes, id);
            LoadProductSizes();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                var konfirmasi = MessageBox.Show("Data akan dihapus? ", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (konfirmasi == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(listView.SelectedItems[0].Tag.ToString());
                    var result = controller.Delete(id);
                    if (result > 0) LoadProductSizes();
                }
            }
            else
            {
                MessageBox.Show("Data tidak ditemukan!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                ProductSizes productSizes = listOfProductSizes[listView.SelectedIndices[0]];

                var formUpdateProductSize = new FormAddUpdateProductSizes(productSizes);
                formUpdateProductSize.onUpdate += onUpdate;

                formUpdateProductSize.ShowDialog();
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
            listOfProductSizes = controller.Search(textCari.Text);

            foreach (var productSize in listOfProductSizes)
            {
                var noUrut = listView.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());

                item.Tag = productSize.Id;
                item.SubItems.Add(productSize.Product.ToString());
                item.SubItems.Add(productSize.Size.ToString());
                item.SubItems.Add(productSize.IsAvaliable.ToString());
                item.SubItems.Add(productSize.Stock.ToString());

                listView.Items.Add(item);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            LoadProductSizes();
        }
    }
}
