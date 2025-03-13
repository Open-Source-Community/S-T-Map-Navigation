using System;
namespace Map_Creation_Tool.src.Model
{
    public class XY_Point
    {
        private int x, y;
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public XY_Point(int X, int Y)
        {
            this.x = X;
            this.y = Y;
        }

        public override bool Equals(object? obj)
        {
            XY_Point point = (XY_Point)obj;
            return this.x == point.x && this.y == point.y;
        }
    }

    
}
