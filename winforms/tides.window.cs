using System;
using System.Drawing;
using System.Windows.Forms;


class TidesWindow : Form, ITidesWindow
{
    private ITidesController controller;

    private const int DISPLAY_WIDTH        = 1000;
    private const int DISPLAY_HEIGHT       = 1000;

    private Timer timer;

    public TidesWindow(Func<ITidesWindow, ITidesController> fnCreateController)
    {
        controller = fnCreateController(this);

        SetupForm();
        CreateTimer();
        Controls.Add( new TidesToolbar(controller) );
    }

    private void SetupForm()
    {
        this.Text            = "Tides";
        this.Size            = new Size(DISPLAY_WIDTH, DISPLAY_HEIGHT);
        this.MouseClick     += (sender, e) => controller.TogglePause();
        this.DoubleBuffered  = true;
    }

    private void CreateTimer()
    {
        timer = new Timer();
        timer.Enabled   = true;
        timer.Interval  = 33;
        timer.Tick     += (sender, e) => controller.NextFrame();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            timer.Dispose();
        }

        base.Dispose(disposing);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var presenter = TidesPresenter.Create(e.Graphics, new DisplayParameters(Size.Width, Size.Height));
        controller.Draw(presenter);
    }

    public void Redraw()
    {
        Invalidate();
    }
}
