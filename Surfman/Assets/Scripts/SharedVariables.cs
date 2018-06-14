using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedVariables : MonoBehaviour {

    public List<LeaderboardEntry> Leaders { get; set; }

    private static SharedVariables _instance;

    public static SharedVariables Instance
    {
        get
        {
            //create a new gameobject in the scene
            if (_instance == null)
            {
                GameObject sv = new GameObject("SharedVariables");
                sv.AddComponent<FacebookManager>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _instance = this;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
