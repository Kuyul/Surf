using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FBScript : MonoBehaviour {

    public GameObject DialogLoggedIn;
    public GameObject DialogLoggedOut;
    public LoginHandler LoginHandler;

    private void Awake()
    {
        FacebookManager.Instance.InitFB();
    }

    public void FBlogin()
    {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");
        permissions.Add("email");

        FB.LogInWithReadPermissions(permissions, AuthCallBack); //we need to declare permissions
    }

    void AuthCallBack(IResult result)
    {
        //First we want to deal with errors
        if (result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                FacebookManager.Instance.IsLoggedIn = true;
                FacebookManager.Instance.GetProfile();
                Debug.Log("FB is logged in");
                //Sign into Firebase
            }
            else
            {
                Debug.Log("FB is not logged in");
            }
            DealWithFBMenus(FB.IsLoggedIn);
        }
    }
    
    public void DealWithFBMenus(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            LoginHandler.SigninWithCredentialAsync();
            if (LoginHandler.getEmail() == "")
            {
                StartCoroutine("WaitForFirebaseAuth");
            }
        }
        else
        {
            DialogLoggedIn.SetActive(false);
            DialogLoggedOut.SetActive(true);
        }
    }
    IEnumerator WaitForFirebaseAuth()
    {
        while(LoginHandler.getEmail() == "")
        {
            yield return null;
        }

        DealWithFBMenus(FacebookManager.Instance.IsLoggedIn);
    }

    IEnumerator WaitForProfileName()
    {
        while(FacebookManager.Instance.ProfileName == null)
        {
            yield return null;
        }

        DealWithFBMenus(FacebookManager.Instance.IsLoggedIn);
    }

    IEnumerator WaitForProfilePic()
    {
        while (FacebookManager.Instance.ProfilePic == null)
        {
            yield return null;
        }

        DealWithFBMenus(FacebookManager.Instance.IsLoggedIn);
    }
}
