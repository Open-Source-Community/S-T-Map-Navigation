﻿using System;
using System.Diagnostics.Eventing.Reader;
using Map_Creation_Tool.src.Model;
namespace Map_Creation_Tool.src.Model
{
    enum PixelType: byte
    {
        PLACE = 1,
        REGULAR_PATH = 1,
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
        private PathType pathType;
        private  List<XY_Point> path;

        //Directions of path finding
        private static readonly int[] dx = { 0, 1, 0, -1 , 1, 1, -1, -1};
        private static readonly int[] dy = { 1, 0, -1, 0 , 1 ,-1 , 1, -1};

        //Weight of the cells
        private static readonly byte OBSTACLE = byte.MaxValue;  // Blocked cells
        private static readonly byte REGULAR_PATH = 1;        // Gray
        private static readonly byte BUSY_PATH = 2;           // Orange
        private static readonly byte VERY_BUSY_PATH = 4;      // Red
        private static readonly byte PLACE = 1;               // White

        public PathFinder(XY_Point fromPoint , XY_Point toPoint , PathType pathType)
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

            if(curPixel == Color.Red)
            {
                return pathType == PathType.FASTEST_PATH? (byte)PixelType.VERY_BUSY_PATH : (byte)PixelType.REGULAR_PATH;
            }
            else if (curPixel == Color.Orange)
            {
                return pathType == PathType.FASTEST_PATH ? (byte)PixelType.BUSY_PATH : (byte)PixelType.REGULAR_PATH;
            }
            else if (curPixel == Color.Gray)
            {
                return (byte)PixelType.REGULAR_PATH;
            }
            else if (curPixel == Color.White)
            {
                return (byte)PixelType.PLACE;
            }
            else
            {
                return (byte)PixelType.OBSTACLE;
            }
        }

        //Assert the boundaries and the cell is not an obstacle
        private bool isValid(int x, int y , int rows ,int cols)
        {
            return 0 <= x && x < rows && 0 <= y && y < cols && calculateWeight(x , y) != OBSTACLE;
        }

        public void findPath()
        {
            MessageBox.Show("Finding path...");
            //we need to confirm the colors and it's weight
            int rows = Database.Instance.ImagePixelsWidth;
            int cols = Database.Instance.ImagePixelsHeight;

            int[,] len = new int[rows, cols];
            Dictionary<XY_Point, XY_Point> parent = new Dictionary<XY_Point, XY_Point>();
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
            parent[fromPoint] = new XY_Point(-1, -1);

            while (pq.Count > 0)
            {
                Node curNode = pq.Dequeue();

                //Reach the target break then build the path
                if (curNode.point.Equals(toPoint))
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
                            parent[curPoint] = curNode.point;
                        }
                    }
                    else
                    {
                       // MessageBox.Show("Invalid move to " + nX + " " + nY );
                        string reason = "";
                        if (nX < 0 || nX >= rows || nY < 0 || nY >= cols)
                            reason = "Out of map boundaries.";
                        else if (calculateWeight(nX, nY) == OBSTACLE)
                            reason = "Blocked by an obstacle.";
        
                        MessageBox.Show($"Invalid move to ({nX}, {nY}). Reason: {reason}");}
                }
            }

            buildPath(ref parent);
           // MessageBox.Show("Path found!");
        }

        public void buildPath(ref Dictionary<XY_Point , XY_Point> parent)
        {
            XY_Point curPoint = toPoint;
            path.Clear();
            MessageBox.Show("starting path build from " + curPoint.X + " " + curPoint.Y + "");
            foreach (var entry in parent)
            {
                System.Windows.MessageBox.Show(entry.Key.X + " " + entry.Key.Y + " " + entry.Value.X + " " + entry.Value.Y);    
            }
            while (parent.ContainsKey(curPoint)&&curPoint.X != -1 && curPoint.Y != -1)
            {
                path.Add(curPoint);
                MessageBox.Show("adding " + curPoint.X + " " + curPoint.Y + "");
                curPoint = parent[curPoint];
            }
            
            if (curPoint.X == fromPoint.X )
            {
                path.Add(fromPoint);  // Include the starting point
                Console.WriteLine($"Reached the start point: ({fromPoint.X}, {fromPoint.Y})");
            }
            else
            {
                Console.WriteLine("Failed to construct a valid path. Path may not exist.");
                MessageBox.Show("Path does not exist!");
                return;
            } 

            path.Reverse();
            Console.WriteLine("final path count: " + path.Count + "");
        }

    }
}