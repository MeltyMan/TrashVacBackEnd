using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrashVac.Entity.Dto;

namespace TrashVac.Admin
{
    public partial class HandleCustomerTagsForm : Form
    {
        public HandleCustomerTagsForm()
        {
            InitializeComponent();
        }
        public Guid UserId { get; set; }
        private void HandleCustomerTagsForm_Load(object sender, EventArgs e)
        {
            this.KeyUp += OnKeyUp;
        }

        private void OnKeyUp(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                CloseMe();
            }
        }

        public void InitMe()
        {
            PopulateList();
        }
        private void PopulateList()
        {
            lstTags.Items.Clear();
            
            var user = ServiceProvider.Current.ApiClient.GetUserWithTags(this.UserId);
            if (user != null)
            {
                lblCustomerName.Text = $"{user.FirstName} {user.LastName}";

                foreach (var userTag in user.Tags)
                {
                    var li = new ListViewItem(userTag.RfId);
                    li.SubItems.Add(userTag.Description);
                    lstTags.Items.Add(li);
                }
            }
        }

        private void btnAddTag_Click(object sender, EventArgs e)
        {
            AddTag();
        }

        private void AddTag()
        {
            var tagListForm = new TagListForm();
            tagListForm.InitMe();
            tagListForm.ShowDialog(this);

            if (!string.IsNullOrEmpty(tagListForm.SelectedTag))
            {
                var result = ServiceProvider.Current.ApiClient.PersistUserTagRelation(this.UserId,
                    new UserRfIdRelation() { UserId = this.UserId, RfId = tagListForm.SelectedTag, Deleted = false });
                if (result)
                {
                    result = ServiceProvider.Current.ApiClient.PersistDoorAccess(tagListForm.SelectedTag,
                        tagListForm.DoorAccess);


                    PopulateList();
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            CloseMe();
        }

        private void CloseMe()
        {
            this.Close();
        }
    }
}
