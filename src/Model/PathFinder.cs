using System;
namespace Model
{
    /* Generate the shortest path between two points based on user option
     * show the path in the view
	 */
    public class PathFinder
	{
        private Point fromPoint, toPoint;
        ArrayList<Point> path;
        public PathFinder(Point fromPoint , Point toPoint)
		{
            this.fromPoint = fromPoint;
            this.toPoint = toPoint;
        }

        //Calculate the shortest path between two points
        /* generate the shortest path between two points in ArrayList
         */
        public void findPath()
		{
            //call database to get the grid
            //find the shortest path using A-star algorithm
        }

        //call the view to show the path
        public void showPath()
        {

        }
    }
}
