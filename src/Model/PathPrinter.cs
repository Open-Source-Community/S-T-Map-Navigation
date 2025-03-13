using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map_Creation_Tool.src.Model
{
    internal class PathPrinter
    {
        private List<XY_Point> path;

        public PathPrinter(List<XY_Point> path)
        {
            this.path = path;
        }

        //Print the path
        public void printPath()
        {

            foreach (XY_Point point in path)
            {
                Console.WriteLine("X: " + point.X + " Y: " + point.Y);
            }
        }
    }
}
