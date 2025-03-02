using System;

namespace Model
{
    public enum CellType
    {
        source = 0x0000FF, // Blue
        regularPath = 0x00FF00,// Green
        busyPath = 0xFF0000, // Red
        obstacle = 0x000000 // Black
    }

    public struct Cell
    {
        private int[,] grid;
        private CellType cell_Type;
        private int gridSize;

        public CellType Cell_Type { get => this.cell_Type; }
        public int GridSize { get => this.gridSize; }

        public Cell(int gridSize, int[,] pixels)
        {
            // For testing purposes
            if (gridSize != pixels.GetLength(0) || gridSize != pixels.GetLength(1))
                throw new Exception("Invalid grid size");

            this.gridSize = gridSize;
            grid = new int[gridSize, gridSize];
            grid = pixels;
            setCellType(pixels);
        }

        private void setCellType(int[,] pixels)
        {
            int countRegularPathPixels = 0,
                countBusyPathPixels = 0,
                countObstaclePixels = 0,
                countSourcePixels = 0;

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    switch (pixels[i, j])
                    {
                        case (int)CellType.regularPath:
                            countRegularPathPixels++;
                            break;
                        case (int)CellType.busyPath:
                            countBusyPathPixels++;
                            break;
                        case (int)CellType.obstacle:
                            countObstaclePixels++;
                            break;
                        case (int)CellType.source:
                            countSourcePixels++;
                            break;
                        default:
                            throw new Exception("Invalid pixel value"); //For testing purposes
                    }
                }
            }

            /*Priority of cell type when there are multiple types have max count:
             * 1- BusyPath
             * 2- RegularPath
             * 3- Source
             * 4- Obstacle
             */
            int maxCount = Math.Max(countRegularPathPixels,
                Math.Max(countBusyPathPixels,
                Math.Max(countObstaclePixels, countSourcePixels)));

            if (countBusyPathPixels == maxCount)
                cell_Type = CellType.busyPath;
            else if (countRegularPathPixels == maxCount)
                cell_Type = CellType.regularPath;
            else if (countSourcePixels == maxCount)
                cell_Type = CellType.source;
            else
                cell_Type = CellType.obstacle;
        }
    }
}
