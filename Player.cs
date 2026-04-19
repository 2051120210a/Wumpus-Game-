using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace WumpusGame
{
    public class Player : Creature
    {
        public int Arrows { get; set; }
        public string CurrentSprite { get; private set; }
        public bool Alive { get; private set; } = true;
        public int playerMoves{ get; set; }
        public int PrevX { get; private set; }
        public int PrevY { get; private set; }

        public Player(int x, int y) : base(x, y)
        {
            Facing = Direction.Down; 
             CurrentSprite = "PLAYER_DOWN";
            Arrows = 3;
            playerMoves=0;
        }
        public void MoveBack()
        {
            X= PrevX;Y=PrevY;
        }
        public void Move(Map map, Keys key)
        {
            this.PrevX=X; this.PrevY=Y;
            switch (key)
            {
                case Keys.A:
                    if (Facing == Direction.Left) {base.Move(map); playerMoves++;}
                    else { Facing = Direction.Left;  CurrentSprite = "PLAYER_LEFT"; }
                    break;

                case Keys.D:
                    if (Facing == Direction.Right)  {base.Move(map); playerMoves++;}
                    else { Facing = Direction.Right; CurrentSprite = "PLAYER_RIGHT"; }
                    break;

                case Keys.W:
                    if (Facing == Direction.Up)  {base.Move(map); playerMoves++;}
                    else { Facing = Direction.Up; CurrentSprite = "PLAYER_UP"; }
                    break;

                case Keys.S:
                    if (Facing == Direction.Down)  {base.Move(map); playerMoves++;}
                    else { Facing = Direction.Down; CurrentSprite = "PLAYER_DOWN"; }
                    break;

                case Keys.Space:
                    ShootArrow(map);
                    break;
            }
            

            // Đánh dấu ô hiện tại đã thăm
            map.Grid[X, Y].Visited = true;

            // Kích hoạt hiệu ứng của cell
            map.Grid[X, Y].OnEnter(this);

            GameForm? form = Application.OpenForms["GameForm"] as GameForm;
            form?.renderer.RefreshMap();
        }


         public void Die(string reason)
        {
            Alive = false;
            Console.WriteLine(reason);
        }
        public void ShootArrow(Map map)
        {
            if (Arrows <= 0)
            {
                Console.WriteLine("Bạn đã hết tên!");
            GameForm? form = Application.OpenForms["GameForm"] as GameForm;
        form?.ShowMessage("Bạn đã hết tên!");
                return;
            }

            Arrows--;

            int nx = X, ny = Y;
            switch (Facing)
            {
                case Direction.Up: nx--; break;
                case Direction.Down: nx++; break;
                case Direction.Left: ny--; break;
                case Direction.Right: ny++; break;
            }

            if (map.IsInside(nx, ny))
            {
                if (map.Grid[nx, ny] is WumpusCell)
                {
                    Console.WriteLine("Bạn đã bắn trúng Wumpus!");
                GameForm? form = Application.OpenForms["GameForm"] as GameForm;
        form?.ShowAction("Bạn đã bắn trúng Wumpus!");
                    // Thay thế Wumpus bằng EmptyCell
                    map.ReplaceCell(nx, ny, new EmptyCell(nx, ny));
                }
                else
                {
                    Console.WriteLine("Tên đã bắn trượt...");
        GameForm? form = Application.OpenForms["GameForm"] as GameForm;
        form?.ShowAction("Tên đã bắn trượt...");

                }
            }
            else
            {
                Console.WriteLine("Tên bay ra ngoài bản đồ...");
        GameForm? form = Application.OpenForms["GameForm"] as GameForm;
        form?.ShowMessage("Tên bay ra ngoài bản đồ...");
            }
        }
    }
}
