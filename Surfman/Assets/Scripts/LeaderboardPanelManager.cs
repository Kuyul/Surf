using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardPanelManager : MonoBehaviour {

    public FBScript FacebookScript;
    public GameObject DatabaseHandler;
    public GameObject StorageHandler;
    public GameObject LeaderboardController;

    private void OnEnable()
    {
        FacebookScript.DealWithFBMenus(FacebookManager.Instance.IsLoggedIn);
        DatabaseHandler.SetActive(true);
        StorageHandler.SetActive(true);
        LeaderboardController.SetActive(true);
    }
}
