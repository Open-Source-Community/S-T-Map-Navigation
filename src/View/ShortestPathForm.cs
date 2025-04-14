using Map_Creation_Tool.src.Controller;
using Map_Creation_Tool.src.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Map_Creation_Tool.src.View
{
    public partial class ShortestPathForm : Form
    {
        PictureBox pictureBox2 = new PictureBox();
        private MenuButton backButton;
        Image homeIcon = Image.FromFile("../../../res/Assets/home.png");

        PictureBox logoBox = new Logo();
        List<(int x, int y)> xY_Points = [
          (40, 40),
            (50, 50),
            (60, 60),
            (0, 0),
            (10, 10),
            (20, 20),
            (30, 30)
            ];

        private (int x, int y) startpoint;
        private int image_x;
        private int image_y;
        private (int x, int y) endpoint;
        private bool startpointSelected = false;

        public ShortestPathForm()
        {
            InitializeComponent();
            pictureBox1.Image = Database.Instance.CurMapImage;
            pictureBox1.Click += new EventHandler(PictureBox1_Click);

        }
        // MessageBox
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            //XY_Point  clickedPoint= new XY_Point(me.X, me.Y);
            if (pictureBox1.Image != null)
            {
                double scaleX = (double)pictureBox1.Image.Width / pictureBox1.Width;
                double scaleY = (double)pictureBox1.Image.Height / pictureBox1.Height;
                image_x = (int)(me.X * scaleX);
                image_y = (int)(me.Y * scaleY);
            }


            if (startpointSelected)
            {
                endpoint = (image_x, image_y);
                MessageBox.Show("End Point Selected");
                //MessageBox.Show($"end Point: {endpoint.x} :: {endpoint.y}");
                FindandDrawPath();
                startpointSelected = false;
            }
            else
            {
                startpoint = (image_x, image_y);
                startpointSelected = true;
                MessageBox.Show("Start Point Selected");
                //MessageBox.Show($"Start Point: {startpoint.x}::{startpoint.y}");
            }

        }
        private void FindandDrawPath()
        {
            using (PathTypeDialog dialog = new PathTypeDialog())
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                Controller.PathType selectedPathType = (dialog.SelectedChoice == PathTypeDialog.PathChoice.FASTEST)
                    ? Controller.PathType.FASTEST_PATH
                    : Controller.PathType.SHORTEST_PATH;

                PathFinderController pathFinderController = new PathFinderController();

                while (!pathFinderController.validatePoints(startpoint.x, startpoint.y, endpoint.x, endpoint.y, selectedPathType))
                {
                    MessageBox.Show("Invalid Points. Please select place or walkable point.");
                    return;
                }

                List<(int x, int y)> path = pathFinderController.pathfinder();

                if (path.Count > 0)
                {
                    PathPrinter pathPrinter = new PathPrinter(path);
                    pathPrinter.printPath();
                    DrawCirclesOnImage(path);
                }
                else
                {
                    MessageBox.Show("No Path Found");
                }
            }
        }
        public void DrawCirclesOnImage(List<(int x, int y)> list)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("No Image");
                return;
            }

            Bitmap updatedImage = new Bitmap(pictureBox1.Image);
            using (Graphics g = Graphics.FromImage(updatedImage))
            {
                Brush brush = new SolidBrush(Color.Yellow);
                int cellSize = 20;
                foreach (var item in list)
                {
                    Rectangle r = new Rectangle(item.x - cellSize / 2, item.y - cellSize / 2, cellSize, cellSize);
                    g.FillEllipse(brush, r);
                }
            }
            pictureBox1.Image = updatedImage;
            pictureBox1.Refresh();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|.jpg;.jpeg;.png;.gif;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
                //  Bitmap img = (Bitmap)pictureBox1.Image;
                // pictureBox2.Image = pictureBox1.Image;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DrawCirclesOnImage(xY_Points);
        }
        private void ShortestPathForm_Load(object sender, EventArgs e)
        {
            //DrawCirclesOnImage(xY_Points);
        }
        private void roundedButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
        private void InitializeComponent()
        {
            this.Resize += (sender, e) => AdjustLayout();
            logoBox = new Logo();
            logoBox.Location = new Point(this.ClientSize.Width - logoBox.Width - 60, 60);
            this.Resize += (sender, e) =>
            {
                logoBox.Location = new Point(this.ClientSize.Width - logoBox.Width - 20, 20);
            };

            this.Controls.Add(logoBox);




            backButton = new MenuButton("Back To Main Menu", 0, homeIcon);
            backButton.Location = new Point(10, 20);
            backButton.Click += backButton_Click;
            this.Controls.Add(backButton);



            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShortestPathForm));
            pictureBox1 = new PictureBox();
            //  roundedButton1 = new Model.RoundedButton();
            homeBtn = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)homeBtn).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            //  pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Cursor = Cursors.Cross;
            pictureBox1.Location = new Point(273, 137);
            pictureBox1.Margin = new Padding(2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1280, 720);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click_1;
            MakePictureBoxRounded(pictureBox1, 20);

            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI Black", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Transparent;
            label1.Location = new Point(729, 7);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(179, 45);
            label1.TabIndex = 5;
            label1.Text = "Navigator";
            // 
            // label2
            // 
            this.Resize += (sender, e) =>
            {
                label2.Location = new Point((this.ClientSize.Width - label2.Width) / 2, 20); // Keep centered at top
            };

            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI Black", 50F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Transparent;
            label2.Location = new Point((this.ClientSize.Width - label2.Width) / 2, 20); // Center horizontally
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(249, 45);
            label2.TabIndex = 7;
            label2.Text = "Select 2 points";
            this.Controls.Add(label2);


            // 
            // ShortestPathForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1904, 1041);
            Controls.Add(label2);

            Controls.Add(label1);

            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(2);
            Name = "ShortestPathForm";
            Text = " ";
            Load += ShortestPathForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)homeBtn).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }
        private void MakePictureBoxRounded(PictureBox pic, int cornerRadius)
        {
            using (GraphicsPath path = GetRoundedRectPath(pic.ClientRectangle, cornerRadius))
            {
                pic.Region = new Region(path);
            }
        }
        private GraphicsPath GetRoundedRectPath(Rectangle bounds, int cornerRadius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = cornerRadius * 2;
            Rectangle arc = new Rectangle(bounds.Location, new Size(diameter, diameter));

            // Top left arc
            path.AddArc(arc, 180, 90);

            // Top right arc
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Bottom right arc
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Bottom left arc
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void AdjustLayout()
        {
            // Center label2 horizontally at top
            if (label2 != null)
            {
                label2.Location = new Point((ClientSize.Width - label2.Width) / 2, 20);
            }

            // Position logoBox near top-right (make sure logoBox is a field, not a local variable)
            if (logoBox != null)
            {
                logoBox.Location = new Point(ClientSize.Width - logoBox.Width - 30, 20);
            }



            // pictureBox1 stays centered horizontally
            if (pictureBox1 != null)
            {
                pictureBox1.Location = new Point((ClientSize.Width - pictureBox1.Width) / 2, pictureBox1.Location.Y);
            }

            // Center label1 if you want too
            if (label1 != null)
            {
                label1.Location = new Point((ClientSize.Width - label1.Width) / 2, label1.Location.Y);
            }
        }

    }
}