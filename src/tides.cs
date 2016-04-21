using System;
using System.Windows.Forms;

public class HelloWorld : Form
{
    public const double massEarth = 5.972e24;
    public const double massMoon  = 7.342e22;
    public const double distance  = 370310054.0;

    public delegate double Function(double a);

    private GravitationalForce gforce;
    private Function fnGforce;

    static public void Main ()
    {
        Application.Run(new HelloWorld());
    }

    public HelloWorld ()
    {
        Text = "Tides";
        Size = new System.Drawing.Size(1000, 1000);

        gforce = new GravitationalForce(massMoon);
        fnGforce = new Function(gforce.compute);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        DrawIt();
    }


    private void DrawIt()
    {
        Console.WriteLine("Gforce: " + fnGforce(distance));
        System.Drawing.Graphics graphics = this.CreateGraphics();
        System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(200, 200, 600, 600);
        graphics.DrawEllipse(System.Drawing.Pens.Black, rectangle);
    }
}
