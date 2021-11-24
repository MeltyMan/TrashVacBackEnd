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
    public partial class TagListForm : Form
    {
        public TagListForm()
        {
            InitializeComponent();
        }

        private IList<string> _tagIdList = new List<string>();

        public string SelectedTag { get; set; } = "";
        public IList<DoorWithAccess> DoorAccess { get; set; } = new List<DoorWithAccess>();

        private void TagListForm_Load(object sender, EventArgs e)
        {
            drpTags.SelectedIndexChanged += DrpTagsOnSelectedIndexChanged;
            drpTags.KeyUp += DrpTagsOnKeyUp;
        }

        private void DrpTagsOnKeyUp(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Cancel();
            }
        }

        private void DrpTagsOnSelectedIndexChanged(object? sender, EventArgs e)
        {
            this.SelectedTag = _tagIdList[drpTags.SelectedIndex];
            this.AcceptButton = drpTags.SelectedIndex > 0 ? btnOk : null;

            doorAccess.Visible = drpTags.SelectedIndex != 0;
            if (doorAccess.Visible)
            {
                doorAccess.PopulateControls(this.SelectedTag);
            }

        }

        public void InitMe()
        {
            PopulateList();
        }

        private void PopulateList()
        {
            drpTags.Items.Clear();
            _tagIdList.Clear();
            
            var tags = ServiceProvider.Current.ApiClient.GetTagList();
            drpTags.Items.Add("-- select tag --");
            _tagIdList.Add("");
         
            if (tags != null)
            {
                foreach (var rfIdTag in tags)
                {
                   
                    drpTags.Items.Add(rfIdTag.Description);
                    _tagIdList.Add(rfIdTag.RfId);
                }
            }

            drpTags.SelectedIndex = 0;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DoorAccess = doorAccess.SelectedAccess;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        private void Cancel()
        {
            this.SelectedTag = string.Empty;
            this.Close();
            
        }
    }
}
