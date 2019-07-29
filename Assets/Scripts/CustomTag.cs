using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTag : MonoBehaviour
{
    [SerializeField]
    private List<string> tags;
    public bool findTag(string tag)
    {
        if (tags.Contains(tag))
        {
            return true;
        }
        return false;
    }
    public int countTag()
    {
        return this.tags.Count;
    }
}
