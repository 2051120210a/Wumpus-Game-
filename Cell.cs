using System;

namespace WumpusGame
{
    public abstract class Cell
    {
        public int X { get; }
        public int Y { get; }
        public bool Visited { get; set; } = false;

        public abstract string Symbol { get; }

        protected Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Hành vi khi Player bước vào ô này
        public abstract void OnEnter(Player player);
    }
}
