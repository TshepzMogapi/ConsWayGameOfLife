using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    bool isAlive = random.Next(5) == 0;
                    currentState[row, column] = isAlive;
                }
            }
        }

        private static void DisplayGrid()
        {
            Console.SetCursorPosition(0, 0);
            StringBuilder stringBuilder = new StringBuilder();
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    if (currentState[row, column])
                    {
                        stringBuilder.Append("*");
                    }
                    else
                    {
                        stringBuilder.Append(" ");
                    }
                }
                stringBuilder.AppendLine();
            }
            Console.Write(stringBuilder);
        }

        private static void ApplyRules()
        {
            bool[,] nextState = new bool[height, width];
            int noOfNeighbours;
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    noOfNeighbours = 0;

                    var newRow = (row - 1 < 0) ? height - 1 : row - 1;
                    var newColumn = (column - 1 < 0) ? width - 1 : column - 1;

                    if (currentState[newRow, newColumn])
                    {
                        noOfNeighbours++;
                    }

                    newColumn = column;
                    if (currentState[newRow, newColumn])
                    {
                        noOfNeighbours++;
                    }

                    newColumn = (column + 1 > width - 1) ? 0 : column + 1;
                    if (currentState[newRow, newColumn])
                    {
                        noOfNeighbours++;
                    }

                    newRow = row;
                    newColumn = (column - 1 < 0) ? width - 1 : column - 1;
                    if (currentState[newRow, newColumn])
                    {
                        noOfNeighbours++;
                    }

                    newColumn = (column + 1 > width - 1) ? 0 : column + 1;
                    if (currentState[newRow, newColumn])
                    {
                        noOfNeighbours++;
                    }

                    newRow = (row + 1 > height - 1) ? 0 : row + 1;
                    newColumn = (column - 1 < 0) ? width - 1 : column - 1;
                    if (currentState[newRow, newColumn])
                    {
                        noOfNeighbours++;
                    }

                    newColumn = column;
                    if (currentState[newRow, newColumn])
                    {
                        noOfNeighbours++;
                    }

                    newColumn = (column + 1 > width - 1) ? 0 : column + 1;
                    if (currentState[newRow, newColumn])
                    {
                        noOfNeighbours++;
                    }

                    // Apply rules
                    if (currentState[row, column] && (noOfNeighbours < 2 || noOfNeighbours > 3))
                    {
                        nextState[row, column] = false;
                    }
                    else if (currentState[row, column] && (noOfNeighbours == 2 || noOfNeighbours == 3))
                    {
                        nextState[row, column] = true;
                    }
                    else if (!currentState[row, column] && noOfNeighbours == 3)
                    {
                        nextState[row, column] = true;
                    }
                }
            }
            currentState = nextState;
        }
    }
}