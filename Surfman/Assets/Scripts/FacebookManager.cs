using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FacebookManager : MonoBehaviour {

    private static FacebookManager _instance;

    public static FacebookManager Instance
    {
        get
        {
            //create a new gameobject in the scene
            if (_instance == null)
            {
                GameObject fbm = new GameObject("FBManager");
                fbm.AddComponent<FacebookManager>();
            }

            return _instance;
        }
    }

    public bool IsLoggedIn { get; set; }
    public string ProfileName { get; set; }
    public string Email { get; set; }
    public Sprite ProfilePic { get; set; }
    public Texture2D ProfileTexture{get; set;}

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _instance = this;

        IsLoggedIn = true;
    }

    public void InitFB()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(SetInit, OnHideUnity);
        }
        else
        {
            IsLoggedIn = FB.IsLoggedIn;
        }
    }

    void SetInit()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("FB is logged in");
            //For when user is already logged in
            GetProfile();
        }
        else
        {
            Debug.Log("FB is not logged in");
        }
        IsLoggedIn = FB.IsLoggedIn;
    }

    void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0; //pause the game if the game isn't shown
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void GetProfile()
    {
        FB.API("/me?fields=first_name,email", HttpMethod.GET, DisplayUsername); //DisplayUsername callback
        FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
    }

    void DisplayUsername(IResult result)
    {
        if (result.Error == null)
        {
            ProfileName = "" + result.ResultDictionary["first_name"];
            Email = "" + result.ResultDictionary["email"];
            Debug.Log(ProfileName);
        }
        else
        {
            Debug.Log(result.Error);
        }
    }

    //why graph result? :/
    void DisplayProfilePic(IGraphResult result)
    {
        if (result.Texture != null)
        {
            ProfilePic = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
            ProfileTexture = result.Texture;
        }
    }

    public string getAccessToken()
    {
        return AccessToken.CurrentAccessToken.TokenString;
    }
}
