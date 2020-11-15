using System.Collections.Generic;
using System;

public abstract class RandomizationType 
{
    public string Name { get; set; }
    public UnityEngine.Sprite Sprite { get; set; }
    public bool IsRandom { get; set; }
    public string Label { get; set; }

    public abstract object GetRandomValue();
    public abstract void SetValues(List<object> list = null);
    
    //return the randomization specific values like Mi/Sigma for Normal Distribution
    public abstract List<object> GetValues();

    public Random GetRandomizer()
    {
        return ApplicationManager.instance.GetRandomizer();
    }
}
