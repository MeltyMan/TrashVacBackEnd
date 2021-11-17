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

namespace TrashVac.Admin
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = Common.AppName();
            this.Closed += OnClosed;
        }

        private void OnClosed(object? sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
