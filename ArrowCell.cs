using System;

namespace WumpusGame
{
class ArrowCell : Cell
{
    public override string Symbol => "A";

    public ArrowCell(int x, int y) : base(x, y) {}

    public override void OnEnter(Player player)
    {
        player.Arrows++;
        Console.WriteLine("Bạn nhặt được một mũi tên!");
        GameForm form = Application.OpenForms["GameForm"] as GameForm;
        form?.ShowMessage("Bạn nhặt được một mũi tên!");

        // Sau khi nhặt xong, biến ô này thành EmptyCell
        Map.Instance.ReplaceCell(X, Y, new EmptyCell(X, Y));
    }
}
}