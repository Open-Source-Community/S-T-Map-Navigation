using System;
using Map_Creation_Tool.src.Model;


/*
 Base Cases:

 */



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
        PathType pathType;

        public PathFinderController(int fromX , int fromY , int toX , int toY , PathType pathType)
        {
            this.fromX = fromX;
            this.fromY = fromY;
            this.toX = toX;
            this.toY = toY;
            this.pathType = pathType;
        }

        public List<XY_Point> pathfinder()
        {
            
            XY_Point fromPoint = findNearestWalkablePoint(new XY_Point(fromX , fromY));
            Color tmp1 = Database.Instance[fromPoint.X, fromPoint.Y];
            MessageBox.Show($"FromPoint::{fromPoint.X} , {fromPoint.Y}");
            XY_Point toPoint = findNearestWalkablePoint(new XY_Point(toX, toY));
            Color tmp2 = Database.Instance[toPoint.X, toPoint.Y];

            MessageBox.Show($"toPoint::{toPoint.X} , {toPoint.Y}");

            PathFinder finder = new(fromPoint , toPoint , pathType);

            return finder.findPath();
            //finder.showPath();
        }



        public XY_Point findNearestWalkablePoint(XY_Point fromPoint)
        {
            PixelType cellType = getCellType(fromPoint.X, fromPoint.Y);
            if (cellType != PixelType.OBSTACLE
                && cellType != PixelType.PLACE)
                return fromPoint;

            //run the bfs algorithm to find the nearest walkable point
            Queue<(int x , int y)> queue = new();
            HashSet<(int x , int y)> visited = new();
            visited.Add((fromPoint.X , fromPoint.Y));
            queue.Enqueue((fromPoint.X, fromPoint.Y));

            while (queue.Count > 0)
            {
                (int x , int y) point = queue.Dequeue();
                PixelType curCellType = getCellType(point.x, point.y);
                if (curCellType != PixelType.OBSTACLE
                    && curCellType != PixelType.PLACE)
                    return new XY_Point(point.x , point.y);

                for (int i = 0; i < 8; i++)
                {
                    int x = point.x + PathFinder.dx[i];
                    int y = point.y + PathFinder.dy[i];
                    if (0 <= x && x < Database.Instance.ImagePixelsWidth
                        && 0 <= y && y < Database.Instance.ImagePixelsHeight
                        && !visited.Contains((x , y)))
                    {
                        visited.Add((x , y));
                        queue.Enqueue((x , y));
                    }

                }

            }
            return null;
        }

        public static PixelType getCellType(int x, int y)
        {
            Color cell = Database.Instance[x, y];

            if (cell == Color.FromArgb(PathFinder.PLACE_COLOR.R, PathFinder.PLACE_COLOR.G, PathFinder.PLACE_COLOR.B))
                return PixelType.PLACE;
            else if (cell == Color.FromArgb(PathFinder.REGULAR_PATH_COLOR.R, PathFinder.REGULAR_PATH_COLOR.G, PathFinder.REGULAR_PATH_COLOR.B))
                return PixelType.REGULAR_PATH;
            else if (cell == Color.FromArgb(PathFinder.BUSY_PATH_COLOR.R, PathFinder.BUSY_PATH_COLOR.G, PathFinder.BUSY_PATH_COLOR.B))
                return PixelType.BUSY_PATH;
            else if (cell == Color.FromArgb(PathFinder.VERY_BUSY_PATH_COLOR.R, PathFinder.VERY_BUSY_PATH_COLOR.G, PathFinder.VERY_BUSY_PATH_COLOR.B))
                return PixelType.VERY_BUSY_PATH;

            return PixelType.OBSTACLE;
        }



    }

}
