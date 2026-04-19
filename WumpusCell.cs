using System;
using System.Collections.Generic;

namespace WumpusGame
{
    class WumpusCell : Cell
    {
        public override string Symbol => "W";

        public WumpusCell(int x, int y) : base(x, y) {}

    public void Move(Map map)
    {
        int[,] dirs = {
            { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 },
            { -1, -1 }, { -1, 1 }, { 1, -1 }, { 1, 1 }
        };

        List<(int, int)> candidates = new List<(int, int)>();
        for (int i = 0; i < dirs.GetLength(0); i++)
        {
            int nx = X + dirs[i, 0];
            int ny = Y + dirs[i, 1];
            if (map.IsInside(nx, ny) && map.Grid[nx, ny] is EmptyCell empty && !empty.Visited)
                candidates.Add((nx, ny));
        }

        if (candidates.Count > 0)
        {
            Random rnd = new Random();
            var (newX, newY) = candidates[rnd.Next(candidates.Count)];
            map.Grid[X, Y] = new EmptyCell(X, Y);
            map.Grid[newX, newY] = new WumpusCell(newX, newY);
        }
    }
        public override void OnEnter(Player player)
        {
            player.Die("Bạn bị Wumpus ăn thịt!");
                    GameForm? form = Application.OpenForms["GameForm"] as GameForm;
        form?.ShowMessage("Bạn bị Wumpus ăn thịt!");
        }
    }
}
