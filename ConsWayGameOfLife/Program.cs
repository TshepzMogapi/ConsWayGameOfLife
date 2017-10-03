using System;
using System.Text;

namespace GameOfLife
{
    internal class Program
    {
        private static int height = 24;
        private static int width = 80;
        private static Random random = new Random();

        private static bool[,] currentState = new bool[height, width];

        private static void Main(string[] args)
        {
            PopulateGrid();
            while (true)
            {
                DisplayGrid();
                ApplyRules();
                System.Threading.Thread.Sleep(200);
            }
        }

        private static void PopulateGrid()
        {
            Loop((row, column) =>
            {
                bool isAlive = random.Next(5) == 0;
                currentState[row, column] = isAlive;
            });
        }

        private static void DisplayGrid()
        {
            Console.SetCursorPosition(0, 0);
            StringBuilder stringBuilder = new StringBuilder();
            Loop((row, column) =>
            {
                if (currentState[row, column])
                {
                    stringBuilder.Append("*");
                }
                else
                {
                    stringBuilder.Append(" ");
                }

                if (column == width - 1)
                {
                    stringBuilder.AppendLine();
                }
            });

            Console.Write(stringBuilder);
        }

        private static void ApplyRules()
        {
            bool[,] nextState = new bool[height, width];
            int NoOfNeighbours;
            Loop((row, column) =>
            {
                NoOfNeighbours = 0;

                var newRow = (row - 1 < 0) ? height - 1 : row - 1;
                var newColumn = (column - 1 < 0) ? width - 1 : column - 1;
                if (currentState[newRow, newColumn])
                {
                    NoOfNeighbours++;
                }

                newColumn = column;
                if (currentState[newRow, newColumn])
                {
                    NoOfNeighbours++;
                }

                newColumn = (column + 1 > width - 1) ? 0 : column + 1;
                if (currentState[newRow, newColumn])
                {
                    NoOfNeighbours++;
                }

                newRow = row;
                newColumn = (column - 1 < 0) ? width - 1 : column - 1;
                if (currentState[newRow, newColumn])
                {
                    NoOfNeighbours++;
                }

                newColumn = (column + 1 > width - 1) ? 0 : column + 1;
                if (currentState[newRow, newColumn])
                {
                    NoOfNeighbours++;
                }

                newRow = (row + 1 > height - 1) ? 0 : row + 1;
                newColumn = (column - 1 < 0) ? width - 1 : column - 1;
                if (currentState[newRow, newColumn])
                {
                    NoOfNeighbours++;
                }

                newColumn = column;
                if (currentState[newRow, newColumn])
                {
                    NoOfNeighbours++;
                }

                newColumn = (column + 1 > width - 1) ? 0 : column + 1;
                if (currentState[newRow, newColumn])
                {
                    NoOfNeighbours++;
                }

                if (currentState[row, column] && (NoOfNeighbours < 2 || NoOfNeighbours > 3))
                {
                    nextState[row, column] = false;
                }
                else if (currentState[row, column] && (NoOfNeighbours == 2 || NoOfNeighbours == 3))
                {
                    nextState[row, column] = true;
                }
                else if (!currentState[row, column] && NoOfNeighbours == 3)
                {
                    nextState[row, column] = true;
                }
            });
            currentState = nextState;
        }

        private static void Loop(Action<int, int> action)
        {
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    action(row, column);
                }
            }
        }
    }
}