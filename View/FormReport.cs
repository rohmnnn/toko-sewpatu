using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TokoSepatuApp.Controller;
using TokoSepatuApp.Model.Entity;
using System.Globalization;

namespace TokoSepatuApp.View
{
    public partial class FormReport : Form
    {
        private List<Report> listOfReport = new List<Report>();
        private List<Brands> listOfBrands = new List<Brands>();
        public FormReport()
        {
            InitializeComponent();
            InisialisasiListView();
            var controllerBrand = new BrandsController();


            comboBoxBrand.Items.Clear();
            comboBoxBrand.DisplayMember = "Text";
            comboBoxBrand.ValueMember = "Value";

            comboBoxFilter.Items.Clear();
            comboBoxFilter.DisplayMember = "Text";
            comboBoxFilter.ValueMember = "Value";

            listOfBrands = controllerBrand.ReadAll();
            List<Object> items = new List<Object>();
            List<Object> options = new List<Object>();

            options.Add(new { Text = "Brand", Value = 1 });
            options.Add(new { Text = "Tanggal", Value = 2 });

            foreach (var brand in listOfBrands)
            {
                items.Add(new { Text = brand.Name, Value = brand.Id });
            }

            comboBoxBrand.DataSource = items;
            comboBoxFilter.DataSource= options;

            LoadReport();
        }

        public void LoadReport()
        {
            ReportController controllerReport = new ReportController();

            listView.Items.Clear();
            listOfReport = controllerReport.Report();

            foreach (var report in listOfReport)
            {
                var noUrut = listView.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());

                item.SubItems.Add(report.Date.ToShortDateString());
                item.SubItems.Add(report.OrderNo);
                item.SubItems.Add(report.Name);
                item.SubItems.Add(report.ProductName);
                item.SubItems.Add(report.Size.ToString());
                item.SubItems.Add(report.Price.ToString("C", CultureInfo.CurrentCulture));
                item.SubItems.Add(report.Amount.ToString());
                item.SubItems.Add(report.Total.ToString("C", CultureInfo.CurrentCulture));


                listView.Items.Add(item);
            }
        }

        private void InisialisasiListView()
        {
            listView.View = System.Windows.Forms.View.Details;
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.Columns.Add("No.", 35, HorizontalAlignment.Center);
            listView.Columns.Add("Tanggal", 80, HorizontalAlignment.Left);
            listView.Columns.Add("Order No", 180, HorizontalAlignment.Left);
            listView.Columns.Add("Brand", 80, HorizontalAlignment.Left);
            listView.Columns.Add("Nama Produk", 140, HorizontalAlignment.Left);
            listView.Columns.Add("Ukuran", 35, HorizontalAlignment.Left);
            listView.Columns.Add("Harga", 80, HorizontalAlignment.Left);
            listView.Columns.Add("Qty", 35, HorizontalAlignment.Left);
            listView.Columns.Add("Total", 125, HorizontalAlignment.Left);

        }

        private void btnTampilkan_Click(object sender, EventArgs e)
        {
            ReportController controllerReport = new ReportController();
            string id = comboBoxBrand.SelectedValue.ToString();
            string from = dateTimePickerDari.Value.ToString("yyyy-MM-dd");
            string to = dateTimePickerSampai.Value.ToString("yyyy-MM-dd");

            int option = Convert.ToInt32(comboBoxFilter.SelectedValue.ToString());


            if (option == 1)
            {
                listOfReport = controllerReport.ReportByBrandId(id);
            } else if (option == 2) {
                listOfReport = controllerReport.ReportByOrderDate(from, to);
            }

            listView.Items.Clear();

            foreach (var report in listOfReport)
            {
                var noUrut = listView.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());

                item.SubItems.Add(report.Date.ToShortDateString());
                item.SubItems.Add(report.OrderNo);
                item.SubItems.Add(report.Name);
                item.SubItems.Add(report.ProductName);
                item.SubItems.Add(report.Size.ToString());
                item.SubItems.Add(report.Price.ToString("C", CultureInfo.CurrentCulture));
                item.SubItems.Add(report.Amount.ToString());
                item.SubItems.Add(report.Total.ToString("C", CultureInfo.CurrentCulture));


                listView.Items.Add(item);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadReport();
        }
    }
}
