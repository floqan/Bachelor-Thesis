using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRandomizationType : RandomizationType
{
    public int minValueR { get; set; }
    public int maxValueR { get; set; }
    
    public int minValueG { get; set; }
    public int maxValueG { get; set; }
    
    public int minValueB { get; set; }
    public int maxValueB { get; set; }

    public ColorRandomizationType()
    {
        Name = "Random Color";
    }
    public override object GetRandomValue()
    {
        int r,g,b;
        if(minValueR == maxValueR)
        {
            r = minValueR;
        }
        else
        {
            r = ApplicationManager.instance.GetRandomizer().Next(minValueR,maxValueR);
        }

        if (minValueG == maxValueG)
        {
            g = minValueG;
        }
        else
        {
            g = ApplicationManager.instance.GetRandomizer().Next(minValueG, maxValueG);
        }

        if (minValueB == maxValueB)
        {
            b = minValueB;
        }
        else
        {
            b = ApplicationManager.instance.GetRandomizer().Next(minValueB, maxValueB);
        }
        
        Color newColor = new Color(r,g,b); 
        return newColor;
    }

    public override void SetValues(List<object> list = null)
    {
        if(list == null || list.Count != 6)
        {
            return;
        }
        minValueR = (int)list[0];
        maxValueR = (int)list[1];
        minValueB = (int)list[2];
        maxValueB = (int)list[3];
        minValueG = (int)list[4];
        maxValueG = (int)list[5];
    }

    public override List<object> GetValues()
    {
        List<object> list = new List<object>();
        list.Add(minValueR);
        list.Add(maxValueR);
        list.Add(minValueG);
        list.Add(maxValueG);
        list.Add(minValueB);
        list.Add(maxValueG);

        return list;
    }
}
