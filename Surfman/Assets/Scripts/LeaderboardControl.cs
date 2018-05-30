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
    protected Firebase.Auth.FirebaseAuth auth;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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
        GetLeaderBoard();
    }

    public void GetLeaderBoard()
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
                          scoreMap = GetProfilePic(scoreMap);
                          Leaderboard.Add(scoreMap); 
                          Debug.Log(childSnapshot.Child("name").Value + " " + childSnapshot.Child("score").Value);
                      }
                  }
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
                scoreMap["profilePic"] = profilePic;
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
}
