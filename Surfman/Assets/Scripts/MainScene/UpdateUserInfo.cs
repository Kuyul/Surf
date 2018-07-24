using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Update Profile pic on loading the leaderboard screen.
 */
public class UpdateUserInfo : MonoBehaviour {

    public bool Initialised = false;

    private void OnEnable()
    {
        if (!Initialised)
        {
            try
            {
                LeaderboardControl.Instance.UpdateProfilePic();
                //LeaderboardControl.Instance.UpdateHighScore();
                Initialised = true;
            }
            catch (Exception e1)
            {
                Debug.Log(e1.ToString());
                // do nothing
            }

        }
    }
}
