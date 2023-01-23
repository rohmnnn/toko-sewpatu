using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TokoSepatuApp.Controller;
using TokoSepatuApp.Model.Entity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TokoSepatuApp.View.FormProducts
{
    public partial class FormAddProducts : Form
    {
        public delegate void CreateEventHandler(Products products);
        public event CreateEventHandler onCreate;

        private List<Brands> listOfBrands = new List<Brands>();

        public FormAddProducts()
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
            products.File = textPhoto.Text;


            onCreate(products);

            textName.Clear();
            textHarga.Clear();
            textPhoto.Clear();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Cari Foto",

                CheckFileExists = true,
                CheckPathExists = true,

                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp",
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textPhoto.Text = openFileDialog1.FileName;
                pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
            }
        }
    }
}
