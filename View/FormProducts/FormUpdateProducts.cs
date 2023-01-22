using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TokoSepatuApp.Controller;
using TokoSepatuApp.Model.Entity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TokoSepatuApp.View.FormProducts
{
    public partial class FormUpdateProducts : Form
    {
        public delegate void UpdateEventHandler(Products products);
        public event UpdateEventHandler onUpdate;

        private List<Brands> listOfBrands = new List<Brands>();
        private BrandsController controller;

        public FormUpdateProducts()
        {
            InitializeComponent();
            comboBoxBrand.Items.Clear();
            comboBoxBrand.SelectedIndex = 0;
            
            listOfBrands = controller.ReadAll();

            foreach (var brand in listOfBrands)
            {
                var noUrut = comboBoxBrand.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());

                item.Tag = brand.Id;
                item.SubItems.Add(brand.Name);

                comboBoxBrand.Items.Add(item);
            }
        }

        public FormUpdateProducts(Products products) : this()
        {
            InitializeComponent();
            comboBoxBrand.Items.Clear();
            comboBoxBrand.SelectedIndex = 0;

            listOfBrands = controller.ReadAll();

            foreach (var brand in listOfBrands)
            {
                var noUrut = comboBoxBrand.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());

                item.Tag = brand.Id;
                item.SubItems.Add(brand.Name);

                comboBoxBrand.Items.Add(item);
            }

            textName.Text = products.Name;
            textHarga.Text = products.Price.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var products = new Products();

            products.Name = textName.Text;
            products.Price = Convert.ToInt32(textHarga.Text);
            products.BrandId = comboBoxBrand.SelectedIndex;
            products.Photo = textPhoto.Text;

            

            onUpdate(products);

            textName.Clear();
            textHarga.Clear();
            textPhoto.Clear();
        }
    }
}
