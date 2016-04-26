using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class TidesForm : Form
{
    private IAnimator animator;
    private bool isPaused;

    private const int DISPLAY_WIDTH        = 1000;
    private const int DISPLAY_HEIGHT       = 1000;

    enum ToolbarButtons
    {
        MOON_ANIMATION = 0,
        SUN_ANIMATION,
        SUN_MOON_ANIMATION,
        SEPARATOR,
        PAUSE,
        SLOW,
        FAST,
        SEPARATOR2,
        RESET
    };

    private Timer timer;
    private ToolBar toolBar;

    public TidesForm()
    {
        isPaused = true;

        SetupForm();
        CreateTimer();
        CreateToolbar();

        this.MouseClick += ToggleAnimatorOnMouseClick;

        animator = new MoonAnimator(32);
    }

    private void SetupForm()
    {
        this.Text = "Tides";
        this.Size = new Size(DISPLAY_WIDTH, DISPLAY_HEIGHT);
        this.DoubleBuffered = true;
    }

    private void CreateTimer()
    {
        timer = new Timer();
        timer.Enabled   = true;
        timer.Interval  = 33;
        timer.Tick     += AdvanceAnimationOnTimer;
    }

    private void CreateToolbar()
    {
        toolBar = new ToolBar();
        toolBar.ButtonClick += ToolbarButtonClick;
        Controls.Add(toolBar);

        ToolBarButton separator = new ToolBarButton();
        separator.Style = ToolBarButtonStyle.Separator;

        Action<string> AddButton = text =>
        {
            ToolBarButton button = new ToolBarButton();
            button.Text = text;
            toolBar.Buttons.Add(button);
        };

        AddButton("Moon");
        AddButton("Sun");
        AddButton("Earth");
        toolBar.Buttons.Add(separator);
        AddButton("Pause");
        AddButton("Slow");
        AddButton("Fast");
        toolBar.Buttons.Add(separator);
        AddButton("Reset");
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
        switch ((ToolbarButtons) toolBar.Buttons.IndexOf(e.Button))
        {
            case ToolbarButtons.MOON_ANIMATION:
                animator = new MoonAnimator(32);
                isPaused = false;
                break;

            case ToolbarButtons.SUN_ANIMATION:
                animator = new SunAnimator(32);
                isPaused = false;
                break;

            case ToolbarButtons.SUN_MOON_ANIMATION:
                animator = new SunMoonAnimator(32);
                isPaused = false;
                break;

            case ToolbarButtons.PAUSE:
                isPaused = true;
                break;

            case ToolbarButtons.SLOW:
                animator.Fast = false;
                isPaused = false;
                break;

            case ToolbarButtons.FAST:
                animator.Fast = true;
                isPaused = false;
                break;

            case ToolbarButtons.RESET:
                animator.Reset();
                isPaused = true;
                Invalidate();
                break;
        }
    }
}
