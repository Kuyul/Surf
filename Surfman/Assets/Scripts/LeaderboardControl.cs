using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class LeaderboardControl : MonoBehaviour {

    private static LeaderboardControl _instance;

    public static LeaderboardControl Instance
    {
        get
        {
            //create a new gameobject in the scene
            if (_instance == null)
            {
                GameObject fbm = new GameObject("LeaderboardController");
                fbm.AddComponent<LeaderboardControl>();
            }

            return _instance;
        }
    }

    public LoginHandler LoginHandler;
    public DatabaseHandler DbHandler;
    public StorageHandler StorageHandler;
    public List<Dictionary<string, object>> Leaderboard { get; set; }
    public List<LeaderboardEntry> Leaders { get; set; }
    public GameObject LeaderboardHolder;
    protected Firebase.Auth.FirebaseAuth auth;

    //1st 2nd and 3rd place GameObjects
    public GameObject FirstPic;
    public GameObject FirstName;
    public GameObject SecondPic;
    public GameObject SecondName;
    public GameObject ThirdPic;
    public GameObject ThirdName;

    //Leaderboard entry prefab
    public GameObject LeaderboardEntry;

    //Leaderboard content height must be increased if there are more leaderboard entries than it can fit.
    public GameObject Content;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Leaders = new List<LeaderboardEntry>();
        Leaderboard = new List<Dictionary<string, object>>();
        _instance = this;
    }

	// Use this for initialization
	void Start () {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://surfman-389c5.firebaseio.com/");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void PopulateLeaderBoard()
    {
        FirebaseDatabase.DefaultInstance
      .GetReference("Leaders").OrderByChild("score")
      .GetValueAsync().ContinueWith(task => {
          if (task.IsFaulted)
          {
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;
              if (snapshot != null && snapshot.ChildrenCount > 0)
              {
                  foreach (var childSnapshot in snapshot.Children)
                  {
                      if (childSnapshot.Child("score") == null || childSnapshot.Child("score").Value == null)
                      {
                          Debug.LogError("Bad data in sample.  Did you forget to call SetEditorDatabaseUrl with your project id?");
                          break;
                      }
                      else
                      {
                          Dictionary<string, object> scoreMap = new Dictionary<string, object>();
                          scoreMap["name"] = childSnapshot.Child("name").Value;
                          scoreMap["score"] = childSnapshot.Child("score").Value;
                          scoreMap["email"] = childSnapshot.Child("email").Value;
                          Debug.Log("Retrieved Score for " + scoreMap["name"] + " " + scoreMap["score"]);
                          GameObject leaderEntry = Instantiate(LeaderboardEntry);
                          LeaderboardEntry entry = new LeaderboardEntry(leaderEntry, scoreMap["name"].ToString(), scoreMap["score"].ToString());
                          scoreMap["entry"] = entry;
                          //Add to list of LeaderboardEntries (accessible between all screens)
                          Leaders.Add(entry);
                          Leaderboard.Add(scoreMap); 
                      }
                  }
                  DisplayLoaderboard();
              }
          }
      });
    }

    //Attempt to add highscore when entering Leaderboard Screen
    public void UpdateHighScore()
    {
        //Only add high score when the user is authenticated
        if (FacebookManager.Instance.IsLoggedIn)
        {
            DbHandler.AddScore();
        }
    }

    public void UpdateProfilePic()
    {
        if (FacebookManager.Instance.IsLoggedIn)
        {
            StorageHandler.UploadProfilePic();
        }
    }

    public string GetEmail()
    {
        return LoginHandler.getEmail();
    }

    //Create leaderboard entries for each profile then load them onto the panel
    public void DisplayLoaderboard()
    {
        DownloadLeaderProfiles();
        Debug.Log("Displaying Leaderboard");
        float entryPos = 0.0f;
        bool topDown = true;
        SortLeaderboard(topDown);
        SortLeaders(topDown);
        //Insert First, second and third game items.
        int count = 1;
        foreach (Dictionary<string, object> scoreEntry in Leaderboard)
        {
            string name = "#" + count + "   " + scoreEntry["name"].ToString();
            string score = scoreEntry["score"].ToString();
            LeaderboardEntry e = (LeaderboardEntry)scoreEntry["entry"];
            //Display rank e.g. "#1   Kyle    30000"
            e.SetName(name);
            e.MakeGameObject();
            GameObject entry = e.GetEntryObj();
            RectTransform rf = entry.GetComponent<RectTransform>();
            rf.SetParent(LeaderboardHolder.transform);
            rf.localPosition = new Vector3(0.0f, entryPos, 0.0f);
            rf.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            entryPos -= 200.0f;
            //Increase Leaderboard content height
            RectTransform r = Content.GetComponent<RectTransform>();
            r.sizeDelta = new Vector2(r.sizeDelta.x, r.sizeDelta.y + 200.0f);

            //You know the three big circles on the left side of the leaderboard panel?
            //The below if statements populate those :)
            if (count == 1)
            {
                FirstName.GetComponent<Text>().text = scoreEntry["name"].ToString();
                e.AddTopLeaderPic(FirstPic);
            }else if(count == 2)
            {
                SecondName.GetComponent<Text>().text = scoreEntry["name"].ToString();
                e.AddTopLeaderPic(SecondPic);
            }
            else if(count == 3)
            {
                ThirdName.GetComponent<Text>().text = scoreEntry["name"].ToString();
                e.AddTopLeaderPic(ThirdPic);
            }
            count++;
        }
    }

    //Download user profile picture stored by each users' Email value from Firebase Storage
    public void DownloadLeaderProfiles()
    {
        StorageHandler.DownloadLeaderboardProfiles(Leaderboard);
    }

    //Sort Leaderboard - Parameter topDown: true = top down, false = bottom up.
    public void SortLeaderboard(bool topDown)
    {
        //Top down
        if (topDown)
        {
            Leaderboard.Sort((x, y) => Convert.ToInt32(y["score"]).CompareTo(Convert.ToInt32(x["score"])));
        }
        //Bottom up
        else
        {
            Leaderboard.Sort((x, y) => Convert.ToInt32(x["score"]).CompareTo(Convert.ToInt32(y["score"])));
        }
    }

    public void SortLeaders(bool topDown)
    {
        if (topDown)
        {
            Leaders.Sort((x, y) => Convert.ToInt32(y.getScore()).CompareTo(Convert.ToInt32(x.getScore())));
        }
        else
        {
            Leaders.Sort((x, y) => Convert.ToInt32(x.getScore()).CompareTo(Convert.ToInt32(y.getScore())));
        }
    }
}
