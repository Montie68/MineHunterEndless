using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePrefs 
{
    public static int difficulty, size;
   
    public static int Difficulty
    {
        get
        {
            return difficulty;
        }
        set 
        {
            difficulty = value;
        }
    }
    public static int Size
    {
        get
        {
            return size;
        }
        set
        {
            size = value;
        }
    }
}
