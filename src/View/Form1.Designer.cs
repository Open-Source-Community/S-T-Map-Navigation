namespace Map_Creation_Tool.src.View
{
    partial class OpenForm
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
			pictureBox1 = new PictureBox();
			button1 = new Button();
			label1 = new Label();
			panel1 = new Panel();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			panel1.SuspendLayout();
			SuspendLayout();
			// 
			// pictureBox1
			// 
			pictureBox1.Location = new Point(31, 68);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(491, 265);
			pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			// 
			// button1
			// 
			button1.BackColor = SystemColors.HotTrack;
			button1.FlatStyle = FlatStyle.Popup;
			button1.ForeColor = SystemColors.Control;
			button1.Location = new Point(226, 383);
			button1.Name = "button1";
			button1.Size = new Size(94, 29);
			button1.TabIndex = 1;
			button1.Text = "load image";
			button1.UseVisualStyleBackColor = false;
			button1.Click += button1_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.ForeColor = SystemColors.WindowText;
			label1.Location = new Point(252, 439);
			label1.Name = "label1";
			label1.Size = new Size(0, 20);
			label1.TabIndex = 2;
			// 
			// panel1
			// 
			panel1.BackColor = SystemColors.GradientInactiveCaption;
			panel1.Controls.Add(pictureBox1);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(button1);
			panel1.Dock = DockStyle.Left;
			panel1.Location = new Point(0, 0);
			panel1.Margin = new Padding(0);
			panel1.Name = "panel1";
			panel1.Size = new Size(568, 600);
			panel1.TabIndex = 4;
			// 
			// OpenForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			AutoSize = true;
			ClientSize = new Size(1006, 600);
			Controls.Add(panel1);
			Name = "OpenForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Map Creation Tool";
			WindowState = FormWindowState.Maximized;
			Load += OpenForm_Load;
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private PictureBox pictureBox1;
		private Button button1;
		private Label label1;
		private Panel panel1;
	}
}
