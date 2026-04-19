using System;

namespace WumpusGame
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public abstract class Creature
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Facing { get; set; }

        protected Creature(int x, int y, Direction facing = Direction.Up)
        {
            X = x;
            Y = y;
            Facing = facing;
        }

        public virtual void Move(Map map)
        {
            int nx = X;
            int ny = Y;

            switch (Facing)
            {
                case Direction.Up:    nx--; break;
                case Direction.Down:  nx++; break;
                case Direction.Left:  ny--; break;
                case Direction.Right: ny++; break;
            }

            // Kiểm tra xem có nằm trong bản đồ và không phải tường
            if (map.IsInside(nx, ny) )
            {
                X = nx;
                Y = ny;
            }
            else
            {
                Console.WriteLine("Không thể di chuyển theo hướng đó!");
                GameForm? form = Application.OpenForms["GameForm"] as GameForm;
      form?.ShowAction("Không thể di chuyển theo hướng đó!");
                
            }
        }
    }
}
