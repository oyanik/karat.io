using System;
using System.Collections.Generic;

namespace KaratCoding
{
    class Program
    {
        private static Dictionary<string, int> result = new Dictionary<string, int>();
        private static Dictionary<string, int> allCoordinates = new Dictionary<string, int>();
        private static int selectedNumber;
        static void Main(string[] args)
        {
            string line;
            bool firstArg = true;

            string coord = "";
            List<string> gridLines = new List<string>();

            while ((line = Console.ReadLine()) != null)
            {
                if (line.Equals("")) continue;
                if (firstArg)
                {
                    coord = line;
                    firstArg = false;
                }
                else
                {
                    gridLines.Add(line);
                }
            }
            string[] tokens;

            int[][] grid = new int[gridLines.Count][];
            for (int i = 0; i < gridLines.Count; i++)
            {
                tokens = gridLines[i].Split(" ");
                grid[i] = new int[tokens.Length];
                for (int j = 0; j < tokens.Length; j++)
                {
                    grid[i][j] = int.Parse(tokens[j]);
                }
            }
            tokens = coord.Split(" ");
            int row = int.Parse(tokens[0]);
            int col = int.Parse(tokens[1]);
            Console.WriteLine(disappear(grid, row, col));
        }



        static bool CheckCoordinate(int[][] grid, int row, int col)
        {
            allCoordinates.Remove($"{row}-{col}");
            
            if (grid[row][col] == selectedNumber)
            {
                result.TryAdd($"{row}-{col}", 1);
                return true;
            }
            return false;
        }


        static void wrapper(int[][] grid, int row, int col)
        {

            if (row < 0 || row >= grid.Length || col < 0 || col >= grid[row].Length)
            {
                return;
            }

            int value = 0;
            allCoordinates.TryGetValue($"{row}-{col}", out value);
            if (value == 0)
                return;

            if (CheckCoordinate(grid, row, col))
            {
                //Right
                wrapper(grid, row, col + 1);

                //Left
                wrapper(grid, row, col - 1);

                //Up
                wrapper(grid, row - 1, col);

                //Down
                wrapper(grid, row + 1, col);
            }
            
            

        }

        static int disappear(int[][] grid, int row, int col)
        {
            // Your code goes here
            // NOTE: You may use print statements for debugging purposes, but you may
            //       need to remove them for the tests to pass.

            for (int r = 0; r < grid.Length; r++)
            {
                for (int c = 0; c < grid[r].Length; c++)
                {
                    allCoordinates.TryAdd($"{r}-{c}", 1);
                }
            }


            selectedNumber = grid[row][col];
            result.TryAdd($"{row}-{col}", 1);
            allCoordinates.Remove($"{row}-{col}");

            //Sağ
            wrapper(grid, row, col + 1);

            //Sol
            wrapper(grid, row, col - 1);

            //Yukarı
            wrapper(grid, row - 1, col);

            //Aşağı
            wrapper(grid, row + 1, col);

            return result.Count;
        }
    }
}
