using System;
using System.Windows.Forms;


public class Tides
{
    static public void Main ()
    {
        Application.Run( new TidesWindow(TidesController.Create) );
    }
}
