using System;


interface ITidesWindow
{
    void Redraw();
}


interface ITidesController
{
    void StartMoonAnimation();
    void StartSunAnimation();
    void StartSunMoonAnimation();
    void TogglePause();
    void Slow();
    void Fast();
    void Reset();
    void NextFrame();
    void Draw(ITidesPresenter presenter);
}


class TidesController : ITidesController
{
    private ITidesWindow window;
    private IAnimator animator;
    private bool isPaused;
    private bool isFast;

    public static ITidesController Create(ITidesWindow window)
    {
        return new TidesController(window);
    }

    private TidesController(ITidesWindow window)
    {
        this.window   = window;
        this.animator = new MoonAnimator();
        this.isPaused = true;
        this.isFast   = false;
    }

    public void StartMoonAnimation()
    {
        animator = new MoonAnimator();
        animator.Fast = isFast;
        isPaused = false;
    }

    public void StartSunAnimation()
    {
        animator = new SunAnimator();
        animator.Fast = isFast;
        isPaused = false;
    }

    public void StartSunMoonAnimation()
    {
        animator = new SunMoonAnimator();
        animator.Fast = isFast;
        isPaused = false;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
    }

    public void Slow()
    {
        isPaused = false;
        isFast   = false;
        animator.Fast = false;
    }

    public void Fast()
    {
        isPaused = false;
        isFast   = true;
        animator.Fast = true;
    }

    public void Reset()
    {
        animator.Reset();
        isPaused = true;
        isFast   = false;
        window.Redraw();
    }

    public void NextFrame()
    {
        if (!isPaused)
        {
            animator.NextFrame();
            window.Redraw();
        }
    }

    public void Draw(ITidesPresenter presenter)
    {
        presenter.DrawEarth();
        animator.Draw(presenter);
    }
}
