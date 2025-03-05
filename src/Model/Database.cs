using System;
namespace Map_Creation_Tool.src.Model
{
    /*Contain any data that needs to be stored
     * Administrate files
     */

    internal class Database
    {
        private static Cell[,] curGrid;
        //private static Image curImage;
        public static Cell[,] CurGrid
        {
            get
            {
                return curGrid;
            }
            set
            {
                curGrid = value;
            }
        }

        public int GridWidth
        {
            get
            {
                return curGrid.GetLength(0);
            }
        }

        public int GridHeight
        {
            get
            {
                return curGrid.GetLength(1);
            }
        }

        //indexer to get a cell from the grid
        public Cell this[int x, int y]
        {
            get
            {
                return curGrid[x, y];
            }
        }


        public static Database Instance { get; } = new Database();   
        private Database()
        {
            curGrid = null;
        }
    }
}