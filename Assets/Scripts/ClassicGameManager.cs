using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClassicGameManager : LevelManager {
    [Header("BoardDimesions")]
    public int m_BoardWidth = 8;
    public int m_BoardHieght = 8;
    private ClassicGameManager m_GameManager;
    [Header("Game Timer")]
    public GameObject m_GameTimer;
    [Header("Game Sizes")]
    [SerializeField]
    public List<GameSizes> m_GameSizes;
    [SerializeField]
    public List<Difficulty> m_Difficulty;

    [Header("Game Debug")]
    public int m_Debug = 99;
    void Awake()
    {
        if (!m_GameManager)
        {
            m_GameManager = this;
           // DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        if (m_Debug != 99)
        {
            GamePrefs.Difficulty = m_Debug;
            GamePrefs.Size = m_Debug;
        }
        if (GamePrefs.Difficulty != -1)
        {
            m_MineDesity = m_Difficulty[GamePrefs.Difficulty].DifficultyRatio;
        }
        if (GamePrefs.Size != -1)
        {
            m_BoardHieght = m_GameSizes[GamePrefs.Size].Height;
            m_BoardWidth = m_GameSizes[GamePrefs.Size].Width;
        }

    }
    // Use this for initialization
    void Start ()
    {
        NewBoard();

        return;
    }

    private void NewBoard()
    {
        if (seed == 0) seed = Random.Range(int.MinValue, int.MaxValue);
        Random.InitState(seed);
        CreateBoard();
    }

    public override void CreateBoard()
    {

        if (m_Mines == 0)
        {
            float boardSize = m_BoardHieght * m_BoardWidth;
            float defaultMineCount = boardSize / m_MineDesity;

            m_Mines = (int)defaultMineCount;
        }
                

        for (int i = 0; i < m_BoardWidth ; i++)
        {
            for (int j = 0; j < m_BoardHieght; j++)
            {
                m_Board.Add(new MineTile(new Vector3(i, j), 0));
            }
        }

        for (int m = 0; m < m_Mines; m++)
        {
            int Tile = Random.Range(0, m_Board.Count - 1);

            if (m_Board[Tile].Value == -1)
            {
                m--;
            }
            else
            {
                m_Board[Tile].Value = -1;
            }
        }

        foreach (MineTile mt in m_Board)
        {
            if (mt.Value == -1) continue;
           for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0) continue;
                    else
                    {
                        foreach (MineTile at in m_Board)
                        {
                            if (at.Position == mt.Position + new Vector3(i,j) )
                            {
                                if (at.Value == -1) mt.Value += 1;
                            }
                        }

                    }
                }

            }
        }
            return;
    }
    public void NewGame()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update () {
		switch (m_GameState)
        {
            case GAMESTATE.WON:
                m_GameBoard.Reveal(99, new Vector3(0,0));
                SetState(GAMESTATE.GAMEOVER);
                m_GameTimer.GetComponent<GameTimer>().setIsPlaying(false);
                break;
            case GAMESTATE.LOSE:
                m_GameBoard.Deactivate();
                m_GameBoard.Reveal(-99, new Vector3(0, 0));
                SetState(GAMESTATE.GAMEOVER);
                m_GameTimer.GetComponent<GameTimer>().setIsPlaying(false);
                break;
                default:
                break;

        }
	}

    internal GAMESTATE GetState()
    {
        return m_GameState;
    }
}

