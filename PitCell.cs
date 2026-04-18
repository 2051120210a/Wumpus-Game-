using System;

namespace WumpusGame
{
    class PitCell : Cell
    {
        public override string Symbol => "P";

        public PitCell(int x, int y) : base(x, y) {}

        public override void OnEnter(Player player)
        {
            // Khi bước vào ô Pit, Player chết ngay
            player.Die("Bạn rơi xuống hố sâu!");
                                GameForm form = Application.OpenForms["GameForm"] as GameForm;
        form?.ShowMessage("Bạn rơi xuống hố sâu!");
        }

    }
}
