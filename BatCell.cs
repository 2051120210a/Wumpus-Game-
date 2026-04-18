using System;

namespace WumpusGame
{
    class BatCell : Cell
    {
        public override string Symbol => "B";

        public BatCell(int x, int y) : base(x, y) {}

        public override void OnEnter(Player player)
        {
            Console.WriteLine("Một con dơi đã bắt bạn đi!");

            // Tìm một ô EmptyCell ngẫu nhiên chưa được thăm
            (int nx, int ny) = FindRandomUnvisitedEmpty();

            // Di chuyển Player đến ô đó
            player.X = nx;
            player.Y = ny;

            Console.WriteLine($"Bạn bị đưa đến vị trí ({nx},{ny}).");
                    GameForm form = Application.OpenForms["GameForm"] as GameForm;
        form?.ShowMessage("Con dơi đã bắt bạn đi \n" + $"Bạn bị đưa đến vị trí ({nx},{ny}).");

            // Sau khi dơi bắt đi, ô này biến thành EmptyCell
            Map.Instance.ReplaceCell(X, Y, new EmptyCell(X, Y));
        }

        private (int, int) FindRandomUnvisitedEmpty()
        {
            Random rand = new Random();
            while (true)
            {
                int x = rand.Next(1, Map.Instance.Width); // tránh cột x=0
                int y = rand.Next(Map.Instance.Height);

                if (Map.Instance.Grid[x, y] is EmptyCell cell)
                {
                    return (x, y);
                }
            }
        }
    }
}
