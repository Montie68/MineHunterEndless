using UnityEngine;
using UnityEditor;

public class ButtonInfo {

    [MenuItem("Assets/Create/Skin Data")]

    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<SkinData>();
    }

}
