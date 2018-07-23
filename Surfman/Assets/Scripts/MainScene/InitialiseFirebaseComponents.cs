using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseFirebaseComponents : MonoBehaviour {

    public GameObject LoginHandler;
    public GameObject DatabaseHandler;
    public GameObject StorageHandler;
    public GameObject LeaderboardController;

    public GameObject StartPanel;

    private void Awake()
    {
        //Instantiate ad manger
        Admob.Instance.Awake();
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(WaitForFacebookLogin());
        StartCoroutine(ActivateStartPanel());
    }

    IEnumerator WaitForFacebookLogin()
    {
        if (!FacebookManager.Instance.IsLoggedIn)
        {
            yield return null;
        }
        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        yield return new WaitForSeconds(3);
        LoginHandler.SetActive(true);
        DatabaseHandler.SetActive(true);
        StorageHandler.SetActive(true);
        LeaderboardController.SetActive(true);
        LeaderboardControl.Instance.PopulateLeaderBoard();
    }

    IEnumerator ActivateStartPanel()
    {
        yield return new WaitForSeconds(1);
        StartPanel.SetActive(true);
    }
}
