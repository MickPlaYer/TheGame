using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TheGame
{
    static class GameMath
    {
        static public PointF Normalize(PointF point)
        {
            float distance = (float)Math.Sqrt(point.X * point.X + point.Y * point.Y);
            return new PointF(point.X / distance, point.Y / distance);
        }

        static public PointF Add(PointF point1, PointF point2)
        {
            point1.X += point2.X;
            point1.Y += point2.Y;
            return point1;
        }

        static public Point Add(Point point1, Point point2)
        {
            point1.X += point2.X;
            point1.Y += point2.Y;
            return point1;
        }

        static public PointF Subtract(PointF point1, PointF point2)
        {
            point1.X -= point2.X;
            point1.Y -= point2.Y;
            return point1;
        }

        static public Point Subtract(Point point1, Point point2)
        {
            point1.X -= point2.X;
            point1.Y -= point2.Y;
            return point1;
        }

        static public PointF Multiply(PointF point, int value)
        {
            point.X *= value;
            point.Y *= value;
            return point;
        }

        static public float Distance(PointF point1, PointF point2)
        {
            float distanceX = point1.X - point2.X;
            float distanceY = point1.Y - point2.Y;
            return (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
        }
    }
}
