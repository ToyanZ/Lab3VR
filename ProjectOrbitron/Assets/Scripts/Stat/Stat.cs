using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public enum Type { Number, Bool, String}
    
    public string displayName = string.Empty;
    
    [Space(15)]
    public Type type = Type.Number;
    public float current = 0;
    public float max = 0;

    [Space(10)]
    public bool boolValue = false;

    [Space(10)]
    public string stringValue= string.Empty;

    float Suma()
    {
        return 1;
    }

    public Stat(Type type, string displayName, float current, float max, bool boolValue, string stringValue)
    {
        this.type = type;
        this.displayName = displayName;
        this.current = current;
        this.max = max;
        this.boolValue = boolValue;
        this.stringValue = stringValue;
    }
}
