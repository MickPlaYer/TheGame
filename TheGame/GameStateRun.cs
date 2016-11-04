using System;
using System.Drawing;
using TheGame.RTS;

namespace TheGame
{
    class GameStateRun : GameState
    {
        private Camera _camera = new Camera();
        private Map _map;
        private ChoseBox _choseBox = new ChoseBox();
        private CommandTable _commandTable;
        private UnitTable _unitTable;

        // 建構式
        public GameStateRun(Game game)
            : base(game)
        {
            _map = new Map();
            _commandTable = new CommandTable(_map.SelectedUnitList);
            _unitTable = new UnitTable(_map.SelectedUnitList);
            System.Threading.Thread.Sleep(500);
            _game.ChangeLoadingProgress(66);
        }

        // 每次開始重整這個遊戲階段
        public override void Init()
        {
            _commandTable.Init();
            _map.Init();
        }

        // 每個遊戲迴圈處理一次Update()
        public override void Update()
        {
            _map.Update();
            _commandTable.Update();
            if (!_choseBox.IsChosing)
                _camera.Update();
        }

        // 繪製畫面
        public override void Draw()
        {
            _map.Draw(_camera);
            if (_choseBox.IsChosing)
                _choseBox.Draw();
            _commandTable.Draw();
            _unitTable.Draw();
        }

        public override void OnMouseMove(int positionX, int positionY)
        {
            Point point = _camera.ScreenToPoint(new Point(positionX, positionY));
            if (_choseBox.IsChosing)
            {
                _choseBox.SetBox(positionX, positionY);
            }
            CameraMotion type = _camera.GetMotion();
            if (type != CameraMotion.None)
                GameCursor.SetType((CursorType)Enum.Parse(typeof(CursorType), type.ToString()));
            else if (_commandTable.ContainCursor(point))
            {
                GameCursor.SetType(_commandTable.IsGettingTarget, false);
            }
            else
            {
                _map.OnMouseMove(point);
                GameCursor.SetType(_commandTable.IsGettingTarget, _map.CursorUnit != null);
            }
        }

        public override void OnMouseDown(MouseButtons mouseButton, int positionX, int positionY)
        {
            Point point = _camera.ScreenToPoint(new Point(positionX, positionY));
            if (_choseBox.IsChosing && mouseButton == MouseButtons.Right)
                _choseBox.Cancle();
            else if (_commandTable.ContainCursor(point))
            {
                _commandTable.OnTableMouseDown(mouseButton, point.X, point.Y);
            }
            else
            {
                if (!_commandTable.IsGettingTarget)
                {
                    if (mouseButton == MouseButtons.Left)
                    {
                        _choseBox.NewBox(new Point(positionX, positionY));
                    }
                }
                _commandTable.OnMapMouseDown(mouseButton, _map.CursorUnit, point.X, point.Y);
            }
        }

        public override void OnMouseUp(MouseButtons mouseButton, int positionX, int positionY)
        {
            Point point = _camera.ScreenToPoint(new Point(positionX, positionY));
            if (_choseBox.IsChosing && mouseButton == MouseButtons.Left)
            {
                _choseBox.SetBox(positionX, positionY);
                _choseBox.ChoseUnits(_map.UnitList, _map.SelectedUnitList, _camera);
            }
            else if (_commandTable.ContainCursor(point))
            {
                _commandTable.OnTableMouseUp(mouseButton, point.X, point.Y);
            }
            else
            {
                _commandTable.OnMapMouseUp();
            }
        }

        public override void OnKeyDown(string key)
        {
            if (key == "Escape")
                _game.GoToState(2);
            if (_map.SelectedUnitList.Count > 0)
            {
                if (key == "Subtract")
                    _map.SelectedUnitList[0].HitPoint.Value--;
                if (key == "Add")
                    _map.SelectedUnitList[0].HitPoint.Value++;
            }
            _commandTable.OnKeyDown(key);
        }

        public override void OnKeyUp(string key)
        {
            _commandTable.OnKeyUp();
        }
    }
}
