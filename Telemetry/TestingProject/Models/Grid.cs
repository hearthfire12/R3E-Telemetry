namespace TestingProject.Models
{
    using System.Collections.Generic;
    using System.Drawing;

    public class Grid : List<RectangleF>
    {
        public Grid(int cols, int rows, int padding, float sourceWidth, float sourceHeight)
        {
            float halfPadding = padding / 2f;

            var size = new SizeF(
                (sourceWidth - halfPadding - cols * halfPadding) / cols,
                (sourceHeight - halfPadding - rows * halfPadding) / rows);

            for (int col = 0; col < cols; col++)
            {
                for (int row = 0; row < rows; row++)
                {
                    this.Add(new RectangleF(new PointF(
                        halfPadding + col * (size.Width + halfPadding),
                        halfPadding + row * (size.Height + halfPadding)), size));
                }
            }

            Rows = rows;
            Cols = cols;
        }

        public int Rows { get; set; }
        public int Cols { get; set; }

        public override string ToString()
        {
            return $"{this.Rows} x {this.Cols}";
        }
    }

    public class Section
    {
        public bool IsSelected { get; set; }
        public RectangleF Rectangle { get; set; }

        public override string ToString()
        {
            return $"X:{Rectangle.X} - Y:{Rectangle.Y}";
        }
    }
}