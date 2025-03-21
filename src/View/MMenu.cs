using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Map_Creation_Tool.src.View
{
    public partial class MMenu : Form
    {
        private Panel buttonPanel;
        private Label titleLabel;
        private PictureBox logoBox;

        public MMenu()
        {
            InitializeComponent();
            this.Load += MMenu_Load;
            this.Resize += MMenu_Resize;
        }


        private void InitializeComponent()
        {
            // Form settings
            this.Text = "Map Navigator";
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.KeyPreview = true;
          
            // Set background image
           
                string bgPath = @"D:\OSC\S&T\Map Navigator\res\Assets\firstScreen.png";
                if (File.Exists(bgPath))
                {
                    this.BackgroundImage = Image.FromFile(bgPath);
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }
            

            // Create logo
            logoBox = new PictureBox
            {
                Size = new Size(120, 120),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };
                // Create a compass logo as a bitmap
                Bitmap logoBitmap = new Bitmap(120, 120);
                using (Graphics g = Graphics.FromImage(logoBitmap))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.Clear(Color.Transparent);

                    // Draw outer circle
                    using (Pen pen = new Pen(Color.White, 3))
                    {
                        g.DrawEllipse(pen, 10, 10, 100, 100);
                    }

                    // Draw compass points
                    string[] directions = { "N", "E", "S", "W" };
                    Point[] positions = {
                        new Point(60, 20),  // N
                        new Point(100, 60), // E
                        new Point(60, 100), // S
                        new Point(20, 60)   // W
                    };

                    for (int i = 0; i < 4; i++)
                    {
                        using (Font font = new Font("Arial", 14, FontStyle.Bold))
                        using (SolidBrush brush = new SolidBrush(Color.White))
                        {
                            SizeF size = g.MeasureString(directions[i], font);
                            g.DrawString(directions[i], font, brush,
                                positions[i].X - size.Width / 2,
                                positions[i].Y - size.Height / 2);
                        }
                    }

                    // Draw compass needle
                    using (Pen redPen = new Pen(Color.Red, 2))
                    using (Pen whitePen = new Pen(Color.White, 2))
                    {
                        g.DrawLine(redPen, 60, 60, 60, 25); // North (red)
                        g.DrawLine(whitePen, 60, 60, 60, 95); // South (white)
                        g.DrawLine(whitePen, 60, 60, 25, 60); // West (white)
                        g.DrawLine(whitePen, 60, 60, 95, 60); // East (white)
                    }

                    // Draw center circle
                    using (SolidBrush brush = new SolidBrush(Color.White))
                    {
                        g.FillEllipse(brush, 55, 55, 10, 10);
                    }
                }
                logoBox.Image = logoBitmap;
            
           

            this.Controls.Add(logoBox);

            // Add title label
            titleLabel = new Label
            {
                Text = "MAP NAVIGATOR",
                Font = new Font("Segoe UI", 36, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true
            };
            this.Controls.Add(titleLabel);

            // Create semi-transparent panel for buttons
            buttonPanel = new Panel
            {
                Size = new Size(400, 400),
                BackColor = Color.FromArgb(100, 20, 20, 60),
                Padding = new Padding(20)
            };
            this.Controls.Add(buttonPanel);

            // Create buttons
            string[] buttonTexts = { "How to Use", "Use Your Map", "Draw Your Map", "Exit" };
            for (int i = 0; i < buttonTexts.Length; i++)
            {
                Button button = new Button
                {
                    Text = buttonTexts[i],
                    Size = new Size(300, 60),
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(80, 40, 120),
                    ForeColor = Color.White,
                    Cursor = Cursors.Hand,
                    TabIndex = i,
                    Tag = i
                };

                button.FlatAppearance.BorderColor = Color.FromArgb(150, 100, 200);
                button.FlatAppearance.BorderSize = 2;
                button.Location = new Point(50, 40 + i * 80);

                // Add hover effects
                button.MouseEnter += (s, e) =>
                {
                    button.BackColor = Color.FromArgb(120, 60, 180);
                    button.FlatAppearance.BorderColor = Color.FromArgb(200, 180, 255);
                };
                button.MouseLeave += (s, e) =>
                {
                    button.BackColor = Color.FromArgb(80, 40, 120);
                    button.FlatAppearance.BorderColor = Color.FromArgb(150, 100, 200);
                };

                // Add click handler
                button.Click += Button_Click;

                buttonPanel.Controls.Add(button);
            }
        }

        private void MMenu_Load(object sender, EventArgs e)
        {
            CenterControls();
        }

        private void MMenu_Resize(object sender, EventArgs e)
        {
            CenterControls();
        }

        private void CenterControls()
        {
            int offset = 50; // Adjust this value as needed to move everything down

            // Center the button panel
            buttonPanel.Location = new Point(
                (this.ClientSize.Width - buttonPanel.Width) / 2,
                (this.ClientSize.Height - buttonPanel.Height) / 2 + 30 + offset
            );

            // Position title above button panel
            titleLabel.Location = new Point(
                (this.ClientSize.Width - titleLabel.Width) / 2,
                buttonPanel.Location.Y - titleLabel.Height - 30
            );

            // Position logo above title
            logoBox.Location = new Point(
                (this.ClientSize.Width - logoBox.Width) / 2,
                titleLabel.Location.Y - logoBox.Height - 20
            );
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int buttonIndex = (int)clickedButton.Tag;

            switch (buttonIndex)
            {
                case 0: // How to Use
                    MessageBox.Show("Map Navigator Help:\n\n" +
                        "- Use Your Map: Load and navigate an existing map\n" +
                        "- Draw Your Map: Create a new custom map\n" +
                        "- Press ESC to exit the application");
                    break;
                case 1: // Use Your Map
                    MessageBox.Show("Loading map navigation module...");
                    // Code to open map navigation form would go here
                    break;
                case 2: // Draw Your Map
                    MessageBox.Show("Loading map creation module...");
                    // Code to open map drawing form would go here
                    break;
                case 3: // Exit
                    if (MessageBox.Show("Are you sure you want to exit?", "Confirm Exit",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Application.Exit();
                    }
                    break;
            }
        }

      
    }
}
