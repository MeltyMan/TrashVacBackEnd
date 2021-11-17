using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrashVac.Admin.Remote;
using TrashVac.Entity;

namespace TrashVac.Admin
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = Common.AppName();

#if DEBUG
            txtUserName.Text = "kalle";
            txtPassword.Text = "kalle!";
#endif

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {
          
            var loginOk = false;

            var loginResponse = ServiceProvider.Current.ApiClient.Login(txtUserName.Text, txtPassword.Text);

            if (loginResponse != null)
            {
                if (loginResponse.UserLevel == Enums.UserLevel.Admin)
                {
                    ServiceProvider.Current.ApiClient.AccessToken = loginResponse.AccessToken;
                    loginOk = true;

                }
            }

            if (!loginOk)
            {
                MessageBox.Show(this, "Login Failed!", Common.AppName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                
                this.Visible = false;
                var mainForm = new MainForm();
                mainForm.Show();
            }

        }
    }
}
