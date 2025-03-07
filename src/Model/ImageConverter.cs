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
			new Rgb(238,232,232), //obstacle
			new Rgb(242,203,170), //medium traffic orange
			new Rgb(255, 0, 0) ,//medium traffic red
            new Rgb(189,198,197) ,//regular path: grey
            new Rgb(162,193,221) ,//busy path: blue
        };
		//public ImageConverter()
		//{

		//}
		public static Mat loadImage(string filepath)
		{
			return CvInvoke.Imread(filepath, ImreadModes.Color);
		}
		
		public static bool IdentifyColor(Rgb rgbColor, HashSet<Rgb> colors )
		{
			int threshold = 30;
			for (int i = 0; i < colors.Count; i++)
			{
				double distance = CalculateEuclideanDistance(rgbColor, colors.ElementAt(i));

				if (distance <= threshold)
				{
					return true;
				}
			}

			return false; // No match found

		}
		static double CalculateEuclideanDistance(Rgb rgbColor, Rgb colors)
		{
			int R1 = (int)colors.Red, G1 = (int)colors.Green, B1 = (int)colors.Blue;
			int R2 = (int)rgbColor.Red, G2 = (int)rgbColor.Green, B2 = (int)rgbColor.Blue;

			// Euclidean distance formula
			return Math.Sqrt(Math.Pow(R1 - R2, 2) + Math.Pow(G1 - G2, 2) + Math.Pow(B1 - B2, 2));
		}

		public static Mat validateImgChannels(Mat image)
		{
			using (Mat convertedImage = new Mat())
			{
				// Convert the image to 8-bit 3-channel format if necessary
				if (image.Depth != DepthType.Cv8U || image.NumberOfChannels != 3)
				{
					if (image.NumberOfChannels == 1)
					{
						// Convert grayscale to 3-channel
						CvInvoke.CvtColor(image, convertedImage, ColorConversion.Gray2Rgb);
					}
					else
					{
						// Convert to 8-bit 3-channel
						image.ConvertTo(convertedImage, DepthType.Cv8U, 1.0, 0.0);
						if (convertedImage.NumberOfChannels != 3)
						{
							CvInvoke.CvtColor(convertedImage, convertedImage, ColorConversion.Bgra2Rgb);
						}
					}
					image = convertedImage;
				}
			}
			return image;
		}
		public static (bool, string) validateImage(Mat image)
			{
				if (image == null || image.IsEmpty)
					return (false, "Image not found");

				Mat rgbImage = new Mat();

				Image<Rgb, byte> img = rgbImage.ToImage<Rgb, byte>();
				//to allow pixel manipulation

				//bool hasSource = false; //if there at least one red pixel (255,0,0) exists which represents the source
			int countColors = 0; 
				for (int y = 0; y < img.Height; y++)
				{
					for (int x = 0; x < img.Width; x++)
				{
						Rgb pixelColor = img[y, x];

						//if (pixelColor.Equals(new Rgb(255, 0, 0)))
						//	hasSource = true;
						if (IdentifyColor(pixelColor, Allowedcolors))
							countColors++ ;
					
					}
				}
				//if (!hasSource)
				//	return (false, "Image does not contain a source");
				
				if (countColors < (img.Cols * img.Rows)/8)
				return (true, "Image doesnot have enough allowed colors");

			return (true, "Image meets the constraints");
			}

		public static (List<List<MCvScalar>>?, string) ConvertImageToGrid(Mat image,int k)
		{
			try
			{
				var (isValid, message) = validateImage(image);
				if (!isValid)
					return (null, message);
				image = validateImgChannels(image);


				// Get original dimensions
				int height =image.Rows;
			int width =image.Cols;

			// Compute new dimensions
			int newHeight = height / k;
			int newWidth = width / k;

			List<List<MCvScalar>> grid = new List<List<MCvScalar>>();
			//empty list called grid store each row of pixels as a list of rgb values
			
				Mat rgbImage = new Mat(image.Size, DepthType.Cv8U, 3);

				// Iterate over the original image in steps of k
				//GRID CREATION
				for (int y = 0; y < newHeight; y++)
				{
					List<MCvScalar> row = new List<MCvScalar>();
					for (int x = 0; x < newWidth; x++)
					{
						// Extract the k x k square
						int roiX = x * k;
						int roiY = y * k;
						int roiWidth = k;
						int roiHeight = k;

						// Adjust ROI dimensions if it exceeds image bounds
						if (roiX + roiWidth > width)
							roiWidth = width - roiX; 
						if (roiY + roiHeight > height)
							roiHeight = height - roiY; 

						// Create a new Rectangle with the adjusted dimensions
						Rectangle roi = new Rectangle(roiX, roiY, roiWidth, roiHeight);

						using (Mat square = new Mat(image, roi))
						{
							if (square.IsEmpty)
							{
								return (null, "The 'square' matrix is empty.");
							}

							// Compute the average RGB value for the square
							MCvScalar averageColor = CvInvoke.Mean(square);
							row.Add(averageColor);
						}
					}
					grid.Add(row);
				}
				
				return (grid, "Image converted to grid");

			}catch (Exception ex)
			{
	
				return (null, $"An exception occurred: {ex.Message}");
			}

			
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
//public static (List<List<Rgb>>, string) ConvertImageToGrid(Mat image)
//{
//	var (isValid, message) = validateImage(image);
//	if (!isValid)
//		return (null, message);

//	List<List<Rgb>> grid = new List<List<Rgb>>();
//	//empty list called grid store each row of pixels as a list of rgb values

//	Mat rgbImage = new Mat();

//	//OpenCV stores images in BGR format by default. The method converts the image to RGB
//	CvInvoke.CvtColor(image, rgbImage, ColorConversion.Bgr2Rgb);

//	Image<Rgb, byte> img = rgbImage.ToImage<Rgb, byte>();//For pixel level access

//	//GRID CREATION
//	for (int y = 0; y < img.Height; y++)
//	{
//		List<Rgb> row = new List<Rgb>();
//		for (int x = 0; x < img.Width; x++)
//		{
//			row.Add(img[y, x]);
//		}
//		grid.Add(row);
//	}
//	return (grid, "Image converted to grid");

//}

