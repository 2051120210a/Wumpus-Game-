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
        private Button btnRestart;
        public bool justEnteredCell = false;

        public GameForm()
        {
        this.Text = "Wumpus Game";
        this.WindowState = FormWindowState.Maximized;
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

        AssetManager.Load();
        UpdateStatus();
        ShowMessage("=== Wumpus Game === \n"+"Game Start");
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


        private void ShowRestartButton()
        {
            btnRestart = new Button();
            btnRestart.Text = "Restart Game";
            btnRestart.Size = new Size(150, 50);
            btnRestart.Location = new Point(lblAction.Left + 200, lblAction.Bottom + 450);

            btnRestart.Click += (s, e) => RestartGame();

            this.Controls.Add(btnRestart);
            btnRestart.BringToFront();
        }
            private void RestartGame()
        {
            if (btnRestart != null)
                this.Controls.Remove(btnRestart);

            // Reset dữ liệu game
            map = new Map(8, 15);
            player = new Player(0, 0);

            map.Grid[player.X, player.Y].OnEnter(player);

            // Reset renderer
            this.Controls.Remove(renderer);
            renderer = new Renderer(map, player);
            renderer.Location = new Point(0, 0);
            renderer.Size = new Size(960, 512);
            this.Controls.Add(renderer);

            // Reset label
            lblMessage.Text = "";
            lblAction.Text = "";
            UpdateStatus();

            // Bật lại control
            this.KeyDown += GameForm_KeyDown;

            ShowMessage("=== Game Restarted ===");
        }

        private void ShowVictory()
        {
            // Tạo PictureBox hiển thị hình chiến thắng
            PictureBox pbVictory = new PictureBox();
            pbVictory.Image = new Bitmap(Image.FromFile("Asset/victory.png"), new Size(350, 350));
            pbVictory.SizeMode = PictureBoxSizeMode.StretchImage;

            // Lấy vị trí lblAction và đặt PictureBox cách 200px phía dưới
            int x = lblAction.Left + 100;
            int y = lblAction.Bottom + 50;

            pbVictory.Location = new Point(x, y);
            pbVictory.Size = new Size(350, 350);

            // Thêm vào Form chính
            this.Controls.Add(pbVictory);
            pbVictory.BringToFront();
            ShowRestartButton();
        }


        private void ShowGameOver()
        {
            PictureBox pbGameOver = new PictureBox();
            pbGameOver.Image = new Bitmap(Image.FromFile("Asset/game_over.png"), new Size(350, 350));
            pbGameOver.SizeMode = PictureBoxSizeMode.StretchImage;

            pbGameOver.Location = new Point(lblAction.Left + 100, lblAction.Bottom + 50);
            pbGameOver.Size = new Size(350, 350);

            this.Controls.Add(pbGameOver);
            pbGameOver.BringToFront();
            ShowRestartButton();
        }


    }
}
