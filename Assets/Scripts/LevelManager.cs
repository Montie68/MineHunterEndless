using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GAMESTATE
{
    PLAYING,
    WON,
    LOSE,
    GAMEOVER

}
public abstract class LevelManager : MonoBehaviour {
    
    [HideInInspector]
    public GAMESTATE m_GameState = GAMESTATE.PLAYING;
    [HideInInspector]
    public List<MineTile> m_Board;
    [Header("Board Parameters")]
    public int m_Mines = 0;
    public float m_MineDesity = 6.4f;
    public Board m_GameBoard;
    public int seed = 0;

[Header("Skin/Theme")]
    public ScriptableObject Skin;

    void Awake()
    {

	}

    public virtual void CreateBoard()
    {    }

    public void SetState  (GAMESTATE NewState)
    {
        m_GameState = NewState;
    }
    // Update is called once per frame
}
