using System;

namespace Map_Creation_Tool.src.Model
{
    /* Get map image from own controller
     * Convert the image to bitmap
     * Set the bitmap and the image in database 
     * Show interactive map in view
     */
    public class ImageConverter
    {
        public ImageConverter()
        {

        }

        public void convert(Image image)
        {
            Bitmap imgMap = new Bitmap(image);
            Database.Instance.ImagePixels = imgMap;
            Database.Instance.CurMapImage = image;
        }



        

        
    }
}

