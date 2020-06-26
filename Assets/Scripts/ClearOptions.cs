using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearOptions : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GamePrefs.Difficulty = 0;
        GamePrefs.Size = 0;
	}

}
