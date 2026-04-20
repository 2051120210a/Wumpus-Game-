using System;
using System.Collections.Generic;

namespace WumpusGame
{
    public class Map
    {
        public static Map Instance { get; private set; }
        public int Width { get; }
        public int Height { get; }
        public Cell[,] Grid { get; }
        private Random rand = new Random();
            public Map()
            {
                Instance = this;
            }
        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            Grid = new Cell[width, height];
            Instance = this;
            Generate();
        }

        private int CountAdjacentHazards(int x, int y)
        {
            int count = 0;
            int[,] dirs = {
                { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 },
                { -1, -1 }, { -1, 1 }, { 1, -1 }, { 1, 1 }
            };

            for (int i = 0; i < dirs.GetLength(0); i++)
            {
                int nx = x + dirs[i, 0];
                int ny = y + dirs[i, 1];
                if (IsInside(nx, ny) && Grid[nx, ny] is PitCell)
                {
                    count++;
                }
            }
            return count;
        }

        private void Generate()
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        if (x == 0)
                        {
                            Grid[x, y] = new EmptyCell(x, y);
                        }
                        else
                        {
                            int r = rand.Next(100);

                            if (r < 5)
                                Grid[x, y] = new WallCell(x, y);
                            else if (r < 25)
                            {
                                if (CountAdjacentHazards(x, y) < 2)
                                    Grid[x, y] = new PitCell(x, y);
                                else
                                    Grid[x, y] = new EmptyCell(x, y);
                            }
                            else if (r < 30)
                                Grid[x, y] = new ArrowCell(x, y);
                            else if (r < 35)
                                Grid[x, y] = new BatCell(x, y);
                            else if (r < 38)
                                Grid[x, y] = new WumpusCell(x, y);
                            else
                                Grid[x, y] = new EmptyCell(x, y);
                        }
                    }
                }

                // Đảm bảo có ít nhất 3 Wumpus
                int wumpusCount = 0;
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        if (Grid[x, y] is WumpusCell)
                            wumpusCount++;
                    }
                }

                while (wumpusCount < 3)
                {
                    var (wx, wy) = RandomEmptyCell();
                    Grid[wx, wy] = new WumpusCell(wx, wy);
                    wumpusCount++;
                }
                Grid[0, 0].Visited = true;
                while (wumpusCount > 7)
                {
                    var (wx, wy) = RandomWumpusCell();
                    Grid[wx, wy] = new EmptyCell(wx, wy);
                    wumpusCount--;
                }
                Grid[0, 0].Visited = true;
            }

            


        public bool IsInside(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public (int, int) RandomEmptyCell()
        {
            while (true)
            {
                int x = rand.Next(Width);
                int y = rand.Next(Height);
                if (Grid[x, y] is EmptyCell)
                    return (x, y);
            }
        }
                public (int, int) RandomWumpusCell()
        {
            while (true)
            {
                int x = rand.Next(Width);
                int y = rand.Next(Height);
                if (Grid[x, y] is WumpusCell)
                    return (x, y);
            }
        }

        public void ReplaceCell(int x, int y, Cell newCell)
        {
            if (IsInside(x, y))
                Grid[x, y] = newCell;
        }

        public void MoveAllWumpus()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (Grid[x, y] is WumpusCell wumpus)
                    {
                        wumpus.Move(this);
                    }
                }
            }
        }

        public void Render(Player player)
        {
            Console.Clear();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (player.X == x && player.Y == y)
                        Console.Write("P ");
                    else if (Grid[x, y] is WumpusCell)
                        Console.Write("W ");
                    else if (Grid[x, y].Visited)
                        Console.Write(Grid[x, y].Symbol + " ");
                    else
                        Console.Write("? ");
                }
                Console.WriteLine();
            }
        }
    }
}
