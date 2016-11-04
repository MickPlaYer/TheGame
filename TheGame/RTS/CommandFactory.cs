using TheGame.RTS.Commands;

namespace TheGame.RTS
{
    class CommandFactory
    {
        const int TABLE_WIDTH = 4;
        const int TABLE_HEIGHT = 3;
        private MovableBitmap _stopButton = new MovableBitmap();
        private MovableBitmap _moveButton = new MovableBitmap();
        private MovableBitmap _attackButton = new MovableBitmap();

        public Command CreateStopCommand()
        {
            _stopButton.LoadBitmap(@"bitmaps\command_buttons\stop.png");
            return new Stop(_stopButton, "S");
        }

        public Command CreateMoveCommand()
        {
            _moveButton.LoadBitmap(@"bitmaps\command_buttons\move.png");
            return new Move(_moveButton, "M");
        }

        public Command CreateAttackCommand()
        {
            _attackButton.LoadBitmap(@"bitmaps\command_buttons\attack.png");
            return new Attack(_attackButton, "A");
        }

        public Command[,] CreateBasicCommands()
        {
            Command[,] commands = new Command[TABLE_WIDTH, TABLE_HEIGHT];
            commands[0, 0] = CreateStopCommand();
            commands[1, 0] = CreateMoveCommand();
            commands[3, 0] = CreateAttackCommand();
            return commands;
        }
    }
}
