using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DoozyUI;

[System.Serializable]
public class toggleValues
{
    public Toggle toggle;
    public int value;
}

public class ToggleValue : MonoBehaviour
{
    public bool Difficulty;
    public bool Size;

    public List<toggleValues> toggleVal;
    private UIManager.Orientation orientation;

    void Start()
    {
        orientation = UIManager.Instance.currentOrientation;
    }

    void ChangeDiff(toggleValues tog)
    {

        GamePrefs.Difficulty = tog.value;
    }
    void ChangeSize(toggleValues tog)
    {
        GamePrefs.Size = tog.value;

    }

    private void SwitchOrientaion()
    {
        for (int i = 0; i < toggleVal.Count; i++)
        {

            if (Difficulty)
            {
                if (i == GamePrefs.Difficulty)
                    toggleVal[i].toggle.isOn = true;
            }
            else if (Size)
            {
                if (i == GamePrefs.Size)
                    toggleVal[i].toggle.isOn = true;
            }

        }

    }

    void Update()
    {
        if (orientation != UIManager.Instance.currentOrientation)
        {
            SwitchOrientaion();
            orientation = UIManager.Instance.currentOrientation;
        }
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


