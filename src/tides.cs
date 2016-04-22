using System;
using System.Windows.Forms;

public class Tides : Form
{

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
        DrawIt();
    }

    private void DrawIt()
    {
        System.Drawing.Graphics graphics = this.CreateGraphics();
        System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(200, 200, 600, 600);
        graphics.DrawEllipse(System.Drawing.Pens.Black, rectangle);
    }
}
