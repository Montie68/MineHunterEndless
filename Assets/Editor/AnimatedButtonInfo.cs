using UnityEngine;
using UnityEditor;

public class AnimatedButtonInfo {

    [MenuItem("Assets/Create/Animated Skin Data")]

    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<SkinDataAnimated>();
    }

}
