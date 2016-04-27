using System;
using System.Collections.Generic;
using System.Linq;


interface IAnimator
{
    void NextFrame();
    void Draw(IPresenter presenter);

    void Reset();
    bool Fast
    { set; }
}


abstract class Animator : IAnimator
{
    protected int frame_step;
    protected TidalVectors vectorGenerator;

    public Animator(int vectorCount)
    {
        this.frame_step = 1;
        this.vectorGenerator = new TidalVectors(vectorCount);
    }

    public abstract void NextFrame();
    public abstract void Draw(IPresenter presenter);

    public virtual void Reset()
    {
        Fast = false;
    }

    public bool Fast
    {
        set { frame_step = value ? 5 : 1; }
    }

}
