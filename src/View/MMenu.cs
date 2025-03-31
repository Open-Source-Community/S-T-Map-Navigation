using Map_Creation_Tool.src.Controller;
using Map_Creation_Tool.src.Model;
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
        private string BACKGROUND_PATH = "../../../res/Assets/firstScreen.png";
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

            this.Text = "Map Navigator";
            this.Size = new Size(1920, 1080);
            this.FormBorderStyle = FormBorderStyle.None;
            this.KeyPreview = true;


            if (File.Exists(BACKGROUND_PATH))
                {
                    this.BackgroundImage = Image.FromFile(BACKGROUND_PATH);
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }

             logoBox = new Logo();
            logoBox.Location = new Point(this.Location.X / 20, this.Location.Y/1);
           




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
                MenuButton button = new MenuButton(buttonTexts[i], i);

                    button.Location = new Point(50, 40 + i * 80);

             
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
                    GuideForm guideForm= new GuideForm();
                    this.Hide();
                    guideForm.ShowDialog();
                    this.Show();
                    break;
                case 1: // Use Your Map

                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";


                    ImageConverterController imageConverter = new ImageConverterController();
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {

                        imageConverter.takeImage(new Bitmap(ofd.FileName));
                        imageConverter.convertImage();

                        ShortestPathForm shortestPathForm = new ShortestPathForm();
                        this.Hide();
                        shortestPathForm.ShowDialog();
                        this.Show();
                    }

                    

                    break;
                case 2: // Draw Your Map
                  MapCreationForm mapCreationForm = new MapCreationForm();
                    this.Hide();
                    mapCreationForm.ShowDialog();
                    this.Show();
                    

                    break;
                case 3: // Exit
                    Application.Exit();
                    break;
            }
        }

      
    }
}
