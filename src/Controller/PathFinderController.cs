//using System;
using Map_Creation_Tool.src.Model;

namespace Map_Creation_Tool.src.Controller
{
    /* Take the from , to points and traffic option from view
     * validate the input
     * call the pathfinder
     */

    public class PathFinderController
    {
        int fromX, fromY, toX, toY;

        public PathFinderController()
        {
                
        }

        public void validatePoints()
        {
            //take the input from view
            //validate the input
        }

        public void callPathFinder()
        {
            XY_Point fromPoint = new(fromX, fromY);
            XY_Point toPoint = new(toX, toY);
            
            PathFinder finder = new(fromPoint , toPoint);

            finder.findPath();
            finder.showPath();
        }

    }
}