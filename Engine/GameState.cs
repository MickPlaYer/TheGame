namespace TheGame.Engine
{
    abstract class GameState
    {
        public Game _game;

        public GameState(Game game)
        {
            _game = game;
        }

        public abstract void Init();

        public abstract void Update();

        public abstract void Draw();

        public virtual void OnKeyDown(string key) { }

        public virtual void OnKeyUp(string key) { }

        public virtual void OnMouseClicked(MouseButtons mouseButton, int positionX, int positionY) { }

        public virtual void OnMouseDown(MouseButtons mouseButton, int positionX, int positionY) { }

        public virtual void OnMouseUp(MouseButtons mouseButton, int positionX, int positionY) { }

        public virtual void OnMouseMove(int positionX, int positionY) { }
    }
}
