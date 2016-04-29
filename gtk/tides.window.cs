using Gtk;
using System;


class TidesWindow : Window, ITidesWindow
{
    ITidesController controller;

    public TidesWindow(Func<ITidesWindow, ITidesController> fnCreateController) : base("Tidal Forces")
    {
        this.controller = fnCreateController(this);

        // Set up a button object.
        // Button btn = new Button ("Hello World");
        // btn.Clicked += hello;
        // Add(btn);

        // var graphics = Gtk.DotNet.Graphics.FromDrawable(window);
        // graphics.DrawLine(Pens.Red, new Point(100, 100), new Point(200, 200));

        SetDefaultSize(1000, 1000);

        DeleteEvent += (obj, args) => Application.Quit();
    }

    void hello(object obj, EventArgs args)
    {
        Console.WriteLine("Hello World");
    }

    public void Redraw()
    {
    }
}
