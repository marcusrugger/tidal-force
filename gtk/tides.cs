using Gtk;
using System;


public class Tides
{
    static public void Main ()
    {
        Application.Init();

        Func<Cairo.Context, DisplayParameters, ITidesPresenter> fnCreatePresenter = (c, p) =>
        {
            var flatlandContext = Flatland.CairoGraphics.Context.Create(c);
            var flatlandCanvas  = Flatland.Common.Canvas.Create(flatlandContext);
            return TidesFlatlandPresenter.Create(flatlandCanvas, p);
        };

        var window = new TidesWindow(TidesController.Create, fnCreatePresenter);
        window.ShowAll(); 
        Application.Run();
    }
}
