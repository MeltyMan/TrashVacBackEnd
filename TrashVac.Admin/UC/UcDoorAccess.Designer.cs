
namespace TrashVac.Admin.UC
{
    partial class UcDoorAccess
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
            this.grpContainer = new System.Windows.Forms.GroupBox();
            this.pnlList = new System.Windows.Forms.Panel();
            this.grpContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpContainer
            // 
            this.grpContainer.Controls.Add(this.pnlList);
            this.grpContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpContainer.Location = new System.Drawing.Point(0, 0);
            this.grpContainer.Name = "grpContainer";
            this.grpContainer.Size = new System.Drawing.Size(250, 199);
            this.grpContainer.TabIndex = 0;
            this.grpContainer.TabStop = false;
            this.grpContainer.Text = "Door Access";
            // 
            // pnlList
            // 
            this.pnlList.AutoScroll = true;
            this.pnlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlList.Location = new System.Drawing.Point(3, 19);
            this.pnlList.Name = "pnlList";
            this.pnlList.Size = new System.Drawing.Size(244, 177);
            this.pnlList.TabIndex = 0;
            // 
            // UcDoorAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpContainer);
            this.Name = "UcDoorAccess";
            this.Size = new System.Drawing.Size(250, 199);
            this.Load += new System.EventHandler(this.UcDoorAccess_Load);
            this.grpContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpContainer;
        private System.Windows.Forms.Panel pnlList;
    }
}
