
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
namespace Map_Creation_Tool.src.View
{
	public partial class OpenForm : Form
	{
		public OpenForm()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
				openFileDialog.Title = "Select an Image";

				// If the user selects a file and clicks OK
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						// Load the selected image into the PictureBox
						pictureBox1.Image = new Bitmap(openFileDialog.FileName);

						// Process the image using ImageConverter class
						Mat matFromImage = Model.ImageConverter.GetMatFromSDImage(pictureBox1.Image);
						(List<List<Rgb>> list, string str) = Model.ImageConverter.ConvertImageToGrid(matFromImage);
						label1.Text = str;


					}
					catch (Exception ex)
					{
						label1.Text = "something went wrong";

					}
				}
			}
		}

		
	}
}
