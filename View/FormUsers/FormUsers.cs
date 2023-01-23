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

namespace TokoSepatuApp.View.FormUsers
{
    public partial class FormUsers : Form
    {
        private List<Users> listOfUsers = new List<Users>();
        private UsersController controller;
        public FormUsers()
        {
            InitializeComponent();
            InisialisasiListView();
            controller = new UsersController();

            LoadUsers();
        }

        public void LoadUsers()
        {
            listView.Items.Clear();
            listOfUsers = controller.ReadAll();

            foreach (var user in listOfUsers)
            {
                var noUrut = listView.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());

                item.Tag = user.Id;
                item.SubItems.Add(user.Name);
                item.SubItems.Add(user.Email);

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
            listView.Columns.Add("Email", 250, HorizontalAlignment.Left);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var formAddUser = new FormAddUsers();
            formAddUser.onCreate += onCreate;

            formAddUser.ShowDialog();
        }

        private void onCreate(Users users)
        {
            controller.Create(users);
            LoadUsers();
        }

        private void onUpdate(Users users)
        {
            string id = listView.SelectedItems[0].Tag.ToString();

            controller.Update(users, id);
            LoadUsers();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                var konfirmasi = MessageBox.Show("Data akan dihapus? ", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (konfirmasi == DialogResult.Yes)
                {
                    string id = listView.SelectedItems[0].Tag.ToString();
                    var result = controller.Delete(id);
                    if (result > 0) LoadUsers();
                }
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data tidak ditemukan!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                Users users = listOfUsers[listView.SelectedIndices[0]];

                var formUpdateUser = new FormUpdateUsers(users);
                formUpdateUser.onUpdate += onUpdate;

                formUpdateUser.ShowDialog();
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data tidak ditemukan!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            listView.Items.Clear();
            listOfUsers = controller.Search(textCari.Text);

            foreach (var user in listOfUsers)
            {
                var noUrut = listView.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());

                item.Tag = user.Id;
                item.SubItems.Add(user.Name);
                item.SubItems.Add(user.Email);

                listView.Items.Add(item);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }
    }
}
