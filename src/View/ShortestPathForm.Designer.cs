namespace Map_Creation_Tool.src.View
{
    partial class ShortestPathForm
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
            pictureBox1 = new PictureBox();
            roundedButton1 = new Model.RoundedButton();
            homeBtn = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)homeBtn).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Location = new Point(322, 223);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1280, 720);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // roundedButton1
            // 
            roundedButton1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            roundedButton1.BackColor = Color.FromArgb(128, 128, 255);
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Popup;
            roundedButton1.Font = new Font("Arial", 10F, FontStyle.Bold);
            roundedButton1.ForeColor = Color.Black;
            roundedButton1.Location = new Point(12, 949);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(430, 108);
            roundedButton1.TabIndex = 4;
            roundedButton1.Text = "Back";
            roundedButton1.UseVisualStyleBackColor = false;
            roundedButton1.Click += roundedButton1_Click;
            // 
            // homeBtn
            // 
            homeBtn.Cursor = Cursors.Hand;
            homeBtn.Image = Properties.Resources.logo;
            homeBtn.Location = new Point(12, 12);
            homeBtn.Name = "homeBtn";
            homeBtn.Padding = new Padding(0, 0, 30, 0);
            homeBtn.Size = new Size(106, 86);
            homeBtn.SizeMode = PictureBoxSizeMode.Zoom;
            homeBtn.TabIndex = 6;
            homeBtn.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI Black", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Transparent;
            label1.Location = new Point(693, 12);
            label1.Name = "label1";
            label1.Size = new Size(266, 65);
            label1.TabIndex = 5;
            label1.Text = "Navigator";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI Black", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Transparent;
            label2.Location = new Point(204, 125);
            label2.Name = "label2";
            label2.Size = new Size(372, 65);
            label2.TabIndex = 7;
            label2.Text = "Select 2 points";
            // 
            // ShortestPathForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackgroundImage = Properties.Resources.firstScreen;
            ClientSize = new Size(1920, 1080);
            Controls.Add(label2);
            Controls.Add(homeBtn);
            Controls.Add(label1);
            Controls.Add(roundedButton1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ShortestPathForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = " ";
            WindowState = FormWindowState.Maximized;
            Load += ShortestPathForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)homeBtn).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Model.RoundedButton roundedButton1;
        private PictureBox homeBtn;
        private Label label1;
        private Label label2;
    }
}