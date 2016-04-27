using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


public class TideWindow : Form, ITideWindow
{
    private Controller controller;

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

    public TideWindow()
    {
        controller = new Controller(this);

        SetupForm();
        CreateTimer();
        CreateToolbar();

        this.MouseClick += (sender, e) => controller.TogglePause();
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
        timer.Tick     += (sender, e) => controller.NextFrame();
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

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            timer.Dispose();
        }

        base.Dispose(disposing);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var presenter = GdiPlusPresenter.Create(e.Graphics, Size);
        controller.Draw(presenter);
    }

    private void ToolbarButtonClick(Object sender, ToolBarButtonClickEventArgs e)
    {
        switch ((ToolbarButtons) toolBar.Buttons.IndexOf(e.Button))
        {
            case ToolbarButtons.MOON_ANIMATION:
                controller.StartMoonAnimation();
                break;

            case ToolbarButtons.SUN_ANIMATION:
                controller.StartSunAnimation();
                break;

            case ToolbarButtons.SUN_MOON_ANIMATION:
                controller.StartSunMoonAnimation();
                break;

            case ToolbarButtons.PAUSE:
                controller.TogglePause();
                break;

            case ToolbarButtons.SLOW:
                controller.Slow();
                break;

            case ToolbarButtons.FAST:
                controller.Fast();
                break;

            case ToolbarButtons.RESET:
                controller.Reset();
                break;
        }
    }

    public void UpdateDisplay()
    {
        Invalidate();
    }
}
