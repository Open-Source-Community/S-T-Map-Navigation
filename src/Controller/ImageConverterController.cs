using Emgu.CV.Dnn;
using Emgu.CV.Structure;                            
using System;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Map_Creation_Tool.src;
using System.Windows.Forms;


namespace Map_Creation_Tool.src.Controller
{
	/* Get map image from view
	 * check if the image is valid
	 * if valid, call the image converter
	 * else call the error message
	 */
	public class ImageConverterController
	{
		//private Map_Creation_Tool.src.Model.ImageConverter _imageConverter;

		//public ImageConverterController()
		//{
		//	_imageConverter = new Map_Creation_Tool.src.Model.ImageConverter();
		//}
		public static (List<List<Rgb>>?, string) processImage(System.Drawing.Image image)
		{
			try
			{
				if (image == null)
					return (null, "Image not found");

				// Process the image using ImageConverter class
				Mat matFromImage = Map_Creation_Tool.src.Model.ImageConverter.GetMatFromSDImage(image);
				var (grid, conversionMessage) = Map_Creation_Tool.src.Model.ImageConverter.ConvertImageToGrid(matFromImage);

				if (grid == null)
					return (null, conversionMessage);

				return (grid, "Image converted to grid");
			}catch(Exception ex)
			{
				return (null, "exception"); ;
			}
			
		}

	}
}