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
        public static Database Instance { get; } = new Database();   
        private Database()
        {
        }
    }
}