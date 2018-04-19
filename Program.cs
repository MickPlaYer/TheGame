using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TheGame.Engine;
using TheGame.RTS;

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
            List<Type> gameStateTypes = new List<Type>
            {
                typeof(GameStateInitial),
                typeof(GameStateRun),
                typeof(GameStateEnd),
            };
            GameWindow gameWindow = new GameWindow(gameStateTypes);
            if (gameWindow.IsLoadFail)
                MessageBox.Show("Game load fail!");
            else
                Application.Run(gameWindow);
        }
    }
}
