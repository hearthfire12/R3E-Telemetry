namespace Helpers
{
    using System;
    using System.Drawing;

    public static class MathHelper
    {
        public static double Normalize(this double value, double min, double max)
        {
            return (value - min) / (max - min);
        }

        public static bool InRange(this float value, float from, float to)
        {
            return value >= from && value <= to;
        }

        public static bool InRange(this PointF point, RectangleF area)
        {
            return InRange(point.X, area.X, area.X + area.Width)
                   && InRange(point.Y, area.Y, area.Y + area.Height);
        }

        public static double AngleTo(this PointF a, PointF b)
        {
            double dx = a.X - b.X,
                dy = a.Y - b.Y;

            return Math.Atan2(dy, dx);
        }

        public static double GetLength(this PointF a, PointF b)
        {
            throw new NotImplementedException();
        }

        public static double ToDegree(this double angleInRad)
        {
            return angleInRad * 180 / Math.PI;
        }

        public static PointF MiddlePoint(this PointF a, PointF b)
        {
            float diffX = Math.Abs(a.X - b.X),
                diffY = Math.Abs(a.Y - b.Y);

            return new Point(
                (int) Math.Round(Math.Min(a.X, b.X) + diffX / 2f),
                (int) Math.Round(Math.Min(a.Y, b.Y) + diffY / 2f));
        }

        public static Point ToPoint(this PointF p)
        {
            return new Point((int) Math.Round(p.X), (int) Math.Round(p.Y));
        }

        public static PointF RectangleCenter(this RectangleF rectangle)
        {
            return new PointF(rectangle.X + rectangle.Width / 2f, rectangle.Y + rectangle.Height / 2f);
        }
    }
}