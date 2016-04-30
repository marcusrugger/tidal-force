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

        CreateToolbar();

        GLib.Timeout.Add(33, () => { controller.NextFrame(); return true; });
    }
    
    private void CreateToolbar()
    {
        Toolbar toolbar = new Toolbar();
        toolbar.ToolbarStyle = ToolbarStyle.Text;

        System.Action<string, EventHandler> addButton = (text, action) =>
        {
            ToolButton button = new ToolButton(Stock.New);
            button.Label = text;
            button.Clicked += action;
            toolbar.Insert(button, -1);
        };

        addButton( "Moon" , (obj, args) => controller.StartMoonAnimation()    );
        addButton( "Sun"  , (obj, args) => controller.StartSunAnimation()     );
        addButton( "Earth", (obj, args) => controller.StartSunMoonAnimation() );
        toolbar.Insert(new SeparatorToolItem(), -1);
        addButton( "Pause", (obj, args) => controller.TogglePause()           );
        addButton( "Slow" , (obj, args) => controller.Slow()                  );
        addButton( "Fast" , (obj, args) => controller.Fast()                  );
        toolbar.Insert(new SeparatorToolItem(), -1);
        addButton( "Reset", (obj, args) => controller.Reset()                 );

        VBox vbox = new VBox(false, 2);
        vbox.PackStart(toolbar, false, false, 0);
        Add(vbox);
    }

    protected override bool OnDrawn(Cairo.Context context)
    {
        bool result = base.OnDrawn(context);

        int width  = 0;
        int height = 0;
        GetSize(out width, out height);
        controller.Draw( TidesPresenter.Create(context, width, height) );
        return result;
    }

    public void Redraw()
    {
        QueueDraw();
    }
}
