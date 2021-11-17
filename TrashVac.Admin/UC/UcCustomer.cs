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
        public UcCustomer()
        {
            InitializeComponent();
        }

        private void UcCustomer_Load(object sender, EventArgs e)
        {
            PopulateList();
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
    }
}
