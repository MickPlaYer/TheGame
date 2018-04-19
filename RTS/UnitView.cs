using TheGame.Engine;

namespace TheGame.RTS
{
    class UnitView
    {
        const string PATH = @"bitmaps\units\";
        const string PNG = ".png";
        const string OUTLINE = "_outline";
        const string SIGN = "_sign";
        const string BASIC = "basic";
        private MovableBitmap _bitmap = new MovableBitmap();
        private MovableBitmap _outline = new MovableBitmap();
        private MovableBitmap _sign = new MovableBitmap();
        private static UnitView _basicView = new UnitView(BASIC);

        public UnitView(string name)
        {
            try { _bitmap.LoadBitmap(PATH + name + PNG); }
            catch { _bitmap = _basicView.Bitmap; }
            try { _outline.LoadBitmap(PATH + name + OUTLINE + PNG); }
            catch { _outline = _basicView.Outline; }
            try { _sign.LoadBitmap(PATH + name + SIGN + PNG); }
            catch { _sign = _basicView.Sign; }
        }

        public static UnitView BasicView
        {
            get { return _basicView; }
        }

        public MovableBitmap Bitmap
        {
            get { return _bitmap; }
        }

        public MovableBitmap Outline
        {
            get { return _outline; }
        }

        public MovableBitmap Sign
        {
            get { return _sign; }
        }
    }
}
