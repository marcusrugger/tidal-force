using Gtk;
using System;


public class Tides
{
    static public void Main ()
    {
        Application.Init();
        var window = new TidesWindow(TidesController.Create);
        window.ShowAll(); 
        Application.Run();
    }
}
