using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePrefsDebug : MonoBehaviour
{
    #region Public
    //public members go here
    public int size;
    public int diff;
  #endregion

  #region Private
  	//private members go here

  #endregion
  // Place all unity Message Methods here like OnCollision, Update, Start ect. 
  #region Unity Messages 
    void Start()
    {
		
    }
	
    void Update()
    {
        size = GamePrefs.Size;
        diff = GamePrefs.Difficulty;
    }
  #endregion
  #region Public Methods
	//Place your public methods here

  #endregion
  #region Private Methods
	//Place your public methods here

  #endregion

}
