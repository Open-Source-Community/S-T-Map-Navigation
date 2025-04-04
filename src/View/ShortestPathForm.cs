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
    public partial class ShortestPathForm : Form
    {
        PictureBox pictureBox2 = new PictureBox();
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
                //  MessageBox.Show("End Point Selected");
                MessageBox.Show($"end Point: {endpoint.x} :: {endpoint.y}");
                FindandDrawPath();
                startpointSelected = false;
            }
            else
            {
                startpoint = (image_x, image_y);
                startpointSelected = true;
                //   MessageBox.Show("Start Point Selected");
                MessageBox.Show($"Start Point: {startpoint.x}::{startpoint.y}");
            }

        }
        private void FindandDrawPath()
        {
            //if (startpoint == null || endpoint == null)
            //{
            //    MessageBox.Show("Select Start and End Points");
            //    return;
            //}
            // Ask the user to select the path type
            DialogResult result = MessageBox.Show("Choose Path Type:\nYes - Fastest Path\nNo - Shortest Path",
                "Path Type Selection",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            Controller.PathType selectedPathType = (result == DialogResult.Yes) ? Controller.PathType.FASTEST_PATH : Controller.PathType.SHORTEST_PATH;
            //PathFinder pathFinder = new PathFinder(startpoint, endpoint, selectedPathType);
            //pathFinder.findPath();

            PathFinderController pathFinderController = new PathFinderController(startpoint.x, startpoint.y, endpoint.x, endpoint.y, selectedPathType);
            List<(int x, int y)> path = pathFinderController.pathfinder();

            if (path.Count > 0)
            {
                PathPrinter pathPrinter = new PathPrinter(path);
                pathPrinter.printPath();
                DrawCirclesOnImage(path);
            }
            else
                MessageBox.Show("No Path Found");

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
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

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
    }
}