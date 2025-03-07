
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using static System.Net.Mime.MediaTypeNames;
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

						//send image to controller to process
						var (grid, conversionMessage) = Controller.ImageConverterController.processImage(pictureBox1.Image);
						label1.Text = conversionMessage;


					}
					catch (Exception ex)
					{
						label1.Text = $"An exception occurred: {ex.Message}";

					}
				}
			}
		}

		private void OpenForm_Load(object sender, EventArgs e)
		{

		}

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{

		}
	}
}
