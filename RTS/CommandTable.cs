using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TheGame.Engine;

namespace TheGame.RTS
{
    class CommandTable
    {
        const int TABLE_WIDTH = 272;
        const int TABLE_HEIGHT = 208;
        const int BUTTON_ROW = 4;
        const int BUTTON_COLUMN = 3;
        const int BUTTON_SIZE = 64;
        private Point _position;
        private Size _anchor = new Size(8, 8);
        private MovableBitmap _table = new MovableBitmap();
        private MovableBitmap _buttonBack = new MovableBitmap();
        private MovableBitmap _button = new MovableBitmap();
        private MovableBitmap _buttonHeighlight = new MovableBitmap();
        private List<Unit> _selectedUnits;
        private Command[,] _commands = new Command[BUTTON_ROW, BUTTON_COLUMN];
        private Command _mouseCommand;
        private Command _keyboardCommand;
        private TargetCommand _frcCommand = new Commands.FastRightClick();
        private TargetCommand _targetCommand;

        public CommandTable(List<Unit> selectedUnits)
        {
            _selectedUnits = selectedUnits;
            _table.LoadBitmap(@"bitmaps\command_buttons\command_table.png");
            _buttonBack.LoadBitmap(@"bitmaps\command_buttons\button_back.png");
            _button.LoadBitmap(@"bitmaps\command_buttons\button.png");
            _buttonHeighlight.LoadBitmap(@"bitmaps\command_buttons\button_heightlight.png");
        }

        public void Init()
        {
            _position = new Point(Game.ScreenWidth - TABLE_WIDTH, Game.ScreenHeight - TABLE_HEIGHT);
            _table.SetPosition(_position);
        }

        public void Update()
        {
            if (_selectedUnits.Count != 0)
                _commands = _selectedUnits.First<Unit>().Commands;
            else
                _commands = new Command[BUTTON_ROW, BUTTON_COLUMN];
        }

        public void Draw()
        {
            _table.Draw();
            for (int i = 0; i < BUTTON_ROW; i++)
                for (int j = 0; j < BUTTON_COLUMN; j++)
                {
                    int x = _position.X + BUTTON_SIZE * i + _anchor.Width;
                    int y = _position.Y + BUTTON_SIZE * j + _anchor.Height;
                    if (_commands[i, j] != null)
                    {
                        Point cursor = GameCursor.Position;
                        Rectangle button = new Rectangle(x, y, BUTTON_SIZE, BUTTON_SIZE);
                        if (_commands[i, j] == _keyboardCommand || _commands[i, j] == _mouseCommand && button.Contains(cursor))
                        {
                            _buttonHeighlight.SetPosition(x, y);
                            _buttonHeighlight.Draw();
                        }
                        else
                        {
                            _button.SetPosition(x, y);
                            _button.Draw();
                        }
                        _commands[i, j].DrawButton(x, y);
                    }
                    else
                    {
                        _buttonBack.SetPosition(x, y);
                        _buttonBack.Draw();
                    }
                }
        }

        public void OnTableMouseDown(MouseButtons mouseButton, int positionX, int positionY)
        {
            if (IsGettingTarget)
                _targetCommand = null;
            if (mouseButton == MouseButtons.Left)
            {
                Command command = GetCommand(GameCursor.Position);
                if (command != null)
                {
                    _mouseCommand = command;
                }
            }
        }

        public void OnMapMouseDown(MouseButtons mouseButton, Unit cursorUnit, int positionX, int positionY)
        {
            if (IsGettingTarget)
            {
                if (mouseButton == MouseButtons.Left)
                {
                    SetTargetAndCommandUnits(_targetCommand, cursorUnit, new Point(positionX, positionY));
                    if (!KeyboardInput.IsKeyDown(KeyCodes.ShiftKey))
                        _targetCommand = null;
                }
                else
                    _targetCommand = null;
            }
            else
            {
                if (mouseButton == MouseButtons.Right)
                    SetTargetAndCommandUnits(_frcCommand, cursorUnit, new Point(positionX, positionY));
            }
        }

        public void OnTableMouseUp(MouseButtons mouseButton, int positionX, int positionY)
        {
            if (mouseButton == MouseButtons.Left)
            {
                Command command = GetCommand(GameCursor.Position);
                if (command != null && command == _mouseCommand)
                {
                    UseCommand(_mouseCommand);
                }
            }
            _mouseCommand = null;
        }

        public void OnMapMouseUp()
        {
            if (_mouseCommand != null)
                _mouseCommand = null;
        }

        private Command GetCommand(Point position)
        {
            for (int i = 0; i < BUTTON_ROW; i++)
            {
                for (int j = 0; j < BUTTON_COLUMN; j++)
                {
                    int x = _position.X + BUTTON_SIZE * i;
                    int y = _position.Y + BUTTON_SIZE * j;
                    Rectangle button = new Rectangle(x, y, BUTTON_SIZE, BUTTON_SIZE);
                    if (button.Contains(position))
                        return _commands[i, j];
                }
            }
            return null;
        }

        private void SetTargetAndCommandUnits(TargetCommand targetCommand, Unit cursorUnit, Point position)
        {
            targetCommand.SetTarget(position, cursorUnit);
            CommandUnits(targetCommand);
        }

        public void OnKeyDown(string key)
        {
            foreach (var command in _commands)
            {
                if (command != null)
                    if (command.Key == key)
                    {
                        _keyboardCommand = command;
                        UseCommand(command);
                        break;
                    }
            }
        }

        public void OnKeyUp()
        {
            _keyboardCommand = null;
        }

        private void UseCommand(Command command)
        {
            if (command is TargetCommand)
                _targetCommand = command as TargetCommand;
            else
                CommandUnits(command);
        }

        private void CommandUnits(Command command)
        {
            for (int i = 0; i < _selectedUnits.Count; i++)
            {
                Action action = command.Cast(_selectedUnits[i]);
                _selectedUnits[i].CommandUnit(action);
            }
        }

        public bool IsGettingTarget
        {
            get { return _targetCommand != null; }
        }

        public bool ContainCursor(Point point)
        {
            Rectangle table = new Rectangle(_position, new Size(TABLE_WIDTH, TABLE_HEIGHT));
            return table.Contains(GameCursor.Position);
        }
    }
}
