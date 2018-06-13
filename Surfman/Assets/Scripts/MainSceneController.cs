using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour {

    public Text highScoreInSetting;
    public Text highScoreInMainMenu;

	// Use this for initialization
	void Start () {
     

    }
	
	// Update is called once per frame
	void Update () {
        highScoreInSetting.text = "Current Highscore is " + PlayerPrefs.GetInt("highscore", 0);

        //quit game when back button is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        Application.Quit();
    }
}
