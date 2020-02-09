using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListUtils
{
    public ListUtils() { }

    public string listToString(IList ls)
    {
        if (ls.Count == 0) return "[]";
        string st = "[";
        for(int i = 0; i < ls.Count; i++)
        {
            st = st + ls[i] + ", ";
        }
        return st.Substring(0, st.Length - 2) + "]";
    } 
}
