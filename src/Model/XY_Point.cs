using System;
namespace Map_Creation_Tool.src.Model
{
	public class XY_Point
	{
		private int x, y;
		public int X
        {
            get { return x; }
        }
        public int Y
        {
            get { return y; }
        }
        public XY_Point(int X , int Y)
		{
			this.x = X;
			this.y = Y;
		}
	}
}
