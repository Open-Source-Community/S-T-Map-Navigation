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
    public partial class GuideForm : Form
    {

        private string BACKGROUND_PATH ="../../../res/Assets/firstScreen.png";
        private Image homeIcon = Image.FromFile("../../../res/Assets/home.png");
        private Image rightArrowIcon = Image.FromFile("../../../res/Assets/right arrow.png");
        private Image leftArrowIcon = Image.FromFile("../../../res/Assets/left arrow.png");

        private PictureBox stepImageBox;
        private PictureBox logoBox;
        private MenuButton backButton;
        private Label titleLabel;
        private Label stepNumberLabel;
        private Label stepDescriptionLabel;
        private int currentStep = 1;
        private const int TotalSteps = 4;
        Panel contentPanel;
        private MenuButton nextSlideButton;
        private MenuButton previousSlideButton;
        Label stepTitleLabel;


        private Image[] images = {

            Image.FromFile("../../../res/Assets/guide.jpg"),
            Image.FromFile("../../../res/Assets/guide 1.png"),
              Image.FromFile("../../../res/Assets/guide.jpg"),
            Image.FromFile("../../../res/Assets/guide 1.png"),

        };
       private string[] stepTitles = {
    "Draw or Upload Your Map",
    "Select Start and End Points",
    "Choose Route Type",
    "Visualize the Route"
};

private string[] stepDescriptions = {
    "Start by either drawing your map using the editor or uploading a pre-existing map with specific RGB values. The system will process the uploaded map and convert it into a usable format.",

    "Select two points on the map—a starting point and a destination. Click on 'Pick Points' and choose two locations on the grid to define the route.",

    "Decide whether you want the cheapest or fastest route. The system will calculate the optimal path based on your selection, taking into account the map details.",

    "Once the route is calculated, the system will visually display it by drawing circles along the path on the screen, making it easy to follow."
};

        public GuideForm()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            SuspendLayout();
            ClientSize = new Size(1920, 1080);
            Name = "GuideForm";
            Text = "GuideForm";
            Load += GuideForm_Load;
            this.Resize += (s, e) => AdjustLayout();

            ResumeLayout(false);
            currentStep = 1;
            if (File.Exists(BACKGROUND_PATH))
            {
                this.BackgroundImage = Image.FromFile(BACKGROUND_PATH);
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                MessageBox.Show("Background image not found: " + BACKGROUND_PATH, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.FormBorderStyle = FormBorderStyle.None;
            this.KeyPreview = true;
            logoBox = new Logo();
           
            this.Controls.Add(logoBox);
            backButton= new MenuButton("Back To Main Menu",0,homeIcon);
            backButton.Location = new Point(10, 20);
            backButton.Click += backButton_Click;
            this.Controls.Add(backButton);

            titleLabel = new Label
            {
                Text = "How To Use Map Navigator",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true

            };

            this.Controls.Add(titleLabel);


            contentPanel = new Panel
            {
                Size = new Size(900, 530),
                BackColor = Color.FromArgb(100, 20, 20, 60),
                Padding = new Padding(20)
            };
            this.Controls.Add(contentPanel);
            stepNumberLabel = new Label
            {
                Text = $"STEP {currentStep} OF {TotalSteps}",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 180, 180),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(200, 40),
                Location = new Point(350, 20)
            };

            contentPanel.Controls.Add(stepNumberLabel);



            stepImageBox = new PictureBox
            {
                Size = new Size(400, 250),
                Location = new Point(250, 110),
                BackColor = Color.FromArgb(60, 30, 100),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = images[0]
            };
            contentPanel.Controls.Add(stepImageBox);




            nextSlideButton = new MenuButton("Next", 0,rightArrowIcon);

            previousSlideButton = new MenuButton("Previous", 0, leftArrowIcon);
            nextSlideButton.Location = new Point(600, 450);
            previousSlideButton.Location = new Point(100, 450);
            previousSlideButton.Height = nextSlideButton.Height = 55;
            previousSlideButton.Width = nextSlideButton.Width = 180;
            nextSlideButton.ImageAlign = ContentAlignment.MiddleRight;
nextSlideButton.TextAlign = ContentAlignment.MiddleLeft;
            contentPanel.Controls.Add(nextSlideButton);

            contentPanel.Controls.Add(previousSlideButton);

            nextSlideButton.Click += NextSlideButton_Click;
            previousSlideButton.Click += PreviousSlideButton_Click;
            previousSlideButton.Enabled = false; 




            stepTitleLabel = new Label
            {
                Text = stepTitles[0],
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(500, 40),
                Location = new Point(250, 60)
            };
            contentPanel.Controls.Add(stepTitleLabel);





            stepDescriptionLabel = new Label
            {
                Text = stepDescriptions[0],
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.TopLeft,
                Size = new Size(860, 100),
                Location = new Point(20, 370)
            };
            contentPanel.Controls.Add(stepDescriptionLabel);



        }

        private void PreviousSlideButton_Click(object? sender, EventArgs e)
        {
            if (currentStep > 1)
            {
                currentStep--;
                stepNumberLabel.Text = $"STEP {currentStep} OF {TotalSteps}";
                stepTitleLabel.Text = stepTitles[currentStep - 1];
                stepDescriptionLabel.Text = stepDescriptions[currentStep - 1];
                stepImageBox.Image = images[currentStep - 1];
                // disable 
                previousSlideButton.Enabled = currentStep > 1;
                nextSlideButton.Enabled = true; // Enable "Next" button
            }
        }

        private void NextSlideButton_Click(object? sender, EventArgs e)
        {
            if (currentStep < TotalSteps)
            {
                currentStep++;
                stepNumberLabel.Text = $"STEP {currentStep} OF {TotalSteps}";
                stepTitleLabel.Text = stepTitles[currentStep - 1];
                stepDescriptionLabel.Text= stepDescriptions[currentStep - 1];
                stepImageBox.Image= images[currentStep - 1];

                //disable button
                nextSlideButton.Enabled = currentStep < TotalSteps;
                previousSlideButton.Enabled = true;

            }
         
        }

        private void AdjustLayout()
        {
            // Center the content panel
            contentPanel.Size = new Size((int)(ClientSize.Width * 0.7), (int)(ClientSize.Height * 0.7));
            contentPanel.Location = new Point((ClientSize.Width - contentPanel.Width) / 2, (ClientSize.Height - contentPanel.Height) / 2 + 30);

            // Title Label
            titleLabel.Location = new Point((ClientSize.Width - titleLabel.Width) / 2, ClientSize.Height / 10);

            // Step Number Label
            stepNumberLabel.Size = new Size(contentPanel.Width / 3, contentPanel.Height / 10);
            stepNumberLabel.Location = new Point(contentPanel.Width / 3, contentPanel.Height / 25);

            // Step Image Box
            stepImageBox.Size = new Size(contentPanel.Width / 2, contentPanel.Height / 2);
            stepImageBox.Location = new Point((contentPanel.Width - stepImageBox.Width) / 2 , contentPanel.Height / 5);

            // Step Title Label
            stepTitleLabel.Size = new Size(contentPanel.Width * 3 / 4, contentPanel.Height / 10);
            stepTitleLabel.Location = new Point((contentPanel.Width - stepTitleLabel.Width) / 2, stepNumberLabel.Bottom - 15);

            // Step Description Label
            stepDescriptionLabel.Size = new Size(contentPanel.Width - 40, contentPanel.Height / 4);
            stepDescriptionLabel.Location = new Point(20, stepImageBox.Bottom + 10);

            // Navigation Buttons
            int buttonWidth = contentPanel.Width / 4;
            int buttonHeight = contentPanel.Height / 10;

            previousSlideButton.Size = nextSlideButton.Size = new Size(buttonWidth, buttonHeight);
            previousSlideButton.Location = new Point(20, contentPanel.Height - buttonHeight - 20);
            nextSlideButton.Location = new Point(contentPanel.Width - buttonWidth - 20, contentPanel.Height - buttonHeight - 20);

            // Logo Position
            logoBox.Location = new Point(this.ClientSize.Width - logoBox.Width - 30, 10);
        }


        private void backButton_Click(object sender, EventArgs e) {
            this.Close();
        }
        private void GuideForm_Load(object sender, EventArgs e)
        {
            AdjustLayout();
        }

      

        
    }
}
