using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
public class Tuple<T, Y>
{
    public ObjectId _id;

    public T item1;

    public Y item2;

    public Tuple(T t, Y y)
    {
        this.item1 = t;
        this.item2 = y;
    }
    public override bool Equals(object obj)
    {
        Tuple<T, Y> tu = (Tuple<T, Y>)obj;
        return this.item1.Equals(tu.item1) && this.item2.Equals(tu.item2);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

}
