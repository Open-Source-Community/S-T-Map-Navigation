using System;
namespace Model
{
    /*Contain any data that needs to be stored
     * Administrate files
     */

    internal class Database
    {
        public static Database Instance { get; } = new Database();   
        private Database()
        {
        }
    }
}