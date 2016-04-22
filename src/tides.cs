using System;
using System.Drawing;
using System.Windows.Forms;

public class Tides : Form
{
    private TidalVectors vectors = new TidalVectors();

    static public void Main ()
    {
        Application.Run(new Tides());
    }

    public Tides ()
    {
        Text = "Tides";
        Size = new System.Drawing.Size(1000, 1000);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        DrawIt(e.Graphics);
    }

    private void DrawIt(Graphics graphics)
    {
        Rectangle rectangle = new Rectangle(200, 200, 600, 600);
        graphics.DrawEllipse(System.Drawing.Pens.Black, rectangle);
        vectors.Draw(graphics);
    }
}
