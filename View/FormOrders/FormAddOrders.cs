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

namespace TokoSepatuApp.View.FormOrders
{
    public partial class FormAddOrders : Form
    {
        public delegate void CreateEventHandler(Orders orders, OrderDetails orderDetails, Customers customers);
        public event CreateEventHandler onCreate;

        private List<Products> listOfProducts = new List<Products>();
        private List<Customers> listOfCustomers = new List<Customers>();

        public FormAddOrders()
        {
            ProductsController productController = new ProductsController();
            CustomersController customerController = new CustomersController();
            InitializeComponent();

            comboBoxProduct.Items.Clear();
            comboBoxProduct.DisplayMember = "Text";
            comboBoxProduct.ValueMember = "Value";

            listOfProducts = productController.ReadAllSizes();
            List<Object> itemsProduct = new List<Object>();

            foreach (var product in listOfProducts)
            {
                itemsProduct.Add(new { Text = product.Name + " - " + product.Size, Value = product.Id });
            }


            comboBoxProduct.DataSource = itemsProduct;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var orders = new Orders();
            var orderDetail = new OrderDetails();
            var customer = new Customers();

            orderDetail.ProductIdSize = Convert.ToInt32(comboBoxProduct.SelectedValue.ToString());
            orderDetail.Amount = Convert.ToInt32(textHarga.Text);

            // hitung harga
            ProductsController controller = new ProductsController();
            int total = 0;
            total = controller.TotalOrder(orderDetail.ProductIdSize, orderDetail.Amount);

            orders.Total = Convert.ToInt32(total);
            orderDetail.Total = Convert.ToInt32(total);

            customer.Name = textCustomerName.Text;
            customer.Address = textCustomerAddress.Text;


            var konfirmasi = MessageBox.Show("Total Harga :  Rp." + total, "Konfirmasi Pembayaran", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (konfirmasi == DialogResult.Yes)
            {
                onCreate(orders, orderDetail, customer);

            } else // data belum dipilih
            {
                MessageBox.Show("Transaksi dibatalkan", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Cari Foto",

                CheckFileExists = true,
                CheckPathExists = true,

                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
        }
    }
}
