using System;
//using Map_Creation_Tool.src.Model;

namespace Map_Creation_Tool.src.Controller
{
    /* Get map image from view
	 * check if the image is valid
	 * if valid, call the image converter
	 * else call the error message
	 */
    public class ImageConverterController
	{
		Image img;
		public ImageConverterController()
		{

		}
		public void takeImage(Image img)
		{
			//Validate the image
			this.img = img;
		}


        public void convertImage()
        {
            //Call the image converter
			Model.ImageConverter imageConverter = new Model.ImageConverter();
            imageConverter.convert(img);
        }
    }
}
