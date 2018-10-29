using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BUTTONSTATE
{
    OFF = 0,
    ON = 1,
    FLAGGED = 2,
    QUESTIONED = 3,
}
public class TileButton : MonoBehaviour {

    // [HideInInspector]
    public SkinData m_SkinData;
    public BUTTONSTATE m_State = BUTTONSTATE.ON;
    public int m_TileValue;
    private SpriteRenderer m_SpriteRender;
    private Board m_Board;
    private LevelManager m_GameManger;

    // Use this for initialization
    void Start() {
        m_GameManger = FindObjectOfType<LevelManager>();
        m_SkinData = (SkinData)m_GameManger.Skin;
        m_TileValue = GetComponent<TileID>().m_TileID;
        m_SpriteRender = GetComponent<SpriteRenderer>();
        if (m_SpriteRender.sprite == null) m_SpriteRender.sprite = m_SkinData.m_ButtonUp;
        m_Board = GetComponentInParent<Board>();
    }
    void OnMouseOver()
    {
        if (Input.GetButtonUp("Fire1") && m_State == BUTTONSTATE.ON)
        {
            m_SpriteRender.sprite = m_SkinData.m_ButtonPressed;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            if (m_State == BUTTONSTATE.ON) m_State = BUTTONSTATE.FLAGGED;
            else if (m_State == BUTTONSTATE.FLAGGED) m_State = BUTTONSTATE.QUESTIONED;
            else if (m_State == BUTTONSTATE.QUESTIONED) m_State = BUTTONSTATE.ON;

            if (m_State == BUTTONSTATE.FLAGGED)
            {
                if (!m_SkinData.m_IsAnimated) m_SpriteRender.sprite = m_SkinData.m_Flagged;

                return;
            }
            else if (m_State == BUTTONSTATE.QUESTIONED)
            {
                if (!m_SkinData.m_IsAnimated) m_SpriteRender.sprite = m_SkinData.m_Question;

                return;
            }
            else if (m_State == BUTTONSTATE.ON)
            {
                if (!m_SkinData.m_IsAnimated) m_SpriteRender.sprite = m_SkinData.m_ButtonUp;
                return;
            }
        }

    }
    // Update is called once per frame
    void FixedUpdate() {
        // if (m_State == BUTTONSTATE.OFF)
        //    CleanUp();
    }

    private void OnMouseDown()
    {
    }
    private void OnMouseUp()
    {
        if (m_State == BUTTONSTATE.ON)
            ClearButton();
    }
    private void CleanUp()

    {
        Destroy(this);
    }
    public void Deactivate()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }


    public void ClearButton()
    {
        m_State = BUTTONSTATE.OFF;
        m_SpriteRender.sprite = m_SkinData.m_ButtonClear;
        if (m_GameManger.m_GameState == GAMESTATE.PLAYING)
        {
            m_Board.Action(m_TileValue, transform.localPosition);
        }
    }

}
