using System;
using System.Collections.Generic;

namespace WumpusGame
{
    class EmptyCell : Cell
    {
        public override string Symbol => ".";
        public List<string> Hints { get; private set; } = new List<string>();

        public EmptyCell(int x, int y) : base(x, y) {}

        public override void OnEnter(Player player)
        {
            Visited = true;
            Hints.Clear();

            List<string> hints = new List<string>();
            int[,] dirs = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };

            for (int i = 0; i < dirs.GetLength(0); i++)
            {
                int nx = X + dirs[i, 0];
                int ny = Y + dirs[i, 1];

                if (Map.Instance.IsInside(nx, ny))
                {
                    if (Map.Instance.Grid[nx, ny] is PitCell)
                        {hints.Add("Có gió lùa...");Hints.Add("Breeze");}
                    else if (Map.Instance.Grid[nx, ny] is BatCell)
                        {hints.Add("Có tiếng cánh dơi..."); Hints.Add("Flapping");}
                    else if (Map.Instance.Grid[nx, ny] is WumpusCell)
                        {hints.Add("Có mùi hôi...");Hints.Add("Smell");}
                }
            }

            GameForm? form = Application.OpenForms["GameForm"] as GameForm;
            if (form != null && !form.justEnteredCell)
{
            if (hints.Count > 0)
            {
                form?.ShowMessage("Bạn cảm thấy: " + string.Join(", và ", hints)); 
            }
            else
            {
                form?.ShowMessage("Không có gì đặc biệt...");
            }
            form.justEnteredCell = true;
        }
}
    }
}
