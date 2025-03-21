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
            pictureBox2 = new PictureBox();
            button1 = new Button();
            button2 = new Button();
            roundedButton1 = new Model.RoundedButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(93, 168);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(565, 615);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox2.Location = new Point(1223, 168);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(600, 615);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(184, 821);
            button1.Name = "button1";
            button1.Size = new Size(425, 91);
            button1.TabIndex = 2;
            button1.Text = "Upload your Image ";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.Location = new Point(1261, 821);
            button2.Name = "button2";
            button2.Size = new Size(425, 91);
            button2.TabIndex = 3;
            button2.Text = "Choose from google maps";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // roundedButton1
            // 
            roundedButton1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            roundedButton1.BackColor = Color.Red;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Arial", 10F, FontStyle.Bold);
            roundedButton1.ForeColor = Color.Black;
            roundedButton1.Location = new Point(725, 12);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(430, 108);
            roundedButton1.TabIndex = 4;
            roundedButton1.Text = "Back";
            roundedButton1.UseVisualStyleBackColor = false;
            roundedButton1.Click += roundedButton1_Click;
            // 
            // ShortestPathForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1920, 1080);
            Controls.Add(roundedButton1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ShortestPathForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = " ";
            WindowState = FormWindowState.Maximized;
            Load += ShortestPathForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Button button1;
        private Button button2;
        private Model.RoundedButton roundedButton1;
    }
}