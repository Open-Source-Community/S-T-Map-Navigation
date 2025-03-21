namespace Map_Creation_Tool.src.View
{
    partial class GuideForm
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
            iconButton1 = new FontAwesome.Sharp.IconButton();
            label1 = new Label();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // iconButton1
            // 
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.House;
            iconButton1.IconColor = Color.Black;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.Location = new Point(23, 12);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(167, 61);
            iconButton1.TabIndex = 6;
            iconButton1.Text = "iconButton1";
            iconButton1.TextAlign = ContentAlignment.MiddleLeft;
            iconButton1.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton1.UseVisualStyleBackColor = true;
            iconButton1.Click += iconButton1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(83, 322);
            label1.Name = "label1";
            label1.Size = new Size(290, 48);
            label1.TabIndex = 8;
            label1.Text = "Choose 2 points";
            // 
            // iconButton2
            // 
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.ArrowRight;
            iconButton2.IconColor = Color.Black;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.Location = new Point(1208, 760);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(167, 61);
            iconButton2.TabIndex = 9;
            iconButton2.Text = "iconButton2";
            iconButton2.TextAlign = ContentAlignment.MiddleLeft;
            iconButton2.TextImageRelation = TextImageRelation.TextBeforeImage;
            iconButton2.UseVisualStyleBackColor = true;
            // 
            // iconButton3
            // 
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.ArrowLeft;
            iconButton3.IconColor = Color.Black;
            iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton3.Location = new Point(99, 760);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(167, 61);
            iconButton3.TabIndex = 10;
            iconButton3.Text = "iconButton3";
            iconButton3.TextAlign = ContentAlignment.MiddleLeft;
            iconButton3.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton3.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(99, 126);
            label2.Name = "label2";
            label2.Size = new Size(264, 48);
            label2.TabIndex = 11;
            label2.Text = "RGB = (..........)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(99, 396);
            label3.Name = "label3";
            label3.Size = new Size(190, 48);
            label3.TabIndex = 12;
            label3.Text = "Final PAth";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(83, 211);
            label4.Name = "label4";
            label4.Size = new Size(280, 48);
            label4.TabIndex = 13;
            label4.Text = "Draw your map";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.guide_1;
            pictureBox1.Location = new Point(775, 102);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(600, 416);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 14;
            pictureBox1.TabStop = false;
            // 
            // GuideForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.firstScreen;
            ClientSize = new Size(1898, 1024);
            Controls.Add(pictureBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(iconButton3);
            Controls.Add(iconButton2);
            Controls.Add(label1);
            Controls.Add(iconButton1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "GuideForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GuideForm";
            WindowState = FormWindowState.Maximized;
            Load += GuideForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private FontAwesome.Sharp.IconButton iconButton1;
        private Label label1;
        private FontAwesome.Sharp.IconButton iconButton2;
        private FontAwesome.Sharp.IconButton iconButton3;
        private Label label2;
        private Label label3;
        private Label label4;
        private PictureBox pictureBox1;
    }
}