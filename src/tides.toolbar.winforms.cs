using System;
using System.Windows.Forms;


class TidesToolbar : ToolBar
{
    private TidesController controller;

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

    public TidesToolbar(TidesController controller) : base()
    {
        this.controller = controller;
        PopulateToolbar();
    }

    private void PopulateToolbar()
    {
        ButtonClick += ToolbarButtonClick;

        ToolBarButton separator = new ToolBarButton();
        separator.Style = ToolBarButtonStyle.Separator;

        Action<string> AddButton = text =>
        {
            ToolBarButton button = new ToolBarButton();
            button.Text = text;
            Buttons.Add(button);
        };

        AddButton("Moon");
        AddButton("Sun");
        AddButton("Earth");
        Buttons.Add(separator);
        AddButton("Pause");
        AddButton("Slow");
        AddButton("Fast");
        Buttons.Add(separator);
        AddButton("Reset");
    }

    private void ToolbarButtonClick(Object sender, ToolBarButtonClickEventArgs e)
    {
        switch ((ToolbarButtons) Buttons.IndexOf(e.Button))
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
}
