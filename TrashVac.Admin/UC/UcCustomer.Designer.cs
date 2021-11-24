
namespace TrashVac.Admin.UC
{
    partial class UcCustomer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpTop = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearchString = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.lstCustomers = new System.Windows.Forms.ListView();
            this.chLastName = new System.Windows.Forms.ColumnHeader();
            this.chFirstName = new System.Windows.Forms.ColumnHeader();
            this.cmnuUser = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuTags = new System.Windows.Forms.ToolStripMenuItem();
            this.grpTop.SuspendLayout();
            this.cmnuUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpTop
            // 
            this.grpTop.Controls.Add(this.label1);
            this.grpTop.Controls.Add(this.txtSearchString);
            this.grpTop.Controls.Add(this.btnCreate);
            this.grpTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpTop.Location = new System.Drawing.Point(0, 0);
            this.grpTop.Name = "grpTop";
            this.grpTop.Size = new System.Drawing.Size(697, 63);
            this.grpTop.TabIndex = 0;
            this.grpTop.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(152, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filter:";
            // 
            // txtSearchString
            // 
            this.txtSearchString.Location = new System.Drawing.Point(194, 22);
            this.txtSearchString.Name = "txtSearchString";
            this.txtSearchString.Size = new System.Drawing.Size(261, 23);
            this.txtSearchString.TabIndex = 1;
            this.txtSearchString.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(16, 22);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // lstCustomers
            // 
            this.lstCustomers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chLastName,
            this.chFirstName});
            this.lstCustomers.ContextMenuStrip = this.cmnuUser;
            this.lstCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCustomers.FullRowSelect = true;
            this.lstCustomers.GridLines = true;
            this.lstCustomers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstCustomers.HideSelection = false;
            this.lstCustomers.Location = new System.Drawing.Point(0, 63);
            this.lstCustomers.Name = "lstCustomers";
            this.lstCustomers.Size = new System.Drawing.Size(697, 408);
            this.lstCustomers.TabIndex = 1;
            this.lstCustomers.UseCompatibleStateImageBehavior = false;
            this.lstCustomers.View = System.Windows.Forms.View.Details;
            // 
            // chLastName
            // 
            this.chLastName.Text = "Last Name";
            this.chLastName.Width = 200;
            // 
            // chFirstName
            // 
            this.chFirstName.Text = "First Name";
            this.chFirstName.Width = 200;
            // 
            // cmnuUser
            // 
            this.cmnuUser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTags});
            this.cmnuUser.Name = "cmnuUser";
            this.cmnuUser.Size = new System.Drawing.Size(181, 48);
            // 
            // mnuTags
            // 
            this.mnuTags.Name = "mnuTags";
            this.mnuTags.Size = new System.Drawing.Size(180, 22);
            this.mnuTags.Text = "Tags";
            this.mnuTags.Click += new System.EventHandler(this.mnuTags_Click);
            // 
            // UcCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstCustomers);
            this.Controls.Add(this.grpTop);
            this.Name = "UcCustomer";
            this.Size = new System.Drawing.Size(697, 471);
            this.Load += new System.EventHandler(this.UcCustomer_Load);
            this.grpTop.ResumeLayout(false);
            this.grpTop.PerformLayout();
            this.cmnuUser.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTop;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearchString;
        private System.Windows.Forms.ListView lstCustomers;
        private System.Windows.Forms.ColumnHeader chLastName;
        private System.Windows.Forms.ColumnHeader chFirstName;
        private System.Windows.Forms.ContextMenuStrip cmnuUser;
        private System.Windows.Forms.ToolStripMenuItem mnuTags;
    }
}
