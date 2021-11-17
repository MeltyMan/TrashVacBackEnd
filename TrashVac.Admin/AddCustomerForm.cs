using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrashVac.Entity;

namespace TrashVac.Admin
{
    public partial class AddCustomerForm : Form
    {
        public AddCustomerForm()
        {
            InitializeComponent();
        }

        private IList<Enums.UserLevel> _userLevels;
        public bool IsCancelled { get; set; }
        private void AddCustomerForm_Load(object sender, EventArgs e)
        {
            this.KeyUp += OnKeyUp;
            _userLevels = new List<Enums.UserLevel>();
            _userLevels.Add(Enums.UserLevel.Standard);
            _userLevels.Add(Enums.UserLevel.Admin);

            drpUserLevel.Items.Clear();
            drpUserLevel.Items.Add("Standard");
            drpUserLevel.Items.Add("Admin");
            drpUserLevel.SelectedIndex = 0;

          

        }

        private void OnKeyUp(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                CancelMe();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelMe();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            var user = new UserFull()
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text, 
                Id = Guid.Empty, 
                Pwd = txtPassword.Text, 
                UserName = txtUserName.Text, 
                UserLevel = _userLevels[drpUserLevel.SelectedIndex]
            };

            var userId = ServiceProvider.Current.ApiClient.AddUser(user);

            IsCancelled = false;
            this.Close();

        }

        private void CancelMe()
        {
            this.IsCancelled = true;
            this.Close();
            
        }

    }
}
