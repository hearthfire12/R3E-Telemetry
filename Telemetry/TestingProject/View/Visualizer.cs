namespace TestingProject.View
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using Helpers;
    using TestingProject.Models;

    public partial class Visualizer : Form
    {
        public Visualizer()
        {
            InitializeComponent();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Grid = new Grid(22, 20, 2, pictureBox1.Width, pictureBox1.Height);
            Selected = new List<RectangleF>();

            TileBrush = new SolidBrush(Color.WhiteSmoke);
            SelectedTileBrush = new SolidBrush(Color.Wheat);
            BorderBrush = new SolidBrush(Color.SlateGray);
        }

        protected Grid Grid;
        protected List<RectangleF> Selected;
        protected readonly SolidBrush TileBrush;
        protected readonly SolidBrush SelectedTileBrush;
        protected readonly SolidBrush BorderBrush;


        private bool TryFindSection(PointF mousePoint, IEnumerable<RectangleF> grid, out RectangleF outSection)
        {
            outSection = RectangleF.Empty;

            foreach (var rectangle in grid)
            {
                if (mousePoint.InRange(rectangle))
                {
                    outSection = rectangle;
                    return true;
                }
            }

            return false;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // draw grid
            e.Graphics.Clear(BorderBrush.Color);
            foreach (var s in Grid)
            {
                Brush brush = TileBrush;
                if (Selected.Contains(s))
                {
                    brush = SelectedTileBrush;
                }
                e.Graphics.FillRectangle(brush, s);
            }

            // draw lines
            pictureBox1.Controls.Clear();
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var linePen = new Pen(Color.Black);

            if (Selected.Count <= 1)
            {
                return;
            }

            var points = new List<PointF>();

            for (int i = 0; i < Selected.Count; i++)
            {
                points.Add(Selected[i].RectangleCenter());
                pictureBox1.Controls.Add(
                    CreateLabel((i + 1).ToString(), Selected[i].RectangleCenter().ToPoint()));
            }

            e.Graphics.DrawLines(linePen, points.ToArray());
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            // if left mouse button was pressed
            if ((e.Button & MouseButtons.Left) != 0)
            {
                if (TryFindSection(new PointF(e.X, e.Y), Grid, out RectangleF section))
                {
                    if (Selected.Contains(section))
                    {
                        Selected.Remove(section);
                    }
                    else
                    {
                        Selected.Add(section);
                    }
                    
                    pictureBox1.Invalidate();
                }
            }
        }

        //private Dictionary<Line, Label> CalculateLines(List<RectangleF> points)
        //{
        //    var lines = new Dictionary<Line, Label>();

        //    for (int i = 1; i < points.Count; i++)
        //    {
        //        PointF a = points[i - 1].RectangleCenter(),
        //                b = points[i].RectangleCenter();

        //        var line = new Line(a, b);
        //        var label = CreateLabel(line, a, b);

        //        lines.Add(line, label);
        //    }

        //    return lines;
        //}

        //private Dictionary<BezierLine, Label> CalculateBezierLines(List<RectangleF> points)
        //{
        //    var lines = new Dictionary<BezierLine, Label>();

        //    for (int i = 3; i < points.Count; i += 3)
        //    {
        //        var a = points[i - 3].RectangleCenter();
        //        var b = points[i - 2].RectangleCenter();
        //        var c = points[i - 1].RectangleCenter();
        //        var d = points[i].RectangleCenter();

        //        var line = new BezierLine(a, b, c, d);
        //        var label = CreateLabel(line, a, d);
        //        lines.Add(line, label);
        //    }

        //    return lines;
        //}

        private Label CreateLabel(string text, Point location)
        {
            return new Label
            {
                Text = text,
                Location = location,
                AutoSize = true,
                BackColor = Color.WhiteSmoke,
                Font = new Font(FontFamily.GenericSerif, 12),
            };
        }

        private void BtnClearGrid_Click(object sender, EventArgs e)
        {
            Selected.Clear();
            pictureBox1.Controls.Clear();
            pictureBox1.Invalidate();
        }
    }
}
