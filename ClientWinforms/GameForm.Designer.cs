using System.Drawing;
using System.Windows.Forms;

namespace ClientWinforms
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private PictureBox backgroundPicture;
        private PictureBox firePlayerPicture;
        private PictureBox icePlayerPicture;
        private PictureBox exitPicture;
        private Button connectButton;
        private Button resetButton;
        private Label statusLabel;
        private Label instructionLabel;

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
            components = new System.ComponentModel.Container();
            backgroundPicture = new PictureBox();
            firePlayerPicture = new PictureBox();
            icePlayerPicture = new PictureBox();
            exitPicture = new PictureBox();
            connectButton = new Button();
            resetButton = new Button();
            statusLabel = new Label();
            instructionLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)backgroundPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)firePlayerPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)icePlayerPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)exitPicture).BeginInit();
            SuspendLayout();
            //
            // backgroundPicture
            //
            backgroundPicture.BackColor = Color.Black;
            backgroundPicture.Dock = DockStyle.Fill;
            backgroundPicture.Name = "backgroundPicture";
            backgroundPicture.Size = new Size(927, 692);
            backgroundPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            backgroundPicture.TabIndex = 0;
            backgroundPicture.TabStop = false;
            // 
            // firePlayerPicture
            // 
            firePlayerPicture.BackColor = Color.Transparent;
            firePlayerPicture.Location = new Point(80, 420);
            firePlayerPicture.Name = "firePlayerPicture";
            firePlayerPicture.Size = new Size(48, 64);
            firePlayerPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            firePlayerPicture.TabIndex = 1;
            firePlayerPicture.TabStop = false;
            // 
            // icePlayerPicture
            // 
            icePlayerPicture.BackColor = Color.Transparent;
            icePlayerPicture.Location = new Point(150, 420);
            icePlayerPicture.Name = "icePlayerPicture";
            icePlayerPicture.Size = new Size(48, 64);
            icePlayerPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            icePlayerPicture.TabIndex = 2;
            icePlayerPicture.TabStop = false;
            // 
            // exitPicture
            // 
            exitPicture.BackColor = Color.Transparent;
            exitPicture.Location = new Point(780, 80);
            exitPicture.Name = "exitPicture";
            exitPicture.Size = new Size(80, 80);
            exitPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            exitPicture.TabIndex = 3;
            exitPicture.TabStop = false;
            // 
            // connectButton
            // 
            connectButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            connectButton.Location = new Point(12, 588);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(120, 36);
            connectButton.TabIndex = 4;
            connectButton.Text = "连接服务器";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // resetButton
            // 
            resetButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            resetButton.Location = new Point(150, 588);
            resetButton.Name = "resetButton";
            resetButton.Size = new Size(120, 36);
            resetButton.TabIndex = 5;
            resetButton.Text = "重新开始";
            resetButton.UseVisualStyleBackColor = true;
            resetButton.Click += resetButton_Click;
            // 
            // statusLabel
            // 
            statusLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            statusLabel.AutoSize = true;
            statusLabel.Font = new Font("Microsoft YaHei", 10F, FontStyle.Regular, GraphicsUnit.Point);
            statusLabel.Location = new Point(288, 595);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(111, 23);
            statusLabel.TabIndex = 6;
            statusLabel.Text = "未连接服务器";
            // 
            // instructionLabel
            // 
            instructionLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            instructionLabel.AutoSize = true;
            instructionLabel.Font = new Font("Microsoft YaHei", 9F, FontStyle.Regular, GraphicsUnit.Point);
            instructionLabel.Location = new Point(12, 640);
            instructionLabel.MaximumSize = new Size(900, 0);
            instructionLabel.Name = "instructionLabel";
            instructionLabel.Size = new Size(781, 40);
            instructionLabel.TabIndex = 7;
            instructionLabel.Text = "方向键控制火娃，WASD 控制冰娃。移动命令将发送到服务器，由服务器判定是否碰撞障碍并返回位置与胜利状态。";
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(927, 692);
            Controls.Add(instructionLabel);
            Controls.Add(statusLabel);
            Controls.Add(resetButton);
            Controls.Add(connectButton);
            Controls.Add(exitPicture);
            Controls.Add(icePlayerPicture);
            Controls.Add(firePlayerPicture);
            Controls.Add(backgroundPicture);
            Name = "GameForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "森林冰火人";
            KeyPreview = true;
            KeyDown += GameForm_KeyDown;
            ((System.ComponentModel.ISupportInitialize)backgroundPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)firePlayerPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)icePlayerPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)exitPicture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
