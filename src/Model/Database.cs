using System;
using System.Runtime.CompilerServices;
namespace Map_Creation_Tool.src.Model
{
    /*Contain any data that needs to be stored
     */

    internal class Database
    {
        private static Bitmap imagePixels;
        private static Image curMapImage;
        
        public Bitmap ImagePixels
        {
            set { imagePixels = value; }
        }

        public Image CurMapImage
        {
            set { curMapImage = value; }
        }

        public int ImagePixelsWidth
        {
            get
            {
                return imagePixels.Width;
            }
        }

        public int ImagePixelsHeight
        {
            get
            {
                return imagePixels.Height;
            }
        }

        //indexer to get a pixel
        public Color this[int x, int y]
        {
            get
            {
                return imagePixels.GetPixel(x , y);
            }
        }

        public static Database Instance { get; } = new Database();   
        private Database()
        {
        }
    }
}