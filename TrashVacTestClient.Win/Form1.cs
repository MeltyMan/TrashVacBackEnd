using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrashVacTestClient.Win
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _apiClient = new ApiClient();
        }

        private readonly ApiClient _apiClient;

        private void btnGo_Click(object sender, EventArgs e)
        {
            ValidateAccess();
        }

        private void ValidateAccess()
        {
            var result = _apiClient.ValidateAccess(txtRfId.Text, txtDoorId.Text);

            if (result != null)
            {
                
                txtResult.Text = result.IsValid ? result.User.FirstName : "Failed";
            }

        }
    }
}
