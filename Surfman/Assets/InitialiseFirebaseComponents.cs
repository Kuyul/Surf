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
        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        yield return new WaitForSeconds(1);
        LoginHandler.SetActive(true);
        DatabaseHandler.SetActive(true);
        StorageHandler.SetActive(true);
        LeaderboardController.SetActive(true);
    }
}
