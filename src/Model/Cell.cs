using System;

namespace Map_Creation_Tool.src.Model
{
    public struct Cell
    {
        byte R , G , B;

        public Cell(int R , int G , int B)
        {
            this.R = (byte)R;
            this.G = (byte)G;
            this.B = (byte)B;
        }
    }
}
