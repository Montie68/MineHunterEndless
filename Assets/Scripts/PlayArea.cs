using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayArea : MonoBehaviour
{
    public Camera m_MainCamera;
    public GameObject m_ButtonUp;


    public virtual void Action(int TileValue, Vector3 position) { }
    public virtual void Reveal(int TileValue, Vector3 position) { }
    public virtual void ClearEmpties(Vector3 pos) { }
    public virtual void PlaceButtons() { }
    public virtual List<MineTile> GetPlayArea() { return null; }
    public virtual LevelManager GetGameManager() { return null; }


}