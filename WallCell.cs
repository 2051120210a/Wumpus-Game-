using System;

namespace WumpusGame
{
    class WallCell : Cell
    {
        public override string Symbol => "#";

        public WallCell(int x, int y) : base(x, y) {}

        public override void OnEnter(Player player)
        {
            Console.WriteLine("Có tường chắn, bạn không thể đi vào!");
                    GameForm form = Application.OpenForms["GameForm"] as GameForm;
        form?.ShowMessage("Có tường chắn, bạn không thể đi vào!");
            player.MoveBack();
        }
    }
}
