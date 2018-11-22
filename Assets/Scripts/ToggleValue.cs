using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class toggleValues
{
    public Toggle toggle;
    public int value;
}

public class ToggleValue : MonoBehaviour {
    public bool Difficulty;
    public bool Size;

    public List<toggleValues> toggleVal;
    ToggleGroup tgroup;

    void Start()
    {
        tgroup = GetComponent<ToggleGroup>();
    }

    void ChangeDiff(toggleValues tog)
    {

                GamePrefs.Difficulty = tog.value;
    }
    void ChangeSize(toggleValues tog)
    {
        GamePrefs.Size = tog.value;

    }

    void Update()
    {
        foreach (toggleValues tog in toggleVal)
        {
            if (tog.toggle.isOn)
            {
                if (Difficulty) ChangeDiff(tog);
                else if (Size) ChangeSize(tog);
            }
        }
    }
}
