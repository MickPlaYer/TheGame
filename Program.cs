using System;
using System.Windows.Forms;
using TheGame.Engine;

namespace TheGame
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GameWindow gameWindow = new GameWindow();
            if (gameWindow.IsLoadFail)
                MessageBox.Show("Game load fail!");
            else
                Application.Run(gameWindow);
        }
    }
}
