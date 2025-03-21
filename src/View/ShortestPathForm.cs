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
        List<XY_Point> xY_Points = [
          new XY_Point(40, 40), new XY_Point(50, 50),new XY_Point(60, 60),new XY_Point(0, 0),new XY_Point(10, 10),new XY_Point(20, 20),new XY_Point(30, 30)
            ];
        public ShortestPathForm()
        {
            InitializeComponent();
        }

        public void DrawCirclesOnImage(List<XY_Point> list)
        {

            if (pictureBox2.Image == null)
            {
                MessageBox.Show("No image loaded in PictureBox2!");
                return;
            }

            Bitmap updatedImage = new Bitmap(pictureBox2.Image);
            using (Graphics g = Graphics.FromImage(updatedImage))
            {
                Brush brush = new SolidBrush(Color.Blue);
                int cellSize = 10;



                foreach (var item in list)
                {
                    Rectangle r = new Rectangle(item.X, item.Y, cellSize, cellSize);
                    g.FillEllipse(brush, r);
                }


            }

            pictureBox2.Image = updatedImage;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";



            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
                Bitmap img = (Bitmap)pictureBox1.Image;
                pictureBox2.Image = pictureBox1.Image;
            }


        }


        private void shortest_path_form_Load(object sender, EventArgs e)
        {

        }



        private void button2_Click(object sender, EventArgs e)
        {
            DrawCirclesOnImage(xY_Points);

        }

        private void ShortestPathForm_Load(object sender, EventArgs e)
        {

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