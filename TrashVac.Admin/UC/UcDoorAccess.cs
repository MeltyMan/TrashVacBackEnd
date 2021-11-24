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

namespace TrashVac.Admin.UC
{
    public partial class UcDoorAccess : UserControl
    {
        public UcDoorAccess()
        {
            InitializeComponent();
        }

        public IList<DoorWithAccess> SelectedAccess
        {
            get { return GetSelectedAccess(); }
        }

        private IList<DoorWithAccess> GetSelectedAccess()
        {
            var result = new List<DoorWithAccess>();
            foreach (Control c in pnlList.Controls)
            {
                if (c is CheckBox)
                {
                    result.Add(new DoorWithAccess()
                    {
                        Id = c.Tag.ToString().Substring(2), 
                        Description = string.Empty, 
                        HasAccess = (c as CheckBox).Checked
                    });
                   
                }
                
            }

            return result;
        }

        private void UcDoorAccess_Load(object sender, EventArgs e)
        {

        }

        public void PopulateControls(string rfId)
        {
            const int x = 10;
            const int space = 2;
            var y = 5;

            pnlList.Controls.Clear();
            
            var doors = ServiceProvider.Current.ApiClient.GetDoorListWithAccess(rfId);

            foreach (var door in doors)
            {
                var checkBox = new CheckBox();
                checkBox.Text = door.Description;
                checkBox.Tag = $"ID{door.Id}";
                checkBox.Checked = door.HasAccess;
                checkBox.Top = y;
                checkBox.Left = x;
                pnlList.Controls.Add(checkBox);

                y = y + checkBox.Height + space;

            }

        }
    }
}
