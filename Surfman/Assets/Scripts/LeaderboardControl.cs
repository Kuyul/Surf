using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

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

    //Menu items


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Leaders = new List<LeaderboardEntry>();
        _instance = this;
    }

	// Use this for initialization
	void Start () {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://surfman-389c5.firebaseio.com/");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        Leaderboard = new List<Dictionary<string, object>>();
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
                          LeaderboardEntry entry = new LeaderboardEntry(scoreMap["name"].ToString(), scoreMap["score"].ToString());
                          scoreMap["entry"] = entry;
                          //Add to list of LeaderboardEntries (accessible between all screens)
                          //Leaders.Add(entry);
                          //Download user profile picture stored by each users' Email value from Firebase Storage
                          scoreMap = GetProfilePic(scoreMap);
                          Leaderboard.Add(scoreMap); 
                      }
                  }
                  DisplayLoaderboard();
              }
          }
      });
    }

    private Dictionary<string, object> GetProfilePic(Dictionary<string, object> scoreMap)
    {
        Texture2D tex = new Texture2D(130, 130, TextureFormat.PVRTC_RGBA4, false);
        //Download to byte array
        Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
        Firebase.Storage.StorageReference storage_ref = storage.GetReferenceFromUrl("gs://surfman-389c5.appspot.com");
        var profilePicReference = storage_ref.Child("images/" + scoreMap["email"]);
        const long maxAllowedSize = 1 * 1024 * 1024;
        profilePicReference.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task) =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log(task.Exception.ToString());
                // Uh-oh, an error occurred!
            }
            else
            {
                byte[] fileContents = task.Result;
                Debug.Log("Finished downloading!");
                tex.LoadImage(fileContents);
                Sprite profilePic = Sprite.Create(tex, new Rect(0, 0, 130, 130), new Vector2());
                scoreMap["sprite"] = profilePic;
                LeaderboardEntry e = (LeaderboardEntry)scoreMap["entry"];
                e.AddSprite(profilePic);

            }
        });
        return scoreMap;
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

    public void DisplayLoaderboard()
    {
        float entryPos = 0.0f;
        Leaderboard.Reverse();
        foreach (Dictionary<string, object> scoreEntry in Leaderboard)
        {
            //Sprite sprite = (Sprite)scoreEntry["sprite"];
            string name = scoreEntry["name"].ToString();
            string score = scoreEntry["score"].ToString();
            LeaderboardEntry e = (LeaderboardEntry)scoreEntry["entry"];
            GameObject entry = e.MakeGameObject();
            RectTransform rf = entry.AddComponent<RectTransform>();
            rf.SetParent(LeaderboardHolder.transform);
            rf.localPosition = new Vector3(0.0f, entryPos, 0.0f);
            rf.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            entryPos -= 200.0f;
        }
    }
}
