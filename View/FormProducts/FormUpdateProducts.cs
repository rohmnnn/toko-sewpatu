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
        bool imageChange = false;

        public FormUpdateProducts()
        {
            BrandsController controller = new BrandsController();
            InitializeComponent();

            comboBoxBrand.Items.Clear();
            comboBoxBrand.DisplayMember = "Text";
            comboBoxBrand.ValueMember = "Value";

            listOfBrands = controller.ReadAll();
            List<Object> items = new List<Object>();

            foreach (var brand in listOfBrands)
            {
                items.Add(new { Text = brand.Name, Value = brand.Id });
            }

            comboBoxBrand.DataSource = items;
        }

        public FormUpdateProducts(Products products) : this()
        {
            comboBoxBrand.Text = products.Brand;
            textName.Text = products.Name;
            textHarga.Text = products.Price.ToString();
            textPhoto.Text = products.Photo;
            if (!string.IsNullOrEmpty(products.Photo))
            {
                pictureBox.Image = new Bitmap(products.Photo);
            }
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
            products.BrandId = Convert.ToInt32(comboBoxBrand.SelectedValue.ToString());

            if (imageChange)
            {
                products.File = textPhoto.Text;
            }

            onUpdate(products);

            textName.Clear();
            textHarga.Clear();
            textPhoto.Clear();

            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Cari Foto",

                CheckFileExists = true,
                CheckPathExists = true,

                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp",
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imageChange = true;
                textPhoto.Text = openFileDialog.FileName;
                pictureBox.Image = new Bitmap(openFileDialog.FileName);
            }
        }
    }
}
