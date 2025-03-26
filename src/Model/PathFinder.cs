using System;
using System.Diagnostics.Eventing.Reader;
using Map_Creation_Tool.src.Controller;
namespace Map_Creation_Tool.src.Model
{
    public enum PixelType: byte
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
        private XY_Point fromPoint, toPoint;
        private Controller.PathType pathType;
        private List<XY_Point> path;

        //Directions of path finding
        public static readonly int[] dx = { 0, 1, 0, -1 , 1, 1, -1, -1};
        public static readonly int[] dy = { 1, 0, -1, 0 , 1 ,-1 , 1, -1};

        //Weight of the cells
        private static readonly byte OBSTACLE = byte.MaxValue;  // Blocked cells
        private static readonly byte REGULAR_PATH = 1;        // Gray
        private static readonly byte BUSY_PATH = 2;           // Orange
        private static readonly byte VERY_BUSY_PATH = 4;      // Red
        private static readonly byte PLACE = 1;               // White

        public static (int R, int G, int B) PLACE_COLOR = (238 , 232 , 232);
        public static (int R, int G, int B) REGULAR_PATH_COLOR = (189, 198, 197);
        public static (int R, int G, int B) BUSY_PATH_COLOR = (162, 193 , 221);
        public static (int R, int G, int B) VERY_BUSY_PATH_COLOR = (255, 0, 0);
        public static (int R, int G, int B) OBSTACLE_COLOR = (255, 255, 255); //white

        public PathFinder(XY_Point fromPoint , XY_Point toPoint , Controller.PathType pathType)
		{
            this.fromPoint = fromPoint;
            this.toPoint = toPoint;
            this.pathType = pathType;
            Path = new List<XY_Point>(); //initialize the list of points
        }

        public List<XY_Point> Path
        {
            
           set { path = value; }
            get { return path; }
        }

        //To use it in the priority queue in A-star algorithm
        class Node : IComparable<Node>
        {
            public XY_Point point { get; set; }
            public int G { get; set; }
            public int H { get; set; }
            public int Weight { get; set; }     

            public Node(XY_Point point, int g, int h)
            {
                this.point = point; 
                G = g;
                H = h;
                Weight = G + H;
            }
            public int CompareTo(Node other)
            {
                return Weight.CompareTo(other.Weight);
            }
        }

        //Calculate the euclidean distance between cur node and the end node (heuristic function)
        private int euclidean_distance(XY_Point from , XY_Point to)
        {
            return (int)Math.Sqrt(Math.Pow(from.X - to.X, 2) + Math.Pow(from.Y - to.Y, 2));
        } 
         
        //Calaculate the weight of the cell based on Color of pixel and the type of path
        public int calculateWeight(int row , int col)
        {
            Color curPixel = Database.Instance[row,col];

            if(curPixel == Color.FromArgb(VERY_BUSY_PATH_COLOR.R, VERY_BUSY_PATH_COLOR.G, VERY_BUSY_PATH_COLOR.B))
            {
                return pathType == Controller.PathType.FASTEST_PATH? (int)PixelType.VERY_BUSY_PATH : (int)PixelType.REGULAR_PATH;
            }
            else if (curPixel == Color.FromArgb(BUSY_PATH_COLOR.R, BUSY_PATH_COLOR.G, BUSY_PATH_COLOR.B))
            {
                return pathType == Controller.PathType.FASTEST_PATH ? (int)PixelType.BUSY_PATH : (int)PixelType.REGULAR_PATH;
            }
            else if (curPixel == Color.FromArgb(REGULAR_PATH_COLOR.R , REGULAR_PATH_COLOR.G, REGULAR_PATH_COLOR.B))
            {
                return (int)PixelType.REGULAR_PATH;
            }
            else if (curPixel == Color.FromArgb(PLACE_COLOR.R, PLACE_COLOR.G, PLACE_COLOR.B))
            {
                return (int)PixelType.PLACE;
            }
            else
            {
                return (int)PixelType.OBSTACLE;
            }
        }

        //Assert the boundaries and the cell is not an obstacle
        private bool isValid(int x, int y , int rows ,int cols)
        {
            return 0 <= x && x < rows && 0 <= y && y < cols /*&& calculateWeight(x , y) != OBSTACLE && calculateWeight(x , y) != PLACE*/;
        }

        public List<XY_Point> findPath()
        {
            MessageBox.Show("Finding path...");
            //we need to confirm the colors and it's weight
            int rows = Database.Instance.ImagePixelsWidth;
            int cols = Database.Instance.ImagePixelsHeight;

            int[,] len = new int[rows, cols];
            //Dictionary<XY_Point, XY_Point> parent = new Dictionary<XY_Point, XY_Point>();
            Dictionary<(int x, int y) , (int x , int y)> parent = new Dictionary<(int x, int y), (int x, int y)>();
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

            pq.Enqueue(new Node(fromPoint, 0, euclidean_distance(fromPoint, toPoint)), 0);
            len[fromPoint.X, fromPoint.Y] = 0;
            parent[(fromPoint.X , fromPoint.Y)] = (-1, -1);
            Node curNode = default;
            while (pq.Count > 0)
            {
                curNode = pq.Dequeue();

                //Reach the target break then build the path
                if (curNode.point.X == toPoint.X && curNode.point.Y == toPoint.Y)
                    break;

                if (visited[curNode.point.X, curNode.point.Y])
                    continue;

                visited[curNode.point.X, curNode.point.Y] = true;

                for (int i = 0; i < 8; i++)
                {
                    int nX = curNode.point.X + dx[i];
                    int nY = curNode.point.Y + dy[i];
                    if (isValid(nX , nY , rows , cols))
                    {
                        int newLen = len[curNode.point.X, curNode.point.Y] + calculateWeight(nX , nY);
                        if (newLen < len[nX, nY])
                        {
                            len[nX, nY] = newLen;

                            XY_Point curPoint = new XY_Point(nX, nY);
                            pq.Enqueue(new Node(curPoint, newLen, euclidean_distance(curPoint, toPoint)), newLen);
                            parent[(curPoint.X, curPoint.Y)] = (curNode.point.X , curNode.point.Y);
                        }
                    }
                    //else
                    //{
                    //   // MessageBox.Show("Invalid move to " + nX + " " + nY );
                    //    string reason = "";
                    //    if (nX < 0 || nX >= rows || nY < 0 || nY >= cols)
                    //        reason = "Out of map boundaries.";
                    //    else if (calculateWeight(nX, nY) == OBSTACLE)
                    //        reason = "Blocked by an obstacle.";
        
                    //    MessageBox.Show($"Invalid move to ({nX}, {nY}). Reason: {reason}");}
                }
            }

            return buildPath(ref parent , (curNode.point.X , curNode.point.Y));
           // MessageBox.Show("Path found!");
        }

        public List<XY_Point> buildPath(ref Dictionary<(int x , int y) , (int x , int y)> parent , (int x , int y) to)
        {
            (int x , int y) curPoint = (to.x , to.y);
            path.Clear();
            MessageBox.Show($"to->{to.x}::{to.y} and toPoint->{toPoint.X}::{toPoint.Y}");
            //MessageBox.Show("starting path build from " + curPoint.x + " " + curPoint.y + "");

            //foreach (var entry in parent)
            //{
            //    System.Windows.MessageBox.Show(entry.Key.X + " " + entry.Key.Y + " " + entry.Value.X + " " + entry.Value.Y);    
            //}
            while (curPoint.x != -1 && curPoint.y != -1)
            {
                path.Add(new XY_Point(curPoint.x , curPoint.y));
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
            MessageBox.Show("final path count: " + path.Count + "");
            return path;
        }

    }
}