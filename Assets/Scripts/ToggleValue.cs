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
        foreach(toggleValues tog in toggleVal)
        {
            tog.toggle.onValueChanged.AddListener( delegate { ChangeValue(tog.value); });
        }
    }

    public void ChangeDiff(toggleValues tog)
    {
        Debug.LogError("change diff");
        GamePrefs.Difficulty = tog.value;
    }
    public void ChangeSize(toggleValues tog)
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
                else
                    toggleVal[i].toggle.isOn = false;
            }
            else if (Size)
            {
                if (i == GamePrefs.Size)
                    toggleVal[i].toggle.isOn = true;
                else
                    toggleVal[i].toggle.isOn = false;
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
    }

    public void ChangeValue(int val)
    {
        for (int i = 0; i < toggleVal.Count; i++)
        {

            if (Difficulty)
            {
                if (i == val)
                    ChangeDiff(toggleVal[i]);
            }
            else if (Size)
            {
                if (i == val)
                    ChangeSize(toggleVal[i]);
            }

        }
    }
}


