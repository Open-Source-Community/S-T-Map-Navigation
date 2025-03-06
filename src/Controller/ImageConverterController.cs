using Emgu.CV.Dnn;
using Emgu.CV.Structure;                            
using System;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Map_Creation_Tool.src.Model;

namespace Controller
{
	/* Get map image from view
	 * check if the image is valid
	 * if valid, call the image converter
	 * else call the error message
	 */
	public class ImageConverterController
	{
		private Map_Creation_Tool.src.Model.ImageConverter _imageConverter;

		public ImageConverterController()
		{
			_imageConverter = new Map_Creation_Tool.src.Model.ImageConverter();
		}
		public (List<List<Rgb>>, string) processImage(string filepath)
		{
			Mat image = _imageConverter.loadImage(filepath);
			if (image.IsEmpty)
				return (null, "Image not found");
			var (grid, conversionMessage) = _imageConverter.ConvertImageToGrid(image);

			if (grid == null)
				return (null, conversionMessage);

			return (grid, "Image converted to grid");
		}

	}
}