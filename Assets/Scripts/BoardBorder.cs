using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBorder : Border {
    private List<MineTile> m_Tiles;
    public ClassicGameManager m_GameManager;

    private List<GameObject> m_BorderTiles;

    public void Awake()
    {
        m_BorderEdgeCorner = m_Skin.m_BorderEdgeCorner;
        m_BorderEdgeHorizontal = m_Skin.m_BorderEdgeHorizontal;
        m_BorderEdgeVertical = m_Skin.m_BorderEdgeVertical;
    }
    private void Start()
    {
        m_Tiles = m_PlayArea.GetPlayArea();
        m_GameManager = (ClassicGameManager)m_PlayArea.GetGameManager();
        BuildBorders();
    }

    public override void BuildBorders()
    {
        GameObject tilePiece;
        foreach (MineTile tile in m_Tiles)
        {

            Vector3 pos = new Vector3();
            

            if (tile.Position.x == 0)
            {
                tilePiece = m_BorderEdgeVertical;
                pos = new Vector3(tile.Position.x - 0.5f, tile.Position.y);
                PlaceBorder(tilePiece,  pos);
            }
            else if (tile.Position.x == m_GameManager.m_BoardHieght - 1)
            {
                tilePiece = m_BorderEdgeVertical;
                pos = new Vector3(tile.Position.x + 0.5f, tile.Position.y);
                PlaceBorder(tilePiece,  pos);
            }

            if (tile.Position.y == 0)
            {
                tilePiece = m_BorderEdgeHorizontal;
                pos = new Vector3(tile.Position.x, tile.Position.y - 0.5f);
                PlaceBorder(tilePiece,  pos);
            }
            else if (tile.Position.y == m_GameManager.m_BoardWidth - 1)
            {
                tilePiece = m_BorderEdgeHorizontal;
                pos = new Vector3(tile.Position.x, tile.Position.y + 0.5f);
                PlaceBorder(tilePiece,  pos);
            }

            pos = placeConer(tile, pos);

        }
    }

    private Vector3 placeConer(MineTile tile, Vector3 pos)
    {
        if (tile.Position.x == 0 && tile.Position.y == m_GameManager.m_BoardWidth - 1)
        {
            pos = new Vector3(tile.Position.x - 0.5f, tile.Position.y + 0.5f);
            PlaceBorder(m_BorderEdgeCorner,  pos);
        }
        if (tile.Position.x == m_GameManager.m_BoardHieght - 1 && tile.Position.y == 0)
        {
            pos = new Vector3(tile.Position.x + 0.5f, tile.Position.y - 0.5f);
            PlaceBorder(m_BorderEdgeCorner,  pos);
        }
        if (tile.Position.x == m_GameManager.m_BoardHieght - 1 && tile.Position.y == m_GameManager.m_BoardWidth - 1)
        {
            pos = new Vector3(tile.Position.x + 0.5f, tile.Position.y + 0.5f);
            PlaceBorder(m_BorderEdgeCorner,  pos);
        }
        if (tile.Position.x == 0 && tile.Position.y == 0)
        {
            pos = new Vector3(tile.Position.x - 0.5f, tile.Position.y - 0.5f);
            PlaceBorder(m_BorderEdgeCorner,  pos);
        }

        return pos;
    }

    private void PlaceBorder(GameObject tile,  Vector3 pos)
    {
        GameObject clone = Instantiate(tile, Vector3.zero, Quaternion.identity);
        clone.transform.parent = gameObject.transform;
        clone.transform.localPosition = pos;
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
}
