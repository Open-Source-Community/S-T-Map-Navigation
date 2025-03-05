using System;
using Map_Creation_Tool.src.Model;
namespace Map_Creation_Tool.src.Model
{
    /* Generate the shortest path between two points based on user option
     * show the path in the view
	 */
    public class PathFinder
	{ 
        private static readonly int INF = 1_000_000_00;
        private XY_Point fromPoint, toPoint;
        private static List<XY_Point> path;

        public PathFinder(XY_Point fromPoint , XY_Point toPoint)
		{
            this.fromPoint = fromPoint;
            this.toPoint = toPoint;
            
        }
        
        private int[] dx = { 0, 1, 0, -1 , 1, 1, -1, -1};
        private int[] dy = { 1, 0, -1, 0 , 1 ,-1 , 1, -1};

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

        //Calaculate the weight of the cell based on RGB values
        public int calculateWeight(int row , int col)
        {
            return 0;
        }

        private bool isValid(int x, int y , int rows ,int cols)
        {
            return 0 <= x && x < rows && 0 <= y && y < cols;
        }


        public void findPath()
        {
            //we need to confirm the colors and it's weight

            int rows = Database.Instance.GridWidth;
            int cols = Database.Instance.GridHeight;

            int[,] len = new int[rows, cols];
            Dictionary<XY_Point, XY_Point> parent = new Dictionary<XY_Point, XY_Point>();
            PriorityQueue<Node, int> pq = new PriorityQueue<Node, int>();

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
                
                for (int i = 0; i < 8; i++)
                {
                    int nX = curNode.point.X + dx[i];
                    int nY = curNode.point.Y + dy[i];
                    if (isValid(nX , nY , rows , cols) /* && this cell not wall*/)
                    {
                        int newLen = len[curNode.point.X, curNode.point.Y] + calculateWeight(nX , nY);
                        if (newLen < len[nX, nY])
                        {
                            len[nX, nY] = newLen;

                            XY_Point curPoint = new XY_Point(nX, nY);
                            pq.Enqueue(new Node(curPoint, newLen, euclidean_distance(curPoint, toPoint)), newLen);

                            parent.Add(curPoint, curNode.point);
                        }
                    }
                }
            }

            buildPath(ref parent);
        }

        public void buildPath(ref Dictionary<XY_Point , XY_Point> parent)
        {
            XY_Point curPoint = toPoint;
            while (curPoint.X != -1 && curPoint.Y != -1)
            {
                path.Add(curPoint);
                curPoint.X = parent[curPoint].X;
                curPoint.Y = parent[curPoint].Y;
            }

            path.Reverse();
        }

        //call the view to show the path
        public void showPath()
        {

        }
    }
}
