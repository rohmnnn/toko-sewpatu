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

namespace TokoSepatuApp.View.FormUsers
{
    public partial class FormAddUpdateBrands : Form
    {
        public delegate void CreateEventHandler(Users users);
        public event CreateEventHandler onCreate;
        public FormAddUpdateBrands()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var users = new Users();

            users.Name = textName.Text;
            users.Email = textEmail.Text;
            users.Password = textPassword.Text;

            onCreate(users);

            textName.Clear();
            textEmail.Clear();
            textPassword.Clear();
        }
    }
}
