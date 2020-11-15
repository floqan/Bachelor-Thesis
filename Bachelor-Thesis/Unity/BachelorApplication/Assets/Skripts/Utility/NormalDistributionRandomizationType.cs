using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDistributionRandomizationType : RandomizationType
{

    public float? Mi { get; set; }
    public float? Sigma { get; set; }
    public NormalDistributionRandomizationType()
    {
        Name = "Normal Distribution";
        Sprite = Resources.Load<Sprite>("Preview/NormalDistribution");
        Label = "Mi/Sigma";
    }


    public override object GetRandomValue()
    {
        if(Mi == null || Sigma == null)
        {
            return null;
        }

        System.Random rand = GetRandomizer(); //reuse this if you are generating many
        double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                     Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
        float randomValue = (float)Mi + (float)Sigma * (float)randStdNormal; //random normal(mean,stdDev^2)
        
        if(randomValue < Mi - 3 * Sigma)
        {
            randomValue = (float)Mi - 3 * (float)Sigma;
        }
        else if(randomValue > Mi + 3 * Sigma)
        {
            randomValue = (float)Mi + 3 * (float)Sigma;
        }

        return randomValue;
    }

    // 1. Mi
    // 2. Sigma
    public override void SetValues(List<object> list = null)
    {
        if(list.Count == 2)
        {
            Mi = (float?)list[0];
            Sigma = (float?)list[1];
        }
    }

    public override List<object> GetValues()
    {
        List<object> list = new List<object>();
        list.Add(Mi);
        list.Add(Sigma);
        return list;
    }
}
