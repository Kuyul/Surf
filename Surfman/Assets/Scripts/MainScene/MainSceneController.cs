using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour
{
    public GameObject muteButton;
    public GameObject unmuteButton;

    public Text currencyTextMain;
    public Text currencyTextShop;

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetFloat("gamevolume", 1) == 1)
        {
            muteButton.SetActive(true);
            unmuteButton.SetActive(false);
        }

        if (PlayerPrefs.GetFloat("gamevolume", 1) == 0)
        {
            muteButton.SetActive(false);
            unmuteButton.SetActive(true);
        }

        if (PlayerPrefs.GetInt("ResetLeaderboard", 1) == 0)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("ResetLeaderboard", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //quit game when back button is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        currencyTextMain.text = PlayerPrefs.GetInt("money", 0).ToString();
        currencyTextShop.text = PlayerPrefs.GetInt("money", 0).ToString();
    }
}
