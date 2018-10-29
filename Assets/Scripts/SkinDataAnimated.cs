using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkinDataAnimated : ScriptableObject {
    public string m_SkinName = "Skin Name";
    [Header("Button State Sprites")]
    public Animation m_ButtonUp;
    public Animation m_ButtonPressed;
    public Animation m_ButtonHighLight;
    [HideInInspector]
    public Animation m_ButtonClear;
    public Animation m_Flagged;
    [Header("Border Tiles")]
    public GameObject m_BorderEdgeHorizontal;
    public GameObject m_BorderEdgeVertical;
    public GameObject m_BorderEdgeCorner;
    [Header("Tile State Objects")]
    public List<GameObject> m_BoardTiles;
}
