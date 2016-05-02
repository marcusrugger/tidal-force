using System;
using System.Windows.Forms;

using CreatePresenter = System.Func<System.Drawing.Graphics, DisplayParameters, ITidesPresenter>;

public class Tides
{
    static public void Main ()
    {
        CreatePresenter fnCreatePresenter = (c, p) =>
        {
            var flatlandContext = Flatland.GdiPlus.Context.Create(c);
            var flatlandCanvas  = Flatland.Common.Canvas.Create(flatlandContext);
            return TidesFlatlandPresenter.Create(flatlandCanvas, p);
        };

        Application.Run( new TidesWindow(TidesController.Create, fnCreatePresenter) );
    }
}
