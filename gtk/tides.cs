using Gtk;
using System;


public class Tides
{
    static public void Main ()
    {
        Application.Init();

        Func<Cairo.Context, Flatland.Transformer, ITidesPresenter> fnCreatePresenter = (c, t) =>
        {
            var flatlandContext = Flatland.CairoGraphics.Context.Create(c, t);
            var flatlandCanvas  = Flatland.Core.Canvas.Create(flatlandContext);
            return TidesFlatlandPresenter.Create(flatlandCanvas);
        };

        var window = new TidesWindow(TidesController.Create, fnCreatePresenter);
        window.ShowAll(); 
        Application.Run();
    }
}
