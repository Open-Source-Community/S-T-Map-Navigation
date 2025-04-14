using System;
using System.Diagnostics.Eventing.Reader;
using Map_Creation_Tool.src.Controller;
namespace Map_Creation_Tool.src.Model
{
    public enum PixelType : byte
    {
        PLACE = 1,
        REGULAR_PATH,
        BUSY_PATH,
        VERY_BUSY_PATH,
        OBSTACLE = 255,
    }

    /* Generate the shortest path between two points based on user option
     * show the path in the view
	 */
    public class PathFinder
    {
        private static readonly int INF = 1_000_000_00;
        private List<(int x, int y)> fromPoints;
        private HashSet<(int x, int y)> toPoints;
        private Controller.PathType pathType;
        private List<(int x, int y)> path;

        //Directions of path finding
        public static readonly int[] dx = { 0, 1, 0, -1, 1, 1, -1, -1 };
        public static readonly int[] dy = { 1, 0, -1, 0, 1, -1, 1, -1 };

        //Weight of the cells
        private static readonly byte OBSTACLE = byte.MaxValue;
        private static readonly byte PLACE = byte.MaxValue;
        private static readonly byte REGULAR_PATH = 1;        // Gray
        private static readonly byte BUSY_PATH = 4;           // Orange
        private static readonly byte VERY_BUSY_PATH = 8;      // Red

        public static (int R, int G, int B) PLACE_COLOR = (238, 232, 232);
        public static (int R, int G, int B) REGULAR_PATH_COLOR = (189, 198, 197);
        public static (int R, int G, int B) BUSY_PATH_COLOR = (162, 193, 221);
        public static (int R, int G, int B) VERY_BUSY_PATH_COLOR = (255, 0, 0);
        public static (int R, int G, int B) OBSTACLE_COLOR = (255, 255, 255); //white

        public PathFinder(List<(int x, int y)> fromPoints, List<(int x, int y)> toPoints, Controller.PathType pathType)
        {
            this.fromPoints = fromPoints;
            this.toPoints = new HashSet<(int x, int y)>();
            foreach (var point in toPoints)
            {
                this.toPoints.Add((point.x, point.y));
            }
            this.pathType = pathType;
            Path = new List<(int x, int y)>();
        }

        public List<(int x, int y)> Path { set { this.path = value; } get { return this.path; } }

        //To use it in the priority queue in A-star algorithm
        class Node : IComparable<Node>
        {
            public (int x, int y) Position { get; set; }
            public int Cost { get; set; }
            public int Heuristic { get; set; }
            public int TotalCost => Cost + Heuristic;
            public Node Parent { get; set; }

            public int CompareTo(Node other) => TotalCost.CompareTo(other.TotalCost);

            public Node((int x, int y) point, int cost, int heuristic)
            {
                Position = point;
                Cost = cost;
                Heuristic = heuristic;
            }
        }

        //Calculate the euclidean distance between cur node and the end node (heuristic function)
        private int euclidean_distance((int x, int y) from, (int x, int y) to)
        {
            return (int)Math.Sqrt(Math.Pow(from.x - to.x, 2) + Math.Pow(from.y - to.y, 2));
        }

        public static bool isValid(int x, int y, int rows, int cols)
        {
            return 0 <= x && x < rows && 0 <= y && y < cols;
        }

        public List<(int x, int y)> findPath()
        {
            if (fromPoints.Count == 0 || toPoints.Count == 0)
            {
                return new List<(int x, int y)>();
            }
            //MessageBox.Show("Finding path...");
            //we need to confirm the colors and it's weight
            int rows = Database.Instance.GridWidth;
            int cols = Database.Instance.GridHeight;
            //MessageBox.Show($"GridWidth::{rows} , GridHeight::{cols}");

            int[,] len = new int[rows, cols];
            Dictionary<(int x, int y), (int x, int y)> parent = new Dictionary<(int x, int y), (int x, int y)>();
            PriorityQueue<Node, int> pq = new PriorityQueue<Node, int>();
            bool[,] visited = new bool[rows, cols];

            //initialize the len array with INF value
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    len[i, j] = INF;
                }
            }

            foreach (var fromPoint in fromPoints)
            {
                foreach (var toPoint in toPoints)
                {
                    pq.Enqueue(new Node(fromPoint, 0, euclidean_distance(fromPoint, toPoint)), 0);
                }
                len[fromPoint.x, fromPoint.y] = 0;
                parent[(fromPoint.x, fromPoint.y)] = (-1, -1);
            }

            Node curNode = default;
            while (pq.Count > 0)
            {
                curNode = pq.Dequeue();

                if (toPoints.Contains((curNode.Position.x, curNode.Position.y)))
                    break;

                if (visited[curNode.Position.x, curNode.Position.y])
                    continue;

                visited[curNode.Position.x, curNode.Position.y] = true;

                for (int i = 0; i < dx.Length; i++)
                {
                    int nX = curNode.Position.x + dx[i];
                    int nY = curNode.Position.y + dy[i];
                    if (isValid(nX, nY, rows, cols) && (Database.Instance[nX, nY].Type == CellType.Exit || Database.Instance[nX, nY].Type == CellType.Walkable))
                    {
                        int newLen = len[curNode.Position.x, curNode.Position.y] + (pathType == Controller.PathType.SHORTEST_PATH && Database.Instance[nX, nY].Type == CellType.Walkable ? 1 : Database.Instance[nX, nY].Weight);
                        if (newLen < len[nX, nY])
                        {
                            len[nX, nY] = newLen;

                            (int x, int y) curPoint = (nX, nY);
                            foreach (var toPoint in toPoints)
                            {
                                pq.Enqueue(new Node(curPoint, newLen, euclidean_distance(curPoint, toPoint)), newLen);
                            }
                            parent[(curPoint.x, curPoint.y)] = (curNode.Position.x, curNode.Position.y);
                        }
                    }
                }
            }

            return buildPath(ref parent, (curNode.Position.x, curNode.Position.y));
            // MessageBox.Show("Path found!");
        }

        public List<(int x, int y)> buildPath(ref Dictionary<(int x, int y), (int x, int y)> parent, (int x, int y) to)
        {
            (int x, int y) curPoint = (to.x, to.y);
            if (path.Count > 0)
                path.Clear();

            //MessageBox.Show($"to->{to.x}::{to.y}");

            //MessageBox.Show("starting path build from " + curPoint.x + " " + curPoint.y + "");

            //foreach (var entry in parent)
            //{
            //    System.Windows.MessageBox.Show(entry.Key.X + " " + entry.Key.Y + " " + entry.Value.X + " " + entry.Value.Y);    
            //}


            while (curPoint.x != -1 && curPoint.y != -1)
            {
                path.Add(curPoint);
                //MessageBox.Show("adding " + curPoint.X + " " + curPoint.Y + "");
                curPoint = parent[curPoint];
            }

            //if (curPoint.X == fromPoint.X )
            //{
            //    path.Add(fromPoint);  // Include the starting point
            //    Console.WriteLine($"Reached the start point: ({fromPoint.X}, {fromPoint.Y})");
            //}
            //else
            //{
            //    Console.WriteLine("Failed to construct a valid path. Path may not exist.");
            //    MessageBox.Show("Path does not exist!");
            //    return;
            //} 

            path.Reverse();
            //MessageBox.Show("final path count: " + path.Count + "");
            return path;
        }

    }
}