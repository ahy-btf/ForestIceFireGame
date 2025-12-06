namespace ClientWinformsExamples
{
    partial class GameForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.PictureBox pictureBox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.sendButton = new System.Windows.Forms.Button();
            this.messageLabel = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();

            // sendButton
            this.sendButton.Location = new System.Drawing.Point(50, 150);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(100, 50);
            this.sendButton.TabIndex = 0;
            this.sendButton.Text = "发送消息";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);

            // messageLabel
            this.messageLabel.AutoSize = true;
            this.messageLabel.Location = new System.Drawing.Point(50, 220);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(81, 17);
            this.messageLabel.TabIndex = 1;
            this.messageLabel.Text = "服务器回应:";

            // pictureBox
            this.pictureBox.Location = new System.Drawing.Point(200, 50);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(200, 200);
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            this.pictureBox.Image = System.Drawing.Image.FromFile("resources.png"); // 加载图像资源

            // GameForm
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.sendButton);
            this.Name = "GameForm";
            this.Text = "森林冰火人游戏";

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
