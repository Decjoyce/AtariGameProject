using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods {

    public static float Map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    public static string ReplaceAt(this string input, int index, char newChar)
    {
        if (input == null)
        {
            throw new ArgumentNullException("input");
        }
        char[] chars = input.ToCharArray();
        chars[index] = newChar;
        return new string(chars);
    }

    public static int AmountOf(this string input, char character)
    {
        int i = 0;
        foreach(char c in input)
        {
            if (c == character)
                i++;
        }
        return i;
    }

    public static float ModularClamp(float val, float min, float max, float rangemin = -180f, float rangemax = 180f)
    {
        var modulus = Mathf.Abs(rangemax - rangemin);
        if ((val %= modulus) < 0f) val += modulus;
        return Mathf.Clamp(val + Mathf.Min(rangemin, rangemax), min, max);
    }

    public static bool CheckTags(string[] theTags, Transform theThing)
    {
        foreach(string tag in theTags) 
        {
            return theThing.tag == tag;
        }
        return false;
    }
}
  
