using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WumpusGame
{
    public class GameForm : Form
    {
        public Renderer renderer;
        private Player player;
        private Map map;
        private Label lblMessage;
        private Label lblStatus;  
        private Label lblAction;
        public bool justEnteredCell = false;

        public GameForm()
        {
        this.Text = "Wumpus Game";
        this.ClientSize = new Size(1200, 512); 
        this.KeyPreview = true;
        this.KeyDown += GameForm_KeyDown;
        this.Name = "GameForm";

            map = new Map(8, 15);
            player = new Player(0, 0);
            map.Grid[player.X, player.Y].OnEnter(player);

        renderer = new Renderer(map, player);
        renderer.Location = new Point(0, 0);
        renderer.Size = new Size(960, 512); 
        this.Controls.Add(renderer);
            
        lblStatus = new Label();
        lblStatus.AutoSize = true;//text info
        lblStatus.Location = new Point(970, 20);
        lblStatus.ForeColor = Color.Black;
        lblStatus.Font = new Font("Arial", 12, FontStyle.Bold);
        this.Controls.Add(lblStatus);

        lblMessage = new Label();
        lblMessage.AutoSize = true;
        lblMessage.Location = new Point(970, 60); //text console
        lblMessage.ForeColor = Color.DarkRed;
        lblMessage.Font = new Font("Arial", 10, FontStyle.Italic);
        this.Controls.Add(lblMessage);

        lblAction = new Label();
        lblAction.AutoSize = true;
        lblAction.Location = new Point(970, 90);
        lblAction.ForeColor = Color.DarkRed;
        lblAction.Font = new Font("Arial", 10, FontStyle.Italic);
        this.Controls.Add(lblAction);


        UpdateStatus();
        ShowMessage("=== Wumpus Game === \n"+"Game Start");


            // Đăng ký icon
            renderer.RegisterIcon(".", Image.FromFile("Asset/empty.png"));
            Image emptyImg = Image.FromFile("Asset/empty.png");
            Image arrowImg = Image.FromFile("Asset/arrow.png");

            Bitmap combinedArrow = new Bitmap(emptyImg.Width, emptyImg.Height);
            using (Graphics g = Graphics.FromImage(combinedArrow))
            {
                g.DrawImage(emptyImg, 0, 0);
                g.DrawImage(arrowImg, 0, 0);
            }
            renderer.RegisterIcon("A", combinedArrow);

            Image wumpusImg = new Bitmap(Image.FromFile("Asset/wumpus.png"), new Size(60, 60));
            Bitmap combinedWumpus = new Bitmap(emptyImg.Width, emptyImg.Height);
            using (Graphics g = Graphics.FromImage(combinedWumpus))
            {
                g.DrawImage(emptyImg, 0, 0);
                g.DrawImage(wumpusImg, 0, 0);
            }
            renderer.RegisterIcon("W", combinedWumpus);

            Image batImg = new Bitmap(Image.FromFile("Asset/bat.png"), new Size(60, 60));
            Bitmap combinedBat = new Bitmap(emptyImg.Width, emptyImg.Height);
            using (Graphics g = Graphics.FromImage(combinedBat))
            {
                g.DrawImage(emptyImg, 0, 0);
                g.DrawImage(batImg, 0, 0);
            }
            renderer.RegisterIcon("B", combinedBat);

            renderer.RegisterIcon("P", Image.FromFile("Asset/hole.png"));
            renderer.RegisterIcon("#", new Bitmap(Image.FromFile("Asset/wall_1.png"), new Size(64, 64)));
            //  renderer.RegisterIcon("X", Image.FromFile("Asset/x.png")); 
         

            renderer.RegisterIcon("PLAYER_RIGHT", new Bitmap(Image.FromFile("Asset/player_right.png"), new Size(60, 60)));
            renderer.RegisterIcon("PLAYER_LEFT",  new Bitmap(Image.FromFile("Asset/player_left.png"),  new Size(60, 60)));
            renderer.RegisterIcon("PLAYER_DOWN",  new Bitmap(Image.FromFile("Asset/player_down.png"),  new Size(60, 60)));
            renderer.RegisterIcon("PLAYER_UP",    new Bitmap(Image.FromFile("Asset/player_up.png"),    new Size(60, 60)));
        }   

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            player.Move(map, e.KeyCode);
            renderer.RefreshMap();
             UpdateStatus();
             justEnteredCell = false;

                if (player.playerMoves > 0 && player.playerMoves % 15 == 0)
                {
                    map.MoveAllWumpus();
                    ShowAction("Các Wumpus đã di chuyển");
                    renderer.RefreshMap();
                }

            if (!player.Alive)
            {
                this.KeyDown -= GameForm_KeyDown;
                ShowGameOver();
            }
        }


public void ShowMessage(string msg) { lblMessage.Text = msg; }
public void ShowAction(string msg) { lblAction.Text = msg; }


        private void UpdateStatus()
            {
         int wumpusCount = 0;
            foreach (var cell in map.Grid)
            {
                if (cell is WumpusCell) wumpusCount++;
            }

            lblStatus.Text ="Điều khiển: W/A/S/D để đổi hướng hoặc di chuyển, SPACE để bắn tên\n"+ 
                            $"Arrow remain: {player.Arrows} " +
                            $"| Wumpus remain: {wumpusCount} " +
                            $"| Player Move: {player.playerMoves}";

                    if (wumpusCount == 0)
                {
                     ShowMessage("Bạn thắng trò chơi!");
                    this.KeyDown -= GameForm_KeyDown;
                    ShowVictory();
                    return;
                }
  
            }


                private void ShowVictory()
        {
            Form form = new Form();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Size = new Size(400, 300);

            PictureBox pb = new PictureBox();
            pb.Image = new Bitmap(Image.FromFile("Asset/victory.png"), new Size(350, 350));
            pb.SizeMode = PictureBoxSizeMode.CenterImage;
            pb.Dock = DockStyle.Fill;

            form.Controls.Add(pb);
            form.ShowDialog();
        }

        private void ShowGameOver()
            {
                Form form = new Form();
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Size = new Size(400, 300);

                PictureBox pb = new PictureBox();
                pb.Image = new Bitmap(Image.FromFile("Asset/game_over.png"), new Size(350, 350));
                pb.SizeMode = PictureBoxSizeMode.CenterImage;
                pb.Dock = DockStyle.Fill;

                form.Controls.Add(pb);
                form.ShowDialog();
            }

    }
}
