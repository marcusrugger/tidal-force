using System;
using System.Windows.Forms;

using CreatePresenter = System.Func<System.Drawing.Graphics, Flatland.Transformer, ITidesPresenter>;

public class Tides
{
    static public void Main ()
    {
        CreatePresenter fnCreatePresenter = (c, t) =>
        {
            var flatlandContext = Flatland.GdiPlus.Context.Create(c, t);
            var flatlandCanvas  = Flatland.Core.Canvas.Create(flatlandContext);
            return TidesFlatlandPresenter.Create(flatlandCanvas);
        };

        Application.Run( new TidesWindow(TidesController.Create, fnCreatePresenter) );
    }
}
