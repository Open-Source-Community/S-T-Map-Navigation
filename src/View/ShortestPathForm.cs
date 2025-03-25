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
        PictureBox pictureBox2= new PictureBox();
        List<XY_Point> xY_Points = [
          new XY_Point(40, 40),
            new XY_Point(50, 50),
            new XY_Point(60, 60),
            new XY_Point(0, 0),
            new XY_Point(10, 10),
            new XY_Point(20, 20),
            new XY_Point(30, 30)
            ];

        private XY_Point startpoint;
        private int image_x;
        private int image_y;
        private XY_Point endpoint;
        private bool startpointSelected = false;

        public ShortestPathForm()
        {
            InitializeComponent();
            pictureBox1.Image = Database.Instance.CurMapImage;
      pictureBox1.Click+=new EventHandler(PictureBox1_Click);
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
                 image_y=(int)(me.Y*scaleY);
            }
            
            
            if (startpointSelected)
            {
                endpoint =new XY_Point(image_x, image_y); 
              //  MessageBox.Show("End Point Selected");
                MessageBox.Show($"end Point: {endpoint.X}::{endpoint.Y}");
                FindandDrawPath();
                startpointSelected = false;
            }
            else
            {
                startpoint = new XY_Point(image_x, image_y);
                startpointSelected = true;
             //   MessageBox.Show("Start Point Selected");
             MessageBox.Show($"Start Point: {startpoint.X}::{startpoint.Y}");
            }

        }
        private void FindandDrawPath()
        {
            if (startpoint == null || endpoint == null)
            {
                MessageBox.Show("Select Start and End Points");
                return;
            }
            // Ask the user to select the path type
            DialogResult result = MessageBox.Show("Choose Path Type:\nYes - Fastest Path\nNo - Shortest Path", 
                "Path Type Selection", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            PathType selectedPathType = (result == DialogResult.Yes) ? PathType.FASTEST_PATH : PathType.SHORTEST_PATH;
            PathFinder pathFinder = new PathFinder(startpoint, endpoint, selectedPathType);
            pathFinder.findPath();
          
            if(pathFinder.Path.Count>0)
           {
               PathPrinter pathPrinter = new PathPrinter(pathFinder.Path); 
               pathPrinter.printPath();
                DrawCirclesOnImage(pathFinder.Path);
            }
            else 
                MessageBox.Show("No Path Found");
        }
        public void DrawCirclesOnImage(List<XY_Point> list)
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
                    Rectangle r = new Rectangle(item.X-cellSize/2, item.Y-cellSize/2, cellSize, cellSize);
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
    }
}