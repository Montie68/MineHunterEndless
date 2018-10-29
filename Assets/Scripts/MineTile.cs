using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MineTile {

    public Vector3 Position = new Vector3(0, 0, 0);
    public int Value = 0;

    public MineTile (Vector3 pos, int value)
    {
        Position = pos;
        Value = value;
    }
}
