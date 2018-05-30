using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUserInfo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        LeaderboardControl.Instance.UpdateHighScore();
        LeaderboardControl.Instance.UpdateProfilePic();
    }
}
