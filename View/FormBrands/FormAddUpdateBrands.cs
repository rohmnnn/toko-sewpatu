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

namespace TokoSepatuApp.View.FormBrands
{
    public partial class FormAddUpdateBrands : Form
    {
        public delegate void CreateUpdateEventHandler(Brands brands);
        public event CreateUpdateEventHandler onCreate;
        public event CreateUpdateEventHandler onUpdate;

        private bool isUpdate = false;
        public FormAddUpdateBrands()
        {
            InitializeComponent();
        }

        public FormAddUpdateBrands(Brands brands) : this()
        {
            isUpdate = true;
            textName.Text = brands.Name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isUpdate)
            {
                var brands = new Brands();
                brands.Name = textName.Text;
                onUpdate(brands);
            } else
            {
                var brands = new Brands();
                brands.Name = textName.Text;
                onCreate(brands);
            }

            this.Close();
            textName.Clear();
        }
    }
}
