using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography.Xml;
using System.Windows.Controls;
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

        public PathFinderController()
        {
            //this.fromX = fromX;
            //this.fromY = fromY;
            //this.toX = toX;
            //this.toY = toY;
            //this.pathType = pathType;
        }

        public bool validatePoints(int fromX, int fromY, int toX, int toY, PathType pathType)
        {
            if ((Database.Instance[fromX, fromY].Type == CellType.Walkable ||
                   Database.Instance[fromX, fromY].Type == CellType.Exit ||
                   Database.Instance[fromX, fromY].Type == CellType.Place)
                   &&
                   (Database.Instance[toX, toY].Type == CellType.Walkable ||
                Database.Instance[toX, toY].Type == CellType.Exit ||
                Database.Instance[toX, toY].Type == CellType.Place))
            {
                this.fromX = fromX;
                this.fromY = fromY;
                this.toX = toX;
                this.toY = toY;
                this.pathType = pathType;
                return true;
            }
            return false;
        }

        public List<(int x, int y)> pathfinder()
        {

            //XY_Point fromPoint = findNearestWalkablePoint(new XY_Point(fromX , fromY));
            //XY_Point toPoint = findNearestWalkablePoint(new XY_Point(toX, toY));
            //List<(int x, int y)> fromPoints = findAllPossibleNearstWalkablePoints((fromX, fromY));
            //List<(int x, int y)> toPoints = findAllPossibleNearstWalkablePoints((toX, toY));

            //for testing
            //Color tmp1 = Database.Instance[fromPoint.X, fromPoint.Y];
            MessageBox.Show($"FromPoint::{fromX} , {fromY}");
            //Color tmp2 = Database.Instance[toPoint.X, toPoint.Y];
            MessageBox.Show($"toPoint::{toX} , {toY}");

            bool isFromPointPath = Database.Instance[fromX, fromY].Type == CellType.Walkable || Database.Instance[fromX, fromY].Type == CellType.Exit;
            bool isToPointPath = Database.Instance[toX, toY].Type == CellType.Walkable || Database.Instance[toX, toY].Type == CellType.Exit;

            PathFinder finder = new(isFromPointPath ? new List<(int x, int y)>() { (fromX, fromY) } : Database.Instance[Database.Instance[(fromX, fromY)]],
                isToPointPath ? new List<(int x, int y)>() { (toX, toY) } : Database.Instance[Database.Instance[(toX, toY)]], pathType);

            return finder.findPath();
            //finder.showPath();
        }



        //public List<(int x, int y)> findAllPossibleNearstWalkablePoints((int x , int y) fromPoint)
        //{
        //    List<(int x, int y)> points = new();
        //    if (Database.Instance[fromPoint.x , fromPoint.y].Type == CellType.Walkable)
        //    {
        //        points.Add(fromPoint);
        //        return points;
        //    }

        //    points = GetPlaceRegion(fromPoint);

        //    List<(int x, int y)> walkableCells = new();

        //    foreach ((int x , int y) placeCell in points)
        //    {
        //        for (int i = 0; i < PathFinder.dx.Length; i++)
        //        {
        //            int nx = placeCell.x + PathFinder.dx[i];
        //            int ny = placeCell.y + PathFinder.dy[i];
        //            if (PathFinder.isValid(nx, ny, Database.Instance.GridWidth, Database.Instance.GridHeight)
        //                && Database.Instance[nx,ny].Type == CellType.Walkable)
        //            {
        //                walkableCells.Add((nx, ny));
        //            }
        //        }
        //    }

        //    return walkableCells;
        //}

        //private List<(int x, int y)> GetPlaceRegion((int x , int y) fromPoint)
        //{
        //    List<(int x, int y)> placeRegion = new();
        //    Queue<(int x, int y)> queue = new();
        //    HashSet<(int x ,int y)> visited = new();

        //    queue.Enqueue(fromPoint);
        //    visited.Add((fromPoint.x , fromPoint.y));

        //    while (queue.Count > 0)
        //    {
        //        (int x , int y) current = queue.Dequeue();
        //        placeRegion.Add(current);

        //        for (int i = 0; i < PathFinder.dx.Length; i++)
        //        {
        //            int nx = current.x + PathFinder.dx[i];
        //            int ny = current.y + PathFinder.dy[i];
        //            (int x , int y) neighbor = (nx, ny);

        //            if (PathFinder.isValid(nx, ny, Database.Instance.GridWidth, Database.Instance.GridHeight)
        //                && Database.Instance[nx, ny].Type == CellType.Place 
        //                && !visited.Contains((neighbor.x, neighbor.y)))
        //            {
        //                queue.Enqueue(neighbor);
        //                visited.Add((neighbor.x , neighbor.y));
        //            }
        //        }
        //    }

        //    return placeRegion;
        //}

        //public static PixelType getCellType(int x, int y)
        //{
        //    Color cell = Database.Instance[x, y];

        //    if (cell == Color.FromArgb(PathFinder.PLACE_COLOR.R, PathFinder.PLACE_COLOR.G, PathFinder.PLACE_COLOR.B))
        //        return PixelType.PLACE;
        //    else if (cell == Color.FromArgb(PathFinder.REGULAR_PATH_COLOR.R, PathFinder.REGULAR_PATH_COLOR.G, PathFinder.REGULAR_PATH_COLOR.B))
        //        return PixelType.REGULAR_PATH;
        //    else if (cell == Color.FromArgb(PathFinder.BUSY_PATH_COLOR.R, PathFinder.BUSY_PATH_COLOR.G, PathFinder.BUSY_PATH_COLOR.B))
        //        return PixelType.BUSY_PATH;
        //    else if (cell == Color.FromArgb(PathFinder.VERY_BUSY_PATH_COLOR.R, PathFinder.VERY_BUSY_PATH_COLOR.G, PathFinder.VERY_BUSY_PATH_COLOR.B))
        //        return PixelType.VERY_BUSY_PATH;

        //    return PixelType.OBSTACLE;
        //}



    }

}
