namespace TestingProject.Models
{
    using System.Drawing;
    using Helpers;

    public class Line
    {
        public Line(PointF a, PointF b)
        {
            A = a;
            B = b;
            Angle = a.AngleTo(b);
        }

        public PointF A { get; set; }
        public PointF B { get; set; }
        public double Angle { get; set; }
    }

    public class BezierLine : Line
    {
        public BezierLine(PointF a, PointF b, PointF c, PointF d) : base(a, b)
        {
            C = c;
            D = d;
            Angle = (a.AngleTo(b) + b.AngleTo(c) + c.AngleTo(d)) / 3;
        }

        public PointF C { get; set; }
        public PointF D { get; set; }
    }
}
