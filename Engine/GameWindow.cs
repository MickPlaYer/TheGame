using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace TheGame.Engine
{
    public partial class GameWindow : Form
    {
        const int TIME_PER_FRAME = 166666;
        private int _fpsCount;
        private MouseInput _mouseInput = new MouseInput();
        private KeyboardInput _keyboardInput = new KeyboardInput();
        private Point _mousePosition = Point.Empty;
        private LoadingBar _loadingBar;
        private List<Type> _gameStateTypes;
        private Game _game;

        public GameWindow(List<Type> gameStateTypes)
        {
            InitializeComponent();
            _gameStateTypes = gameStateTypes;
            _loadingBar = new LoadingBar();
            _LoadWorker.RunWorkerAsync();
            _loadingBar.ShowDialog();
            MouseClick += _mouseInput.OnMouseClick;
            MouseDown += _mouseInput.OnMouseDown;
            MouseUp += _mouseInput.OnMouseUp;
            KeyDown += _keyboardInput.OnKeyDown;
            KeyUp += _keyboardInput.OnKeyUp;
        }

        private void OnIdle(object sender, EventArgs e)
        {
            _game.OnFormResize(this.ClientSize);
            _game.Init();
            _fpsTimer.Start();
            while (_game.IsRunning)
            {
                DateTime time1 = DateTime.Now;
                Application.DoEvents();
                _fpsCount++;
                OnMouseInput();
                OnKeyInput();
                _game.Update();
                _mouseInput.ResetInput();
                _keyboardInput.ResetInput();
                Invalidate(this.ClientRectangle);
                DateTime time2 = DateTime.Now;
                TimeSpan _delta = time2 - time1;
                if (_delta.Ticks < TIME_PER_FRAME)
                    Thread.Sleep(new TimeSpan(TIME_PER_FRAME) - _delta);
            }
        }

        // 使用BackgroungWorker來載入遊戲
        private void LoadForm(object sender, DoWorkEventArgs e)
        {
            try
            {
                _game = new Game(_gameStateTypes, _LoadWorker.ReportProgress);
            }
            catch (Exception exception)
            {
                _loadingBar.IsLoadFail = true;
                MessageBox.Show(exception.Message);
            }
        }

        // BackgroungWorker回報遊戲載入的進度
        private void LoadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _loadingBar.SetValue(e.ProgressPercentage);
        }

        // BackgroungWorker回報遊戲載入完成
        private void LoadCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _loadingBar.Close();
        }


        // 處理鍵盤事件
        private void OnKeyInput()
        {
            _game.OnKeyDown(_keyboardInput.DownKey);
            _game.OnKeyUp(_keyboardInput.UpKey);
        }

        // 處理滑鼠事件
        private void OnMouseInput()
        {
            _game.OnMouseMove(_mousePosition.X, _mousePosition.Y);
            _game.OnMouseClicked(_mouseInput.MouseClickButton, _mousePosition.X, _mousePosition.Y);
            _game.OnMouseDown(_mouseInput.MouseDownButton, _mousePosition.X, _mousePosition.Y);
            _game.OnMouseUp(_mouseInput.MouseUpButton, _mousePosition.X, _mousePosition.Y);
        }

        // 計算上一秒內的更新次數
        private void OnFpsCount(object sender, EventArgs e)
        {
            Text = "FPS: " + _fpsCount.ToString();
            _fpsCount = 0;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _game.Draw(e.Graphics);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            _mousePosition = new Point(e.X, e.Y);
        }

        private void OnFormResize(object sender, EventArgs e)
        {
            _game.OnFormResize(this.ClientSize);
        }

        // 當滑鼠進入視窗時鎖定滑鼠的移動範圍
        private void OnMouseEnter(object sender, EventArgs e)
        {
            Rectangle mouseClip = new Rectangle(this.PointToScreen(new Point()), this.ClientSize);
            Cursor.Clip = mouseClip;
            Cursor.Hide();
        }

        // 當滑鼠離開視窗時解除滑鼠的鎖定
        private void OnMouseLeave(object sender, EventArgs e)
        {
            Cursor.Clip = Rectangle.Empty;
            Cursor.Show();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            _game.IsRunning = false;
        }

        public bool IsLoadFail { get { return _loadingBar.IsLoadFail; } }
    }
}
