namespace TestingProject.View
{
    partial class Subscribers
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TBMessage = new System.Windows.Forms.TextBox();
            this.BtnPublish = new System.Windows.Forms.Button();
            this.BtnStop = new System.Windows.Forms.Button();
            this.BtnStart = new System.Windows.Forms.Button();
            this.SubscribersHistory = new Helpers.Controls.SubscribersList();
            this.ActiveSubscribers = new Helpers.Controls.SubscribersList();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.TBMessage);
            this.splitContainer1.Panel1.Controls.Add(this.BtnPublish);
            this.splitContainer1.Panel1.Controls.Add(this.BtnStop);
            this.splitContainer1.Panel1.Controls.Add(this.BtnStart);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.SubscribersHistory);
            this.splitContainer1.Panel2.Controls.Add(this.ActiveSubscribers);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 448;
            this.splitContainer1.TabIndex = 0;
            // 
            // TBMessage
            // 
            this.TBMessage.Location = new System.Drawing.Point(113, 187);
            this.TBMessage.Name = "TBMessage";
            this.TBMessage.Size = new System.Drawing.Size(223, 20);
            this.TBMessage.TabIndex = 3;
            // 
            // BtnPublish
            // 
            this.BtnPublish.Location = new System.Drawing.Point(186, 214);
            this.BtnPublish.Name = "BtnPublish";
            this.BtnPublish.Size = new System.Drawing.Size(75, 23);
            this.BtnPublish.TabIndex = 2;
            this.BtnPublish.Text = "Publish";
            this.BtnPublish.UseVisualStyleBackColor = true;
            this.BtnPublish.Click += new System.EventHandler(this.BtnPublish_Click);
            // 
            // BtnStop
            // 
            this.BtnStop.Enabled = false;
            this.BtnStop.Location = new System.Drawing.Point(248, 53);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(75, 23);
            this.BtnStop.TabIndex = 1;
            this.BtnStop.Text = "Stop";
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(144, 53);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(75, 23);
            this.BtnStart.TabIndex = 0;
            this.BtnStart.Text = "Start";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // SubscribersHistory
            // 
            this.SubscribersHistory.DisplayMember = "Title";
            this.SubscribersHistory.FormattingEnabled = true;
            this.SubscribersHistory.Location = new System.Drawing.Point(6, 210);
            this.SubscribersHistory.Name = "SubscribersHistory";
            this.SubscribersHistory.Size = new System.Drawing.Size(331, 225);
            this.SubscribersHistory.TabIndex = 3;
            this.SubscribersHistory.ValueMember = "Connected";
            // 
            // ActiveSubscribers
            // 
            this.ActiveSubscribers.DisplayMember = "Title";
            this.ActiveSubscribers.FormattingEnabled = true;
            this.ActiveSubscribers.Location = new System.Drawing.Point(6, 25);
            this.ActiveSubscribers.Name = "ActiveSubscribers";
            this.ActiveSubscribers.Size = new System.Drawing.Size(331, 147);
            this.ActiveSubscribers.TabIndex = 2;
            this.ActiveSubscribers.ValueMember = "Connected";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "History of subscribers:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Active subscribers:";
            // 
            // Subscribers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Subscribers";
            this.Text = "Subscribers";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button BtnStop;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Helpers.Controls.SubscribersList ActiveSubscribers;
        private Helpers.Controls.SubscribersList SubscribersHistory;
        private System.Windows.Forms.Button BtnPublish;
        private System.Windows.Forms.TextBox TBMessage;
    }
}