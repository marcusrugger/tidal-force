using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class Tides : Form
{
    private IAnimator animator;

    private const int DISPLAY_WIDTH        = 1000;
    private const int DISPLAY_HEIGHT       = 1000;
    private const int DISPLAY_EARTH_RADIUS = 300;

    private readonly Point DISPLAY_CENTER;

    private Timer timer;

    static public void Main ()
    {
        Application.Run(new Tides());
    }

    public Tides ()
    {
        DISPLAY_CENTER = new Point(DISPLAY_WIDTH / 2, DISPLAY_HEIGHT / 2);

        Text = "Tides";
        Size = new Size(DISPLAY_WIDTH, DISPLAY_HEIGHT);

        timer = new Timer();
        timer.Enabled   = true;
        timer.Interval  = 50;
        timer.Tick     += new EventHandler(TimerTick);

        animator = new SunAnimator(32);
    }

    public void Dispose()
    {
        timer.Dispose();
    }

    private Tuple<Point, Point> TransformToDisplayPoints(Tuple<Cartesian, Cartesian> vector)
    {
        var p1 = vector.Item1.Scale(DISPLAY_EARTH_RADIUS / Constants.Earth.MEAN_RADIUS).ToPoint();
        var p2 = vector.Item2.Scale(50.0).ToPoint();

        p1.Offset(DISPLAY_CENTER);
        p2.Offset(p1);

        return Tuple.Create(p1, p2);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        DrawEarth(e.Graphics);
        DrawSegments(e.Graphics);
    }

    private void DrawEarth(Graphics graphics)
    {
        graphics.FillEllipse(Brushes.Aqua,
                             DISPLAY_CENTER.X - DISPLAY_EARTH_RADIUS + 1,
                             DISPLAY_CENTER.Y - DISPLAY_EARTH_RADIUS + 1,
                             2 * DISPLAY_EARTH_RADIUS,
                             2 * DISPLAY_EARTH_RADIUS);
    }

    private void DrawSegments(Graphics graphics)
    {
        var segments = animator.computeFrame().Select(TransformToDisplayPoints);

        foreach (var pair in segments)
            DrawSegment(graphics, pair.Item1, pair.Item2);
    }

    private void DrawSegment(Graphics graphics, Point p1, Point p2)
    {
        graphics.DrawLine(Pens.Red, p1, p2);
        graphics.FillEllipse(Brushes.Green, p1.X - 2, p1.Y - 2, 5, 5);
        graphics.FillEllipse(Brushes.Blue , p2.X - 2, p2.Y - 2, 5, 5);
    }

    private void TimerTick(object sender, System.EventArgs e)
    {
        animator.nextFrame();
        Invalidate();
    }
}
