
namespace TrashVacTestClient.Win
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtRfId = new System.Windows.Forms.TextBox();
            this.txtDoorId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "RFID:";
            // 
            // txtRfId
            // 
            this.txtRfId.Location = new System.Drawing.Point(73, 18);
            this.txtRfId.Name = "txtRfId";
            this.txtRfId.Size = new System.Drawing.Size(100, 23);
            this.txtRfId.TabIndex = 1;
            this.txtRfId.Text = "KALLE1";
            // 
            // txtDoorId
            // 
            this.txtDoorId.Location = new System.Drawing.Point(73, 47);
            this.txtDoorId.Name = "txtDoorId";
            this.txtDoorId.Size = new System.Drawing.Size(100, 23);
            this.txtDoorId.TabIndex = 3;
            this.txtDoorId.Text = "DOOR1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Door Id:";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(12, 157);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(533, 23);
            this.txtResult.TabIndex = 4;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(33, 76);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(140, 23);
            this.btnGo.TabIndex = 5;
            this.btnGo.Text = "Validate Access";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 353);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtDoorId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRfId);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRfId;
        private System.Windows.Forms.TextBox txtDoorId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnGo;
    }
}

