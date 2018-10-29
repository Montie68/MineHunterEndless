using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour {
    [HideInInspector]
    public static int m_Time;
    [HideInInspector]
    public static bool m_IsPlaying;

	// Use this for initialization
	void Awake () {
        m_Time = 0;
        m_IsPlaying = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
