using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkinData : ScriptableObject
{
    public string m_SkinName = "Skin Name";
    public bool m_IsAnimated = false;
    [Header("Button State Sprites")]
    public Sprite m_ButtonUp;
    public Sprite m_ButtonPressed;
    public Sprite m_ButtonHighLight;
    [HideInInspector]
    public Sprite m_ButtonClear;
    public Sprite m_Flagged;
    public Sprite m_Question;
    [Header("Button State Animated Sprites")]
    public GameObject m_ButtonUpAnim;
    public GameObject m_ButtonPressedAnim;
    public GameObject m_ButtonHighLightAnim;
    [HideInInspector]
    public GameObject m_ButtonClearAnim;
    public GameObject m_FlaggedAnim;
    public GameObject m_QuestionAnim;
    [Header("Border Tiles")]
    public GameObject m_BorderEdgeHorizontal;
    public GameObject m_BorderEdgeVertical;
    public GameObject m_BorderEdgeCorner;
    [Header("Tile State Objects")]
    public List<GameObject> m_BoardTiles;
    public GameObject m_WrongTile;
}
