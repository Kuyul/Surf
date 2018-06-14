using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseFirebaseComponents : MonoBehaviour {

    public GameObject LoginHandler;
    public GameObject DatabaseHandler;
    public GameObject StorageHandler;
    public GameObject LeaderboardController;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(WaitForFacebookLogin());
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
        yield return new WaitForSeconds(2);
        LoginHandler.SetActive(true);
        DatabaseHandler.SetActive(true);
        StorageHandler.SetActive(true);
        LeaderboardController.SetActive(true);
        LeaderboardControl.Instance.PopulateLeaderBoard();
    }
}
