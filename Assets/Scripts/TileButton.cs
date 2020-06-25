using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BUTTONSTATE
{
    OFF = 0,
    ON = 1,
    FLAGGED = 2,
    QUESTIONED = 3,
}
public class TileButton : MonoBehaviour
{

    [HideInInspector]
    public SkinData m_SkinData;
    [Header("Timer Settings")]
    [Tooltip("Number of Seconds player needs to press and hold to cycle flags/question marks.")]
    public float m_HoldTime = 2f;
    [Header("Tile States")]
    public BUTTONSTATE m_State = BUTTONSTATE.ON;
    public int m_TileValue;
    private SpriteRenderer m_SpriteRender;
    private Board m_Board;
    private LevelManager m_GameManger;

    private float m_timerhold = 0;
    private float m_timerLasthold;
    private bool m_oneClick = false;
    private bool m_HoldClick = false;
    private GameTimer m_gameTimer;
    private Touch m_Touch;
    private Slider m_slider;

    // Use this for initialization
    void Start()
    {
        m_GameManger = FindObjectOfType<LevelManager>();
        m_SkinData = (SkinData)m_GameManger.Skin;
        m_TileValue = GetComponent<TileID>().m_TileID;
        m_SpriteRender = GetComponent<SpriteRenderer>();
        if (m_SpriteRender.sprite == null) m_SpriteRender.sprite = m_SkinData.m_ButtonUp;
        m_Board = GetComponentInParent<Board>();
        m_gameTimer = GameObject.FindObjectOfType<GameTimer>();
        m_slider = GetComponentInChildren<Slider>();
    }

    void Update()
    {

        if (m_oneClick && m_State == BUTTONSTATE.ON)
        {
            if (!m_gameTimer.getIsPlaying()) m_gameTimer.startTimer();

            m_SpriteRender.sprite = m_SkinData.m_ButtonPressed;
            ClearButton();
            StopCoroutine(CheckForLongPress());
            m_oneClick = false;
            m_timerhold = 0;
        }

        if (m_HoldClick)
        {
            if (!m_gameTimer.getIsPlaying()) m_gameTimer.startTimer();

            if (m_State == BUTTONSTATE.ON) m_State = BUTTONSTATE.FLAGGED;
            else if (m_State == BUTTONSTATE.FLAGGED) m_State = BUTTONSTATE.QUESTIONED;
            else if (m_State == BUTTONSTATE.QUESTIONED) m_State = BUTTONSTATE.ON;

            if (m_State == BUTTONSTATE.FLAGGED)
            {
                if (!m_SkinData.m_IsAnimated) m_SpriteRender.sprite = m_SkinData.m_Flagged;
            }
            else if (m_State == BUTTONSTATE.QUESTIONED)
            {
                if (!m_SkinData.m_IsAnimated) m_SpriteRender.sprite = m_SkinData.m_Question;
            }
            else if (m_State == BUTTONSTATE.ON)
            {
                if (!m_SkinData.m_IsAnimated) m_SpriteRender.sprite = m_SkinData.m_ButtonUp;
            }
            StopCoroutine(CheckForLongPress());
            m_HoldClick = false;
        }

    }

    private void OnMouseOver()
    {
        if (Input.touchCount > 0 && Input.touchCount < 2)
        {
            if (!m_oneClick && !m_HoldClick)
            {
                StartCoroutine(CheckForLongPress());
            }
            
        }
#if UNITY_EDITOR
        else  if (Input.GetMouseButtonDown(0))
        {
            m_oneClick = true;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            m_HoldClick = true;
        }
#endif

    }
    
    private IEnumerator CheckForLongPress()
    {
        if (Input.touchCount > 0)
        {
            Touch theTouch = Input.GetTouch(0);
            if (theTouch.phase == TouchPhase.Began)
            { // If the user puts her finger on screen...

                m_timerhold = 0;
            }
            else if (theTouch.phase == TouchPhase.Stationary)
            {
                m_timerhold += Input.GetTouch(0).deltaTime * 5;
                m_slider.value = m_timerhold;
            }
            if (theTouch.phase == TouchPhase.Ended)
            { // If the user raises her finger from screen
                m_slider.value = 0;

                if (m_timerhold > m_HoldTime)
                { // Is the time pressed greater than our time delay threshold?
                    m_HoldClick = true;
                }
                else m_oneClick = true;
            }
            yield return null;
        }
    }
    /*
#if UNITY_EDITOR

    private IEnumerator CheckForLongClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_timerhold += Time.deltaTime * 5;
            m_slider.value = m_timerhold;
        }
        else if (Input.GetMouseButtonUp(0))
        { // If the user raises her finger from screen
            m_slider.value = 0;

            if (m_timerhold > m_HoldTime)
            { // Is the time pressed greater than our time delay threshold?
                m_HoldClick = true;
            }
            else m_oneClick = true;
        }
        yield return null;
    }
#endif
*/
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
