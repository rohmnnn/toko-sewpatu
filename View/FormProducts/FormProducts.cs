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
            controller = new ProductsController();

            LoadProducts();
        }

        public void LoadProducts()
        {
            listOfUsers = controller.ReadAll();
            dataGridView.Rows.Clear();
            foreach (var product in listOfUsers)
            {
                if(!string.IsNullOrEmpty(product.Photo))
                {
                    string imgPath = Directory.GetCurrentDirectory() + product.Photo;
                    dataGridView.Rows.Add(product.Id, product.Name, product.Brand, new Bitmap(imgPath));
                }
                else
                {
                    string defaultImg = Directory.GetCurrentDirectory() + @"\Images\default.png";
                    dataGridView.Rows.Add(product.Id, product.Name, product.Brand, new Bitmap(defaultImg));
                }
            }

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
            dataGridView.SelectedRows.ToString();
            string id = "";
            foreach (DataGridViewRow row in dataGridView.SelectedRows)
            {
                id = row.Cells[0].Value.ToString();
            }

            controller.Update(users, id);
            LoadProducts();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = "";
            foreach (DataGridViewRow row in dataGridView.SelectedRows)
            {
                id = row.Cells[0].Value.ToString();
            }

            if (!string.IsNullOrEmpty(id))
            {
                var konfirmasi = MessageBox.Show("Data akan dihapus? ", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (konfirmasi == DialogResult.Yes)
                {
                    var result = controller.Delete(id);
                    if (result > 0) LoadProducts();
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
            string id = "";
            foreach (DataGridViewRow row in dataGridView.SelectedRows)
            {
                id = row.Cells[0].Value.ToString();
            }

            if (!string.IsNullOrEmpty(id))
            {
                Products users = listOfUsers[dataGridView.CurrentCell.RowIndex];

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
            listOfUsers = controller.Search(textCari.Text);
            dataGridView.Rows.Clear();
            foreach (var product in listOfUsers)
            {
                if (!string.IsNullOrEmpty(product.Photo))
                {
                    string imgPath = Directory.GetCurrentDirectory() + product.Photo;
                    dataGridView.Rows.Add(product.Id, product.Name, product.Brand, new Bitmap(imgPath));
                }
                else
                {
                    string defaultImg = Directory.GetCurrentDirectory() + @"\Images\default.png";
                    dataGridView.Rows.Add(product.Id, product.Name, product.Brand, new Bitmap(defaultImg));
                }
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }
    }
}
