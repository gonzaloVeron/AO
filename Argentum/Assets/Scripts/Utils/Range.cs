using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range
{
    public int start;
    public int end;

    public Range(int start, int end)
    {
        this.start = start;
        this.end = end;
    }

    public List<int> calculateRange()
    {
        var list = new List<int>();
        for(int i = start; i <= end; i++)
        {
            list.Add(i);
        }
        return list;
    }

}
