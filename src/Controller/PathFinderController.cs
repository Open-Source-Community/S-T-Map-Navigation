using System;
using Map_Creation_Tool.src.Model;

namespace Map_Creation_Tool.src.Controller
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
        Controller.PathType pathType;

        public PathFinderController(int fromX, int fromY, int toX, int toY, int pathType)
        {
            this.fromX = fromX;
            this.fromY = fromY;
            this.toX = toX;
            this.toY = toY;
            this.pathType = (Controller.PathType)pathType;
        }

        public void pathfinder()
        {

            XY_Point fromPoint = new(fromX, fromY);
            XY_Point toPoint = new(toX, toY);


            //first we need to get the nearest walkable point to the from point and to point
            fromPoint = findNearestWalkablePoint(fromPoint);
            if (fromPoint == null)
                throw new Exception("Invalid from point when find nearest walkable point fromPoint");

            toPoint = findNearestWalkablePoint(toPoint);
            if (toPoint == null)
                throw new Exception("Invalid to point when find nearest walkable point toPoint");


            PathFinder finder = new(fromPoint, toPoint, pathType);
            finder.findPath();
            //finder.showPath();
        }

        public XY_Point findNearestWalkablePoint(XY_Point fromPoint)
        {
            PixelType cellType = getCellType(fromPoint.X, fromPoint.Y);
            if (getCellType(fromPoint.X, fromPoint.Y) != PixelType.OBSTACLE
                && getCellType(fromPoint.X, fromPoint.Y) != PixelType.PLACE)
                return fromPoint;

            //run the bfs algorithm to find the nearest walkable point
            Queue<XY_Point> queue = new();
            HashSet<XY_Point> visited = new();
            visited.Add(fromPoint);
            queue.Enqueue(fromPoint);

            while (queue.Count > 0)
            {
                XY_Point point = queue.Dequeue();

                if (getCellType(point.X, point.Y) != PixelType.OBSTACLE
                    && getCellType(point.X, point.Y) != PixelType.PLACE)
                    return point;

                for (int i = 0; i < 8; i++)
                {
                    int x = point.X + PathFinder.dx[i];
                    int y = point.Y + PathFinder.dy[i];
                    if (0 <= x && x < Database.Instance.ImagePixelsWidth
                        && 0 <= y && y < Database.Instance.ImagePixelsHeight
                        && !visited.Contains(new XY_Point(x, y)))
                    {
                        visited.Add(new XY_Point(x, y));
                        queue.Enqueue(new XY_Point(x, y));
                    }

                }

            }
            return null;
        }

        public static PixelType getCellType(int x, int y)
        {
            Color cell = Database.Instance[x, y];

            if (cell == Color.White)
                return PixelType.PLACE;
            else if (cell == Color.Gray)
                return PixelType.REGULAR_PATH;
            else if (cell == Color.Orange)
                return PixelType.BUSY_PATH;
            else if (cell == Color.Red)
                return PixelType.VERY_BUSY_PATH;



            return PixelType.OBSTACLE;
        }



    }
}