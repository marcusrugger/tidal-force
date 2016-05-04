using Gtk;
using System;

using CreateController = System.Func<ITidesWindow, ITidesController>;
using CreatePresenter  = System.Func<Cairo.Context, Flatland.Transformer, ITidesPresenter>;


class TidesWindow : Window, ITidesWindow
{
    readonly ITidesController controller;
    readonly CreatePresenter fnCreatePresenter;

    public TidesWindow(CreateController fnCreateController, CreatePresenter fnCreatePresenter)
    : base("Tidal Forces")
    {
        this.controller = fnCreateController(this);
        this.fnCreatePresenter = fnCreatePresenter;

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
        controller.Draw( fnCreatePresenter(context, Transformer) );
        return result;
    }
    
    private Flatland.Transformer Transformer
    {
        get
        {
            int width  = 0;
            int height = 0;
            GetSize(out width, out height);
            double display_earth = 0.3 * (double) Math.Min(width, height);

            return Flatland.Transformer
                           .Create()
                           .SetTranslation( new Flatland.Cartesian(width/2, height/2) )
                           .SetScale( new Flatland.Cartesian(display_earth/Constants.Earth.MEAN_RADIUS, -display_earth/Constants.Earth.MEAN_RADIUS) );
        }
    }

    public void Redraw()
    {
        QueueDraw();
    }
}
