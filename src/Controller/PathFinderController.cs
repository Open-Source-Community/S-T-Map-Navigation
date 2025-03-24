using System;
using Map_Creation_Tool.src.Model;

namespace Map_Creation_Tool.src.Model
{

    /* Take the from , to points and traffic option from view
     * validate the input
     * call the pathfinder
     */
    public enum PathType
    {
        SHORTEST_PATH,
        FASTEST_PATH
    }

    public class PathFinderController
    {
        private int fromX, fromY, toX, toY;
        PathType pathType;

        public PathFinderController(int fromX , int fromY , int toX , int toY , PathType pathType)
        {
            this.fromX = fromX;
            this.fromY = fromY;
            this.toX = toX;
            this.toY = toY;
            this.pathType = pathType;
        }

        public void pathfinder()
        {
            
            XY_Point fromPoint = new(fromX, fromY);
            XY_Point toPoint = new(toX, toY);
 
            PathFinder finder = new(fromPoint , toPoint , pathType);

            finder.findPath();
            //finder.showPath();
        }

    }
}