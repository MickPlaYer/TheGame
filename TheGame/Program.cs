using System;
using System.Windows.Forms;

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
            TheGame theGame = new TheGame();
            if (theGame.IsLoadFail)
                MessageBox.Show("Game load fail!");
            else
                Application.Run(theGame);
        }
    }
}
