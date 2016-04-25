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

    private Timer timer;

    static public void Main ()
    {
        Application.Run(new Tides());
    }

    public Tides ()
    {
        Text = "Tides";
        Size = new Size(DISPLAY_WIDTH, DISPLAY_HEIGHT);

        timer = new Timer();
        timer.Enabled   = true;
        timer.Interval  = 50;
        timer.Tick     += new EventHandler(TimerTick);

        animator = new MoonAnimator(32);
    }

    public void Dispose()
    {
        timer.Dispose();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var presenter = Presenter.Create(e.Graphics, Size);
        presenter.DrawEarth();
        animator.Draw(presenter);
    }

    private void TimerTick(object sender, System.EventArgs e)
    {
        animator.nextFrame();
        Invalidate();
    }
}
