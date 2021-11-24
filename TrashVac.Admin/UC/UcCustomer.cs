using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrashVac.Admin.UC
{
    public partial class UcCustomer : UserControl
    {
        private bool _searchCancelled = false;

        public UcCustomer()
        {
            InitializeComponent();
        }

        private void UcCustomer_Load(object sender, EventArgs e)
        {
            PopulateList();
            txtSearchString.KeyUp += TxtSearchStringOnKeyUp;
        }

        private void TxtSearchStringOnKeyUp(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _searchCancelled = true;
                txtSearchString.Text = string.Empty;
                PopulateList();
                _searchCancelled = false;
            }
        }

        public void PopulateList()
        {
            lstCustomers.Items.Clear();
            
            var users = ServiceProvider.Current.ApiClient.GetUserList();
            foreach (var user in users)
            {
                var listItem = new ListViewItem(user.LastName);
                listItem.SubItems.Add(user.FirstName);
                listItem.Tag = $"ID{user.Id}";
                lstCustomers.Items.Add(listItem);
            }


        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var addForm = new AddCustomerForm();
            addForm.ShowDialog(this);
            if (!addForm.IsCancelled)
            {
                PopulateList();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SearchUser();
        }

        private void SearchUser()
        {
            if (!_searchCancelled)
            {
                lstCustomers.Items.Clear();

                var users = ServiceProvider.Current.ApiClient.SearchUser(txtSearchString.Text);
                foreach (var user in users)
                {
                    var listItem = new ListViewItem(user.LastName);
                    listItem.SubItems.Add(user.FirstName);
                    listItem.Tag = $"ID{user.Id}";
                    lstCustomers.Items.Add(listItem);
                }
            }

        }

        private void mnuTags_Click(object sender, EventArgs e)
        {
            ShowCustomerTags();
        }

        private void ShowCustomerTags()
        {
            if (lstCustomers.SelectedItems.Count == 1)
            {
                if (lstCustomers.SelectedItems[0].Tag != null)
                {
                    var userId = new Guid(lstCustomers.SelectedItems[0].Tag.ToString().Substring(2));
                    var tagForm = new HandleCustomerTagsForm();
                    tagForm.UserId = userId;
                    tagForm.InitMe();
                    tagForm.ShowDialog(this);
                    tagForm = null;
                }
            }
        }
    }
}
