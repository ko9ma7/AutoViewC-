
namespace AutoViewWebAdsCSharp
{
    partial class FormStartup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStartup));
            this.buttonStartAuto = new System.Windows.Forms.Button();
            this.buttonStopAuto = new System.Windows.Forms.Button();
            this.checkedListBoxRunning = new System.Windows.Forms.CheckedListBox();
            this.buttonUpdateVMs = new System.Windows.Forms.Button();
            this.richTextProxyList = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonStartAuto
            // 
            this.buttonStartAuto.Location = new System.Drawing.Point(12, 316);
            this.buttonStartAuto.Name = "buttonStartAuto";
            this.buttonStartAuto.Size = new System.Drawing.Size(149, 22);
            this.buttonStartAuto.TabIndex = 2;
            this.buttonStartAuto.Text = "Bắt đầu chạy view";
            this.buttonStartAuto.UseVisualStyleBackColor = true;
            this.buttonStartAuto.Click += new System.EventHandler(this.ButtonStartAuto_Click);
            // 
            // buttonStopAuto
            // 
            this.buttonStopAuto.Enabled = false;
            this.buttonStopAuto.Location = new System.Drawing.Point(12, 344);
            this.buttonStopAuto.Name = "buttonStopAuto";
            this.buttonStopAuto.Size = new System.Drawing.Size(149, 25);
            this.buttonStopAuto.TabIndex = 3;
            this.buttonStopAuto.Text = "Dừng chạy view";
            this.buttonStopAuto.UseVisualStyleBackColor = true;
            this.buttonStopAuto.Click += new System.EventHandler(this.ButtonStopAuto_Click);
            // 
            // checkedListBoxRunning
            // 
            this.checkedListBoxRunning.FormattingEnabled = true;
            this.checkedListBoxRunning.Location = new System.Drawing.Point(12, 27);
            this.checkedListBoxRunning.Name = "checkedListBoxRunning";
            this.checkedListBoxRunning.Size = new System.Drawing.Size(149, 256);
            this.checkedListBoxRunning.TabIndex = 7;
            this.checkedListBoxRunning.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckedListBoxRunning_ItemCheck);
            // 
            // buttonUpdateVMs
            // 
            this.buttonUpdateVMs.Location = new System.Drawing.Point(12, 287);
            this.buttonUpdateVMs.Name = "buttonUpdateVMs";
            this.buttonUpdateVMs.Size = new System.Drawing.Size(149, 23);
            this.buttonUpdateVMs.TabIndex = 8;
            this.buttonUpdateVMs.Text = "Cập nhật máy ảo";
            this.buttonUpdateVMs.UseVisualStyleBackColor = true;
            this.buttonUpdateVMs.Click += new System.EventHandler(this.ButtonUpdateVMs_Click);
            // 
            // richTextProxyList
            // 
            this.richTextProxyList.Location = new System.Drawing.Point(167, 28);
            this.richTextProxyList.Name = "richTextProxyList";
            this.richTextProxyList.Size = new System.Drawing.Size(221, 371);
            this.richTextProxyList.TabIndex = 9;
            this.richTextProxyList.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(167, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Proxy List";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 376);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Chụp màn hình";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "Máy ảo";
            // 
            // FormStartup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 408);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextProxyList);
            this.Controls.Add(this.buttonUpdateVMs);
            this.Controls.Add(this.checkedListBoxRunning);
            this.Controls.Add(this.buttonStopAuto);
            this.Controls.Add(this.buttonStartAuto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormStartup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto View ADS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonStartAuto;
        private System.Windows.Forms.Button buttonStopAuto;
        private System.Windows.Forms.CheckedListBox checkedListBoxRunning;
        private System.Windows.Forms.Button buttonUpdateVMs;
        private System.Windows.Forms.RichTextBox richTextProxyList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
    }
}

