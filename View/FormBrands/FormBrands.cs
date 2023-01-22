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

namespace TokoSepatuApp.View.FormBrands
{
    public partial class FormBrands : Form
    {
        private List<Brands> listOfBrands = new List<Brands>();
        private BrandsController controller;
        public FormBrands()
        {
            InitializeComponent();
            InisialisasiListView();
            controller = new BrandsController();

            LoadBrands();
        }

        public void LoadBrands()
        {
            listView.Items.Clear();
            listOfBrands = controller.ReadAll();

            foreach (var brand in listOfBrands)
            {
                var noUrut = listView.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());

                item.Tag = brand.Id;
                item.SubItems.Add(brand.Name);

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
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var formAddbrand = new FormAddUpdateBrands();
            formAddbrand.onCreate += onCreate;

            formAddbrand.ShowDialog();
        }

        private void onCreate(Brands brands)
        {
            controller.Create(brands);
            LoadBrands();
        }

        private void onUpdate(Brands brands)
        {
            int id = Convert.ToInt32(listView.SelectedItems[0].Tag.ToString());

            controller.Update(brands, id);
            LoadBrands();
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
                    if (result > 0) LoadBrands();
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
                Brands brands = listOfBrands[listView.SelectedIndices[0]];

                var formUpdateBrand = new FormAddUpdateBrands(brands);
                formUpdateBrand.onUpdate += onUpdate;

                formUpdateBrand.ShowDialog();
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
            listOfBrands = controller.Search(textCari.Text);

            foreach (var brand in listOfBrands)
            {
                var noUrut = listView.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());

                item.Tag = brand.Id;
                item.SubItems.Add(brand.Name);

                listView.Items.Add(item);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            LoadBrands();
        }
    }
}
