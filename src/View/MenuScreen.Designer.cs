namespace Map_Creation_Tool.src.View
{
    partial class MenuScreen
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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(192, 192, 255);
            button1.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(148, 13);
            button1.Name = "button1";
            button1.Size = new Size(520, 334);
            button1.TabIndex = 0;
            button1.Text = "How to use";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.PaleGreen;
            button2.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            button2.Location = new Point(730, 24);
            button2.Name = "button2";
            button2.Size = new Size(520, 334);
            button2.TabIndex = 1;
            button2.Text = "Upload Your Map";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.Red;
            button3.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.Location = new Point(730, 444);
            button3.Name = "button3";
            button3.Size = new Size(520, 294);
            button3.TabIndex = 3;
            button3.Text = "Exit";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(128, 128, 255);
            button4.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button4.Location = new Point(148, 433);
            button4.Name = "button4";
            button4.Size = new Size(520, 294);
            button4.TabIndex = 2;
            button4.Text = "Draw Your Map";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // MenuScreen
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackgroundImage = Properties.Resources._2;
            ClientSize = new Size(1918, 1078);
            Controls.Add(button3);
            Controls.Add(button4);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.None;
            MaximumSize = new Size(1920, 1080);
            MinimumSize = new Size(1918, 1078);
            Name = "MenuScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MenuScreen";
            WindowState = FormWindowState.Maximized;
            Load += MenuScreen_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}