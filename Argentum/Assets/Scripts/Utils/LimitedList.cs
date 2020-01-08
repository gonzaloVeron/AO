using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedList<T>
{
    public int size;

    public T[] array;

    public LimitedList(int limit)
    {
       this.size = 0;
       this.array = new T[limit];
    }

    public void Add(T obj)
    {
        if(size + 1 > size)
        {
            array.SetValue(obj, size);
            size += 1;
        }
        else
        {
            throw new System.IndexOutOfRangeException();
        }
    }

    public void Remove(T obj)
    {
        for(int i = 0; i < array.Length; i++)
        {
            if(this.array[i].Equals(obj))
            {
                this.array.SetValue(null, i);
                this.size -= 1;
            }
        }
    }

    public T Find(System.Predicate<T> match)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (match.Invoke(this.array[i]))
            {
                return this.array[i];
            }
        }
        throw new System.ArgumentNullException();
    }

}
