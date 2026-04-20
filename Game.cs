// using System;
// using System.Drawing;
// using System.Windows.Forms;

// namespace WumpusGame
// {
//     class Game
//     {
//         private Map map;
//         private Player player;
//         private bool running;
//         private int playerMoves;

//         public Game()
//         {
//             // Khởi tạo map 8x15
//             map = new Map(8, 15);

//             // Player bắt đầu ở (0,0)
//             player = new Player(0, 0);

//             running = true;
//             playerMoves = 0;
//         }


//                 public void Run()
//         {
//             Console.WriteLine("=== Wumpus Game ===");
//             Console.WriteLine("Điều khiển: W/A/S/D để đổi hướng hoặc di chuyển, SPACE để bắn tên, Q để thoát");

//             while (running && player.Alive)
//             {
//                 map.Render(player);
//                 Console.WriteLine($"Mũi tên: {player.Arrows}");

//                 player.Move(map);

//                 if (!player.Alive)
//                 {
//                     running = false;
//                     break;
//                 }

//                 if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q)
//                 {
//                     running = false;
//                 }
//             }
//              Console.WriteLine("Kết thúc game!");
//               GameForm? form = Application.OpenForms["GameForm"] as GameForm;
//         form?.ShowAction("Kết thúc game!");
//         }
//     }
// }
