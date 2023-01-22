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
    public partial class FormUpdateUsers : Form
    {
        public delegate void UpdateEventHandler(Users users);
        public event UpdateEventHandler onUpdate;
        public FormUpdateUsers()
        {
            InitializeComponent();
        }

        public FormUpdateUsers(Users users) : this()
        {
            textEmail.Text = users.Email;
            textName.Text = users.Name;
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

            onUpdate(users);

            textName.Clear();
            textEmail.Clear();

            this.Close();
        }
    }
}
