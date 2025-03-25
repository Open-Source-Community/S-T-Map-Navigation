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
            if (path == null || path.Count == 0)
            {
                MessageBox.Show("No path");
                return;
            }

            string pathdetails = "path has " + path.Count + " points";
            MessageBox.Show("for each point in the path, print the X and Y coordinates, separated by a space:");
            foreach (XY_Point point in path)
            {
                pathdetails += " " + point.X + " " + point.Y;
                //Console.WriteLine("X: " + point.X + " Y: " + point.Y);
            }
            MessageBox.Show(pathdetails);
        }
    }
}
