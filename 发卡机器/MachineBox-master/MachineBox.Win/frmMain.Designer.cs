using MachineBox.Core.Globals;

namespace MachineBox.Win
{
    partial class frmMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSummary = new System.Windows.Forms.RichTextBox();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.chckCompabilityModeBarcodeReader = new System.Windows.Forms.CheckBox();
            this.btnOpenLogs = new System.Windows.Forms.Button();
            this.btnStartKiosk = new System.Windows.Forms.Button();
            this.chckRemoveLastCharacter = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSummary);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(8, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6, 0, 6, 6);
            this.groupBox2.Size = new System.Drawing.Size(499, 208);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // txtSummary
            // 
            this.txtSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSummary.ForeColor = System.Drawing.Color.Gray;
            this.txtSummary.Location = new System.Drawing.Point(6, 14);
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.ReadOnly = true;
            this.txtSummary.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtSummary.Size = new System.Drawing.Size(487, 188);
            this.txtSummary.TabIndex = 1;
            this.txtSummary.Text = "";
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 60000;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // chckCompabilityModeBarcodeReader
            // 
            this.chckCompabilityModeBarcodeReader.AutoSize = true;
            this.chckCompabilityModeBarcodeReader.Location = new System.Drawing.Point(14, 14);
            this.chckCompabilityModeBarcodeReader.Name = "chckCompabilityModeBarcodeReader";
            this.chckCompabilityModeBarcodeReader.Size = new System.Drawing.Size(255, 17);
            this.chckCompabilityModeBarcodeReader.TabIndex = 4;
            this.chckCompabilityModeBarcodeReader.Text = "Turn on compatibility mode for USB HID";
            this.chckCompabilityModeBarcodeReader.UseVisualStyleBackColor = true;
            this.chckCompabilityModeBarcodeReader.CheckedChanged += new System.EventHandler(this.chckCompabilityModeBarcodeReader_CheckedChanged);
            // 
            // btnOpenLogs
            // 
            this.btnOpenLogs.Location = new System.Drawing.Point(408, 37);
            this.btnOpenLogs.Name = "btnOpenLogs";
            this.btnOpenLogs.Size = new System.Drawing.Size(99, 23);
            this.btnOpenLogs.TabIndex = 5;
            this.btnOpenLogs.Text = "Open Logs";
            this.btnOpenLogs.UseVisualStyleBackColor = true;
            this.btnOpenLogs.Click += new System.EventHandler(this.btnOpenLogs_Click);
            // 
            // btnStartKiosk
            // 
            this.btnStartKiosk.Location = new System.Drawing.Point(409, 8);
            this.btnStartKiosk.Name = "btnStartKiosk";
            this.btnStartKiosk.Size = new System.Drawing.Size(98, 23);
            this.btnStartKiosk.TabIndex = 6;
            this.btnStartKiosk.Text = "Start Kiosk";
            this.btnStartKiosk.UseVisualStyleBackColor = true;
            this.btnStartKiosk.Click += new System.EventHandler(this.btnStartKiosk_Click);
            // 
            // chckRemoveLastCharacter
            // 
            this.chckRemoveLastCharacter.AutoSize = true;
            this.chckRemoveLastCharacter.Location = new System.Drawing.Point(14, 41);
            this.chckRemoveLastCharacter.Name = "chckRemoveLastCharacter";
            this.chckRemoveLastCharacter.Size = new System.Drawing.Size(291, 17);
            this.chckRemoveLastCharacter.TabIndex = 7;
            this.chckRemoveLastCharacter.Text = "Remove last character from scanned bar code";
            this.chckRemoveLastCharacter.UseVisualStyleBackColor = true;
            this.chckRemoveLastCharacter.CheckedChanged += new System.EventHandler(this.chckRemoveLastCharacter_CheckedChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 282);
            this.Controls.Add(this.chckRemoveLastCharacter);
            this.Controls.Add(this.btnStartKiosk);
            this.Controls.Add(this.btnOpenLogs);
            this.Controls.Add(this.chckCompabilityModeBarcodeReader);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Padding = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox txtSummary;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.CheckBox chckCompabilityModeBarcodeReader;
        private System.Windows.Forms.Button btnOpenLogs;
        private System.Windows.Forms.Button btnStartKiosk;
        private System.Windows.Forms.CheckBox chckRemoveLastCharacter;
    }
}

