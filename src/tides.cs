using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class Tides : Form
{
    private IAnimator animator;
    private bool isPaused;

    private const int DISPLAY_WIDTH        = 1000;
    private const int DISPLAY_HEIGHT       = 1000;

    private Timer timer;
    private ToolBar toolBar;

    static public void Main ()
    {
        Application.Run(new Tides());
    }

    public Tides ()
    {
        isPaused = true;

        Text = "Tides";
        Size = new Size(DISPLAY_WIDTH, DISPLAY_HEIGHT);
        DoubleBuffered = true;

        timer = new Timer();
        timer.Enabled   = true;
        timer.Interval  = 33;
        timer.Tick     += AdvanceAnimationOnTimer;

        this.MouseClick += ToggleAnimatorOnMouseClick;

        CreateToolbar();

        animator = new MoonAnimator(32);
    }

    private void CreateToolbar()
    {
        toolBar = new ToolBar();
        toolBar.ButtonClick += ToolbarButtonClick;
        Controls.Add(toolBar);

        ToolBarButton button1 = new ToolBarButton();
        ToolBarButton button2 = new ToolBarButton();
        ToolBarButton button3 = new ToolBarButton();

        button1.Text = "Moon";
        button2.Text = "Sun";
        button3.Text = "Earth";

        toolBar.Buttons.Add(button1);
        toolBar.Buttons.Add(button2);
        toolBar.Buttons.Add(button3);
    }

    public void Dispose()
    {
        timer.Dispose();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var presenter = Presenter.Create(e.Graphics, Size);
        presenter.DrawEarth();
        animator.Draw(presenter);
    }

    private void AdvanceAnimationOnTimer(object sender, System.EventArgs e)
    {
        if (!isPaused)
        {
            animator.nextFrame();
            Invalidate();
        }
    }

    private void ToggleAnimatorOnMouseClick(object sender, MouseEventArgs e)
    {
        isPaused = !isPaused;
    }

    private void ToolbarButtonClick(Object sender, ToolBarButtonClickEventArgs e)
    {
        switch (toolBar.Buttons.IndexOf(e.Button))
        {
            case 0:
                animator = new MoonAnimator(32);
                break;

            case 1:
                animator = new SunAnimator(32);
                break;

            case 2:
                animator = new SunMoonAnimator(32);
                break;
        }
    }
}
