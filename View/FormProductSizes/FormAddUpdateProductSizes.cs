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

namespace TokoSepatuApp.View.FormProductSizes
{
    public partial class FormAddUpdateProductSizes : Form
    {
        public delegate void CreateUpdateEventHandler(ProductSizes ProductSizes);
        public event CreateUpdateEventHandler onCreate;
        public event CreateUpdateEventHandler onUpdate;

        private bool isUpdate = false;
        private List<Products> listOfProducts = new List<Products>();
        public FormAddUpdateProductSizes()
        {
            ProductsController productController = new ProductsController();
            InitializeComponent();

            // Product
            comboBoxProduct.Items.Clear();
            comboBoxProduct.DisplayMember = "Text";
            comboBoxProduct.ValueMember = "Value";

            listOfProducts = productController.ReadAll();
            List<Object> itemsProduct = new List<Object>();

            foreach (var product in listOfProducts)
            {
                itemsProduct.Add(new { Text = product.Name, Value = product.Id });
            }

            comboBoxProduct.DataSource = itemsProduct;

            // Available
            comboBoxAvailable.Items.Clear();
            comboBoxAvailable.DisplayMember = "Text";
            comboBoxAvailable.ValueMember = "Value";

            List<Object> itemsAvailable = new List<Object>();

            itemsAvailable.Add(new { Text = "Tersedia", Value = 1 });
            itemsAvailable.Add(new { Text = "Tidak Tersedia", Value = 0 });

            comboBoxAvailable.DataSource = itemsAvailable;

            // Size
            comboBoxSize.Items.Clear();
            comboBoxSize.DisplayMember = "Text";
            comboBoxSize.ValueMember = "Value";

            List<Object> itemsSize = new List<Object>();

            itemsSize.Add(new { Text = "36", Value = 36 });
            itemsSize.Add(new { Text = "37", Value = 37 });
            itemsSize.Add(new { Text = "38", Value = 38 });
            itemsSize.Add(new { Text = "39", Value = 39 });
            itemsSize.Add(new { Text = "40", Value = 40 });
            itemsSize.Add(new { Text = "41", Value = 41 });
            itemsSize.Add(new { Text = "42", Value = 42 });
            itemsSize.Add(new { Text = "43", Value = 43 });

            comboBoxSize.DataSource = itemsSize;
        }

        public FormAddUpdateProductSizes(ProductSizes ProductSizes) : this()
        {
            isUpdate = true;
            textStock.Text = ProductSizes.Stock.ToString();
            comboBoxProduct.Text = ProductSizes.ProductId.ToString();
            comboBoxSize.Text = ProductSizes.Size.ToString();
            comboBoxAvailable.Text = ProductSizes.IsAvaliable.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isUpdate)
            {
                var ProductSizes = new ProductSizes();
                ProductSizes.ProductId = Convert.ToInt32(comboBoxProduct.SelectedValue.ToString());
                ProductSizes.Size = Convert.ToInt32(comboBoxSize.SelectedValue.ToString());
                ProductSizes.IsAvaliable = Convert.ToInt32(comboBoxAvailable.SelectedValue.ToString());
                ProductSizes.Stock = Convert.ToInt32(textStock.Text);

                onUpdate(ProductSizes);
            } else
            {
                var ProductSizes = new ProductSizes();
                ProductSizes.ProductId = Convert.ToInt32(comboBoxProduct.SelectedValue.ToString());
                ProductSizes.Size = Convert.ToInt32(comboBoxSize.SelectedValue.ToString());
                ProductSizes.IsAvaliable = Convert.ToInt32(comboBoxAvailable.SelectedValue.ToString());
                ProductSizes.Stock = Convert.ToInt32(textStock.Text);

                onCreate(ProductSizes);
            }

            this.Close();
            textStock.Clear();
        }
    }
}
