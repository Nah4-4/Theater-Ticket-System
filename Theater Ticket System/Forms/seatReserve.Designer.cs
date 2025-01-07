namespace Theater_Ticket_System
{
    partial class seatReserve
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
            this.orchestraPanel = new System.Windows.Forms.Panel();
            this.MezzaninePanel = new System.Windows.Forms.Panel();
            this.balconyPanel = new System.Windows.Forms.Panel();
            this.doneButton = new System.Windows.Forms.Button();
            this.boxPanel = new System.Windows.Forms.Panel();
            this.boxPanel2 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // orchestraPanel
            // 
            this.orchestraPanel.BackColor = System.Drawing.Color.White;
            this.orchestraPanel.Location = new System.Drawing.Point(270, 80);
            this.orchestraPanel.Name = "orchestraPanel";
            this.orchestraPanel.Size = new System.Drawing.Size(200, 100);
            this.orchestraPanel.TabIndex = 0;
            // 
            // MezzaninePanel
            // 
            this.MezzaninePanel.BackColor = System.Drawing.Color.White;
            this.MezzaninePanel.Location = new System.Drawing.Point(270, 459);
            this.MezzaninePanel.Name = "MezzaninePanel";
            this.MezzaninePanel.Size = new System.Drawing.Size(200, 100);
            this.MezzaninePanel.TabIndex = 1;
            // 
            // balconyPanel
            // 
            this.balconyPanel.BackColor = System.Drawing.Color.White;
            this.balconyPanel.Location = new System.Drawing.Point(270, 930);
            this.balconyPanel.Name = "balconyPanel";
            this.balconyPanel.Size = new System.Drawing.Size(200, 100);
            this.balconyPanel.TabIndex = 2;
            // 
            // doneButton
            // 
            this.doneButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.doneButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.doneButton.Location = new System.Drawing.Point(0, 1030);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(1829, 57);
            this.doneButton.TabIndex = 3;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // boxPanel
            // 
            this.boxPanel.BackColor = System.Drawing.Color.White;
            this.boxPanel.Location = new System.Drawing.Point(78, 80);
            this.boxPanel.Name = "boxPanel";
            this.boxPanel.Size = new System.Drawing.Size(129, 100);
            this.boxPanel.TabIndex = 1;
            // 
            // boxPanel2
            // 
            this.boxPanel2.BackColor = System.Drawing.Color.White;
            this.boxPanel2.Location = new System.Drawing.Point(1700, 80);
            this.boxPanel2.Name = "boxPanel2";
            this.boxPanel2.Size = new System.Drawing.Size(129, 100);
            this.boxPanel2.TabIndex = 2;
            // 
            // seatReserve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1385, 793);
            this.Controls.Add(this.boxPanel2);
            this.Controls.Add(this.boxPanel);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.MezzaninePanel);
            this.Controls.Add(this.balconyPanel);
            this.Controls.Add(this.orchestraPanel);
            this.Name = "seatReserve";
            this.Text = "The Medallion Theater Seating";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel orchestraPanel;
        private System.Windows.Forms.Panel MezzaninePanel;
        private System.Windows.Forms.Panel balconyPanel;
        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.Panel boxPanel;
        private System.Windows.Forms.Panel boxPanel2;
    }
}

