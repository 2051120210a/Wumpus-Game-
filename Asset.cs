using System;
using System.Collections.Generic;
using System.Drawing;

namespace WumpusGame
{
    public static class AssetManager
    {
        public static Dictionary<string, Image> Icons = new Dictionary<string, Image>();

        public static void Load()
        {
            if (Icons.Count > 0) return;

            Image emptyImg = Image.FromFile("Asset/empty.png");
            Image arrowImg = Image.FromFile("Asset/arrow.png");

            Bitmap combinedArrow = new Bitmap(emptyImg.Width, emptyImg.Height);
            using (Graphics g = Graphics.FromImage(combinedArrow))
            {
                g.DrawImage(emptyImg, 0, 0);
                g.DrawImage(arrowImg, 0, 0);
            }

            Icons["."] = emptyImg;
            Icons["A"] = combinedArrow;

            Icons["W"] = new Bitmap(Image.FromFile("Asset/wumpus.png"), new Size(60, 60));
            Icons["B"] = new Bitmap(Image.FromFile("Asset/bat.png"), new Size(60, 60));
            Icons["P"] = Image.FromFile("Asset/hole.png");
            Icons["#"] = new Bitmap(Image.FromFile("Asset/wall_1.png"), new Size(64, 64));
            Icons["X"] = Image.FromFile("Asset/x.png");

            Icons["PLAYER_RIGHT"] = new Bitmap(Image.FromFile("Asset/player_right.png"), new Size(60, 60));
            Icons["PLAYER_LEFT"]  = new Bitmap(Image.FromFile("Asset/player_left.png"), new Size(60, 60));
            Icons["PLAYER_DOWN"]  = new Bitmap(Image.FromFile("Asset/player_down.png"), new Size(60, 60));
            Icons["PLAYER_UP"]    = new Bitmap(Image.FromFile("Asset/player_up.png"), new Size(60, 60));
        }
    }
}