using System;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Map_Creation_Tool.src.Model
{
    /* Get map image from own controller
     * Convert the image to a grid of cells
     * Set the grid and the image in database 
     * Show interactive map in view
     */
		public static class ImageConverter
		{
			private static readonly HashSet<Rgb> Allowedcolors = new HashSet<Rgb>
		{
			new Rgb(0, 0, 0), //obstacle
            new Rgb(255, 0, 0) ,//source: red
            new Rgb(0,255,0) ,//regular path: green
            new Rgb(0,0,255) ,//busy path: blue
        };
		//public ImageConverter()
		//{

		//}
		public static Mat loadImage(string filepath)
		{
			return CvInvoke.Imread(filepath, ImreadModes.Color);
		}

		public static (bool, string) validateImage(Mat image)
			{
				if (image == null || image.IsEmpty)
					return (false, "Image not found");

				Mat rgbImage = new Mat();
				CvInvoke.CvtColor(image, rgbImage, ColorConversion.Bgr2Rgb);

				Image<Rgb, byte> img = rgbImage.ToImage<Rgb, byte>();
				//to allow pixel manipulation

				bool hasSource = false; //if there at least one red pixel (255,0,0) exists which represents the source
				for (int y = 0; y < img.Height; y++)
				{
					for (int x = 0; x < img.Width; x++)
					{
						Rgb pixelColor = img[y, x];

						if (pixelColor.Equals(new Rgb(255, 0, 0)))
							hasSource = true;
					if (!Allowedcolors.Contains(pixelColor))
						return (false, "Image contains invalid colors");
				}
				}
				if (!hasSource)
					return (false, "Image does not contain a source");

				return (true, "Image meets the constraints");
			}

		public static (List<List<Rgb>>, string) ConvertImageToGrid(Mat image)
			{
				var (isValid, message) = validateImage(image);
				if (!isValid)
					return (null, message);

				List<List<Rgb>> grid = new List<List<Rgb>>();
				//empty list called grid store each row of pixels as a list of rgb values

				Mat rgbImage = new Mat();

				//OpenCV stores images in BGR format by default. The method converts the image to RGB
				CvInvoke.CvtColor(image, rgbImage, ColorConversion.Bgr2Rgb);
			
				Image<Rgb, byte> img = rgbImage.ToImage<Rgb, byte>();//For pixel level access

			    //GRID CREATION
				for (int y = 0; y < img.Height; y++)
				{
					List<Rgb> row = new List<Rgb>();
					for (int x = 0; x < img.Width; x++)
					{
						row.Add(img[y, x]);
					}
					grid.Add(row);
				}
				return (grid, "Image converted to grid");

			}

		public static Mat GetMatFromSDImage(System.Drawing.Image image)
		{
			int stride = 0;
			Bitmap bmp = new Bitmap(image);

			System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
			System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);

			System.Drawing.Imaging.PixelFormat pf = bmp.PixelFormat;
			if (pf == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
			{
				stride = bmp.Width * 4;
			}
			else
			{
				stride = bmp.Width * 3;
			}

			Image<Bgra, byte> cvImage = new Image<Bgra, byte>(bmp.Width, bmp.Height, stride, (IntPtr)bmpData.Scan0);

			bmp.UnlockBits(bmpData);

			return cvImage.Mat;
		}


	}
}

