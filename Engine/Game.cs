using System;
using System.Collections.Generic;
using System.Drawing;

namespace TheGame.Engine
{
    class Game
    {
        public delegate void LoadingProgressChangedEventHandler(int value);
        public event LoadingProgressChangedEventHandler _loadingProgressChanged;
        private static Graphics _graphics;
        private static Size _screenSize = new Size(1280, 720);
        private GameCursor _cursor;
        private List<GameState> _gameStateList = new List<GameState>();
        private int _gameState;
        private bool _isRunning = true;

        // 建構式
        public Game(LoadingProgressChangedEventHandler loadWorkerDoReportProgress)
        {
            _loadingProgressChanged += loadWorkerDoReportProgress;
            _cursor = new GameCursor();
            _gameStateList.Add(new GameStateInitial(this));
            _gameStateList.Add(new GameStateRun(this));
            _gameStateList.Add(new GameStateEnd(this));
            _gameState = 0;
        }

        // 遊戲最一開始的動作
        public void Init()
        {
            _gameStateList[_gameState].Init();
        }

        // 遊戲回圈
        public void Update()
        {
            _gameStateList[_gameState].Update();
        }

        // 繪製畫面
        public void Draw(Graphics graphics)
        {
            _graphics = graphics;
            _gameStateList[_gameState].Draw();
            _cursor.Draw();
        }

        // 更換遊戲階段
        public void GoToState(int state)
        {
            _gameState = state;
            Init();
        }

        // 鍵盤被按下
        public void OnKeyDown(string key)
        {
            if (key != String.Empty)
                _gameStateList[_gameState].OnKeyDown(key);
        }

        // 鍵盤被釋放
        public void OnKeyUp(string key)
        {
            if (key != String.Empty)
                _gameStateList[_gameState].OnKeyUp(key);
        }

        // 滑鼠被點擊
        public void OnMouseClicked(MouseButtons mouseButton, int positionX, int positionY)
        {
            if (mouseButton != MouseButtons.None)
                _gameStateList[_gameState].OnMouseClicked(mouseButton, positionX, positionY);
        }

        // 滑鼠被移動
        public void OnMouseMove(int positionX, int positionY)
        {
            _gameStateList[_gameState].OnMouseMove(positionX, positionY);
            _cursor.Update(positionX, positionY);
        }

        // 滑鼠被按下
        public void OnMouseDown(MouseButtons mouseButton, int positionX, int positionY)
        {
            if (mouseButton != MouseButtons.None)
                _gameStateList[_gameState].OnMouseDown(mouseButton, positionX, positionY);
        }

        // 滑鼠被釋放
        public void OnMouseUp(MouseButtons mouseButton, int positionX, int positionY)
        {
            if (mouseButton != MouseButtons.None)
                _gameStateList[_gameState].OnMouseUp(mouseButton, positionX, positionY);
        }

        // 當視窗被調整的時候更改畫面大小
        public void OnFormResize(Size clientSize)
        {
            _screenSize.Width = clientSize.Width;
            _screenSize.Height = clientSize.Height;
        }

        public static Graphics Graphics
        {
            get { return _graphics; }
        }

        public static int ScreenWidth
        {
            get { return _screenSize.Width; }
        }

        public static int ScreenHeight
        {
            get { return _screenSize.Height; }
        }

        public static Size ScreenSize
        {
            get { return _screenSize; }
        }

        public void ChangeLoadingProgress(int value)
        {
            _loadingProgressChanged?.Invoke(value);
        }

        public GameCursor GetCursor()
        {
            return _cursor;
        }

        public bool IsRunning
        {
            get { return _isRunning; }
            set { _isRunning = value; }
        }
    }
}
