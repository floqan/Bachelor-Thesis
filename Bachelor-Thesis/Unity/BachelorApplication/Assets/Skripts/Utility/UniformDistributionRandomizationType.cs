using System;
using System.Collections.Generic;

public class UniformDistributionRandomizationType : RandomizationType
{
    public float? MinValue { get; set; }
    public float? MaxValue { get; set; }

    public UniformDistributionRandomizationType()
    {
        Name = "Unifom Distribution";
        Sprite = UnityEngine.Resources.Load<UnityEngine.Sprite>("Preview/UniformDistribution");
        Label = "Min - Max";
        MinValue = null;
        MaxValue = null;
    }
    
    public override object GetRandomValue()
    {
        if (MaxValue != null && MinValue != null)
        {
            float diff = (float)MaxValue - (float)MinValue;
            double index = GetRandomizer().NextDouble();
            return (float)(MinValue + (diff * index));
        }
        else return null;
    }

    //1.value = minValue
    //2.value = maxValue
    public override void SetValues(List<object> list = null)
    {
        if(list.Count == 2)
        {
            MinValue = (float?)list[0];
            MaxValue = (float?)list[1];
        }
        else
        {
            throw new MissingMemberException();
        }
    }

    public override List<object> GetValues()
    {
        List<object> list = new List<object>
        {
            MinValue,
            MaxValue
        };
        return list;
    }
}
