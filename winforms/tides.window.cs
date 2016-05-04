using System;
using System.Drawing;
using System.Windows.Forms;

using CreateController = System.Func<ITidesWindow, ITidesController>;
using CreatePresenter = System.Func<System.Drawing.Graphics, Flatland.Transformer, ITidesPresenter>;


class TidesWindow : Form, ITidesWindow
{
    readonly ITidesController controller;
    readonly CreatePresenter fnCreatePresenter;

    const int DISPLAY_WIDTH        = 1000;
    const int DISPLAY_HEIGHT       = 1000;

    readonly Timer timer;

    public TidesWindow(CreateController fnCreateController, CreatePresenter fnCreatePresenter)
    {
        this.controller = fnCreateController(this);
        this.fnCreatePresenter = fnCreatePresenter;

        SetupForm();
        timer = CreateTimer();
        Controls.Add( new TidesToolbar(controller) );
    }

    private void SetupForm()
    {
        this.Text            = "Tides";
        this.Size            = new Size(DISPLAY_WIDTH, DISPLAY_HEIGHT);
        this.MouseClick     += (sender, e) => controller.TogglePause();
        this.DoubleBuffered  = true;
    }

    private Timer CreateTimer()
    {
        Timer timer = new Timer();
        timer.Enabled   = true;
        timer.Interval  = 33;
        timer.Tick     += (sender, e) => controller.NextFrame();
        return timer;
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
        var presenter = fnCreatePresenter(e.Graphics, Transformer);
        controller.Draw(presenter);
    }
    
    private Flatland.Transformer Transformer
    {
        get
        {
            double display_earth = 0.3 * (double) Math.Min(Size.Width, Size.Height);

            return Flatland.Transformer
                           .Create()
                           .SetTranslation( new Flatland.Cartesian(Size.Width/2, Size.Height/2) )
                           .SetScale( new Flatland.Cartesian(display_earth/Constants.Earth.MEAN_RADIUS, -display_earth/Constants.Earth.MEAN_RADIUS) );
        }
    }

    public void Redraw()
    {
        Invalidate();
    }
}
