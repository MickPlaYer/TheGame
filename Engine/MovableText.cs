using System;
using System.Drawing;

namespace TheGame.Engine
{
    class MovableText : IDisposable
    {
        const string FONT_FAMILY = "Arial";
        private Brush _backBrush = new SolidBrush(Color.FromArgb(128, 128, 128, 128));
        private Brush _textBrush = Brushes.White;
        private MovableBitmap _bitmap = new MovableBitmap();

        public MovableText(string text, int size)
        {
            if (text.Length == 0)
                throw new Exception("MoveableText can't use empty text.");
            Bitmap bitmap = new Bitmap(text.Length * size, text.Length * size);
            Graphics grapgics = Graphics.FromImage(bitmap);
            GraphicsUnit units = GraphicsUnit.Pixel;
            SizeF fontSize = grapgics.MeasureString(text, new Font(FONT_FAMILY, size, units));
            RectangleF fontRectangle = new RectangleF(new PointF(), fontSize);
            grapgics.FillRectangle(_backBrush, fontRectangle);
            grapgics.DrawString(text, new Font(FONT_FAMILY, size, units), _textBrush, new Point());
            Bitmap cloneBitmap = new Bitmap((int)fontSize.Width, (int)fontSize.Height);
            grapgics = Graphics.FromImage(cloneBitmap);
            grapgics.DrawImage(bitmap, 0, 0, fontRectangle, GraphicsUnit.Pixel);
            _bitmap.SetBitmap(cloneBitmap);
            grapgics.Dispose();
            bitmap.Dispose();
        }

        public void SetPosition(int x, int y)
        {
            _bitmap.SetPosition(x, y);
        }

        public void Draw()
        {
            _bitmap.Draw();
        }

        public void Dispose()
        {
            _backBrush.Dispose();
            _textBrush.Dispose();
        }
    }
}
