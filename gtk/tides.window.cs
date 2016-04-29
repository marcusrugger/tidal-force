using Gtk;
using System;


class TidesWindow : Window, ITidesWindow
{
    ITidesController controller;

    public TidesWindow(Func<ITidesWindow, ITidesController> fnCreateController) : base("Tidal Forces")
    {
        this.controller = fnCreateController(this);

        SetDefaultSize(1000, 1000);

        DeleteEvent += (obj, args) => Application.Quit();

        GLib.Timeout.Add(33, () => { controller.NextFrame(); return true; });

        this.controller.StartMoonAnimation();
    }

    protected override bool OnDrawn(Cairo.Context context)
    {
        bool result = base.OnDrawn(context);
        controller.Draw( TidesPresenter.Create(context) );
        return result;
    }

    public void Redraw()
    {
        QueueDraw();
    }
}
