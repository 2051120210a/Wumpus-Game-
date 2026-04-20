using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WumpusGame
{
   public class Renderer : Control
{
    private Map map;
    private Player player;
    private int cellSize = 64;
    private readonly Dictionary<string, Image> icons = new Dictionary<string, Image>();

    public Renderer(Map map, Player player)
    {
        this.map = map;
        this.player = player;
        this.DoubleBuffered = true;
        this.icons = AssetManager.Icons;
        this.Width = map.Width * cellSize;
        this.Height = map.Height * cellSize;
    }


    
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    Cell cell = map.Grid[x, y];
                    Rectangle rect = new Rectangle(y * cellSize, x * cellSize, cellSize, cellSize);

                    // Vẽ nền mặc định
                    if (icons.ContainsKey("."))
                        g.DrawImage(icons["."], rect);
                    else
                        g.FillRectangle(Brushes.Black, rect);

                    using (Pen borderPen = new Pen(Color.White, 2))
                    {
                        g.DrawRectangle(borderPen, rect);
                    }

                    // Nếu ô chưa được thăm
                    if (!cell.Visited)
                    {
                        if (icons.ContainsKey("X"))
                            g.DrawImage(icons["X"], rect);
                        else
                            g.DrawString("?", new Font("Arial", 24), Brushes.White, rect);
                        continue;
                    }

                    // Vẽ cell trước
                    string symbol = cell.Symbol;
                    if (icons.ContainsKey(symbol))
                        g.DrawImage(icons[symbol], rect);
                    else
                        g.DrawString(symbol, new Font("Arial", 12), Brushes.White, rect);
                    if (cell is EmptyCell empty && empty.Hints.Count > 0)
                        {
                            string hintText = string.Join(" ", empty.Hints);
                            g.DrawString(hintText, new Font("Arial", 8), Brushes.Yellow, rect);
                        }
                    // Sau đó vẽ Player nếu đứng ở đây
                    if (player.X == x && player.Y == y)
                    {
                        if (icons.ContainsKey(player.CurrentSprite))
                        {
                            Image sprite = icons[player.CurrentSprite];
                            int scale = 40;
                            int offsetX = rect.X + (cellSize - scale) / 2;
                            int offsetY = rect.Y + (cellSize - scale) / 2;

                            g.DrawImage(sprite, new Rectangle(offsetX, offsetY, scale, scale));
                        }
                        else
                        {
                            g.DrawString("P", new Font("Arial", 24), Brushes.White, rect);
                        }
                    }
                }
            }
        }

    public void RefreshMap()
    {
        this.Invalidate();
    }
}

}
