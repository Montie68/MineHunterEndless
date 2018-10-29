using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : PlayArea {

    private ClassicGameManager m_GameManger;
    private SkinData m_Skin;
    private List<MineTile> m_Board;
    private List<GameObject> m_Tiles;
    private List<TileButton> m_Buttons = null;

    // Use this for initialization
    void Start () {

        if (m_MainCamera != null)
        {
            Camera[] cams = FindObjectsOfType<Camera>();
            foreach (Camera cam in cams)
            {
                if (cam.tag == "MainCamera") m_MainCamera = FindObjectOfType<Camera>();
                break;
            }
        }
        m_GameManger = FindObjectOfType<ClassicGameManager>();
        m_Skin = (SkinData)m_GameManger.Skin;
        m_Tiles = m_Skin.m_BoardTiles;
        if (m_GameManger == null) Debug.LogError("No GameManager in Scene");
        else
        {
            m_Board = m_GameManger.m_Board;
            BuildBoard();
            PlaceButtons();
            ScaleBoardToScreen();
        }
	}

    public override void Action(int TileValue, Vector3 position)
    {
        switch (TileValue)
        {
            case -1:
                Reveal(TileValue, position);
                break;
            case 0:
                ClearEmpties(position);

                break;
            default:
                cleanUp();
                break;
        }
        return;
    }

    public override void Reveal(int TileValue, Vector3 position)
    {
        GameButtonTile[] Mines = GetComponentsInChildren<GameButtonTile>();


        foreach (GameButtonTile mine in Mines)
        {
        /*    if (mine.GetComponent<TileID>().m_TileID != -1)
            {
                continue;
            }*/
            if (m_GameManger.m_GameState == GAMESTATE.WON)
            {
                position = mine.transform.localPosition;
                foreach (TileButton tb in m_Buttons)
                {
                   tb.ClearButton();
                   // cleanUp();
                }
            }
            else if (m_GameManger.m_GameState == GAMESTATE.LOSE)
            {
                position = mine.transform.localPosition;
                foreach (TileButton tb in m_Buttons)
                {
                    if (tb.m_TileValue == -1)
                    {
                        tb.ClearButton();
                        cleanUp();
                    }
                   else if (tb.m_TileValue >= 0 &&
                        (tb.m_State == BUTTONSTATE.FLAGGED ||
                        tb.m_State == BUTTONSTATE.QUESTIONED))
                    {
                        SwapWrong(tb.transform.localPosition);
                        tb.ClearButton();
                        cleanUp();
                    }
                }

            }
             Animator anim = mine.GetComponent<Animator>();

            if (mine.transform.localPosition == position && (TileValue == mine.GetComponent<TileID>().m_TileID  || TileValue == 99) )
            {
                if (m_GameManger.m_GameState == GAMESTATE.WON) anim.SetTrigger("Reveal");
                if (TileValue != 99)
                {
                    m_GameManger.SetState(GAMESTATE.LOSE);
                }
                cleanUp();
            }
        }
    }

    private void SwapWrong(Vector3 position)
    {

        foreach (MineTile tile in m_Board)
        {
            if(tile.Position == position)
            {
               // Destroy(tile);
                GameObject Tile = m_Skin.m_WrongTile; 

                GameObject clone = Instantiate(Tile, position, Quaternion.identity);

                clone.transform.SetParent(this.transform);
                clone.GetComponent<SpriteRenderer>().sortingOrder = 5;
                clone.transform.localPosition = position;

            }
        }
    }

    public override void ClearEmpties(Vector3 pos)
    {

       for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                {
                   if (i == 0 && j == 0) continue;

                    
                    foreach (TileButton at in m_Buttons)
                    {
                        if (at.transform.localPosition == pos + new Vector3(i, j))
                        {
                            if (at.m_TileValue != -1 && at.m_State == BUTTONSTATE.ON)
                            {
                                at.ClearButton();
                            }
                        }
                    }

                }

            }
        }
        cleanUp();
    }

    public override void PlaceButtons()
    {
        foreach (MineTile mt in m_Board)
        {
            GameObject Tile = m_ButtonUp;

            Tile.GetComponent<TileID>().m_TileID = mt.Value;

            GameObject clone = Instantiate(Tile, mt.Position, Quaternion.identity);

            clone.transform.SetParent(this.transform);
        }
        cleanUp();
    }

    private void 
        BuildBoard()
    {
        foreach (MineTile mt in m_Board)
        {
            GameObject Tile = null;
            foreach(GameObject tile in m_Tiles)
            {
                if (tile.GetComponent<TileID>().m_TileID == mt.Value)
                {
                    Tile = tile;
                }
            }

            GameObject clone = Instantiate(Tile, mt.Position, Quaternion.identity );

            clone.transform.SetParent(this.transform);
        }
    }

    void cleanUp()
    {
        m_Buttons = new List<TileButton>();
            TileButton[] tileB = GetComponentsInChildren<TileButton>();
            foreach (TileButton tb in tileB)
            {
                if (tb.m_State != BUTTONSTATE.OFF) m_Buttons.Add(tb);
            }
    }

    public void Deactivate()
    {
        cleanUp();
        if (m_GameManger.m_GameBoard)
        foreach (TileButton tb in m_Buttons)
        {
            tb.Deactivate();
        }
    }
    // Update is called once per frame
    void Update () {

        if (m_Buttons.Count <= m_GameManger.m_Mines)
        {
            m_GameManger.SetState(GAMESTATE.WON);
        }

        if ((Input.deviceOrientation == DeviceOrientation.LandscapeLeft)&&(Screen.orientation != ScreenOrientation.LandscapeLeft))
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            ScaleBoardToScreen();
        }

        if ((Input.deviceOrientation == DeviceOrientation.LandscapeRight)&&(Screen.orientation != ScreenOrientation.LandscapeRight))
        {
            Screen.orientation = ScreenOrientation.LandscapeRight;
            ScaleBoardToScreen();
        }
        if ((Input.deviceOrientation == DeviceOrientation.Portrait) && (Screen.orientation != ScreenOrientation.Portrait))
        {
            Screen.orientation = ScreenOrientation.Portrait;
            ScaleBoardToScreen();
        }
#if UNITY_EDITOR
        ScaleBoardToScreen();
#endif
    }

    void ScaleBoardToScreen()
    {
        float margin = 0;
        if (Screen.width < Screen.height)
        {
            float width = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
            float offset = m_GameManger.m_BoardWidth + 1f;
            transform.localScale = new Vector3(width / offset, width / offset, width / offset);
            margin = (width/ offset) / 2;
        }
        else if (Screen.width > Screen.height)
        {
            float Height = Camera.main.orthographicSize * 2.0f ;
            float offset = m_GameManger.m_BoardHieght + 1f;
            transform.localScale = new Vector3(Height / offset, Height / offset, Height / offset);
            margin = (Height/ offset)/2;
        }


        Vector3 Centre = new Vector3((m_GameManger.m_BoardWidth-1)*margin, (m_GameManger.m_BoardHieght-1)*margin);

        transform.position = -Centre;
    }

    public override List<MineTile> GetPlayArea() { return m_Board; }
    public override LevelManager GetGameManager() { return m_GameManger; }


}
