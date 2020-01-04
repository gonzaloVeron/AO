using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuple<T, Y>
{
    public T item1;

    public Y item2;

    public Tuple(T t, Y y)
    {
        this.item1 = t;
        this.item2 = y;
    }
        
}
