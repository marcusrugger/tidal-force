using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class Tides : Form
{
    private IEnumerable<Tuple<Point, Point>> segments;

    static public void Main ()
    {
        Application.Run(new Tides());
    }

    public Tides ()
    {
        Text = "Tides";
        Size = new System.Drawing.Size(1000, 1000);

        IEnumerable<Vector> vectors = TidalVectors.Create();
        segments = vectors.Select(TransformToDisplayPoints);
    }

    private Tuple<Point, Point> TransformToDisplayPoints(Vector vector)
    {
        const double DISPLAY_RADIUS = 300;
        var v = vector.p.Multiply(DISPLAY_RADIUS / Constants.Earth.MEAN_RADIUS);

        var p1 = new Point((int) (v.x + 0.5), (int) (v.y + 0.5));
        p1.Offset(500, 500);

        var p2 = new Point((int) (50.0 * vector.f.x + 0.5), (int) (50.0 * vector.f.y + 0.5));
        p2.Offset(p1);

        return Tuple.Create(p1, p2);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        DrawIt(e.Graphics);
    }

    private void DrawIt(Graphics graphics)
    {
        DrawEarth(graphics);
        foreach (var pair in segments)
            DrawSegment(graphics, pair.Item1, pair.Item2);
    }

    private void DrawEarth(Graphics graphics)
    {
        Rectangle rectangle = new Rectangle(200, 200, 600, 600);
        graphics.FillEllipse(System.Drawing.Brushes.Aqua, rectangle);
    }

    private void DrawSegment(Graphics graphics, Point p1, Point p2)
    {
        graphics.DrawLine(Pens.Red, p1, p2);
        graphics.FillEllipse(Brushes.Green, p1.X - 2, p1.Y - 2, 5, 5);
        graphics.FillEllipse(Brushes.Blue , p2.X - 2, p2.Y - 2, 5, 5);
    }
}
