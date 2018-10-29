using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Border : MonoBehaviour {
    public SkinData m_Skin;
    public PlayArea m_PlayArea;
    [HideInInspector]
    public GameObject m_BorderEdgeHorizontal;
    [HideInInspector]
    public GameObject m_BorderEdgeVertical;
    [HideInInspector]
    public GameObject m_BorderEdgeCorner;

    public virtual void BuildBorders()
    { }

}
