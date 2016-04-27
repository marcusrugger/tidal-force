using System;


interface ITideWindow
{
    void UpdateDisplay();
}


class Controller
{
    private ITideWindow window;
    private IAnimator animator;
    private bool isPaused;
    private bool isFast;

    public Controller(ITideWindow window)
    {
        this.window   = window;
        this.animator = new MoonAnimator(32);
        this.isPaused = true;
        this.isFast   = false;
    }

    public void StartMoonAnimation()
    {
        animator = new MoonAnimator(32);
        animator.Fast = isFast;
        isPaused = false;
    }

    public void StartSunAnimation()
    {
        animator = new SunAnimator(32);
        animator.Fast = isFast;
        isPaused = false;
    }

    public void StartSunMoonAnimation()
    {
        animator = new SunMoonAnimator(32);
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
        window.UpdateDisplay();
    }

    public void NextFrame()
    {
        if (!isPaused)
        {
            animator.NextFrame();
            window.UpdateDisplay();
        }
    }

    public void Draw(IPresenter presenter)
    {
        presenter.DrawEarth();
        animator.Draw(presenter);
    }
}
