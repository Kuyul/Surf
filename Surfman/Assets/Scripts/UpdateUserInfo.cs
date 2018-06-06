using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUserInfo : MonoBehaviour {

    public bool Initialised = false;

    private void OnEnable()
    {
        if (!Initialised)
        {
            LeaderboardControl.Instance.PopulateLeaderBoard();
            LeaderboardControl.Instance.UpdateProfilePic();
            Initialised = true;
        }
        LeaderboardControl.Instance.UpdateHighScore();
    }
}
