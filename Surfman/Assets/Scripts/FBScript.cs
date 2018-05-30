﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FBScript : MonoBehaviour {

    public GameObject DialogLoggedIn;
    public GameObject DialogLoggedOut;
    public GameObject DialogUsername;
    public GameObject DialogProfilepic;
    public LoginHandler LoginHandler;

    private void Awake()
    {
        FacebookManager.Instance.InitFB();
        DealWithFBMenus(FB.IsLoggedIn);
    }

    public void FBlogin()
    {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");
        permissions.Add("email");

        FB.LogInWithReadPermissions(permissions, AuthCallBack); //we need to declare permissions
    }

    public void FBlogout()
    {
        FB.LogOut();

        if (FB.IsLoggedIn)
        {
            Debug.Log("FB is logged in");
        }
        else
        {
            Debug.Log("FB is not logged in");
        }
        DealWithFBMenus(FB.IsLoggedIn);
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
                LoginHandler.SigninWithCredentialAsync();
            }
            else
            {
                Debug.Log("FB is not logged in");
            }
            DealWithFBMenus(FB.IsLoggedIn);
        }
    }

    void DealWithFBMenus(bool isLoggedIn)
    {
        if (isLoggedIn)
        {


            if (FacebookManager.Instance.ProfileName != null)
            {
                Text userName = DialogUsername.GetComponent<Text>();
                userName.text = "Hi, " + FacebookManager.Instance.ProfileName;
            }
            else
            {
                StartCoroutine("WaitForProfileName");
            }
            //Wait until Profile Pic is retrieved from Facebook
            if (FacebookManager.Instance.ProfilePic != null)
            {
                Image profilePic = DialogProfilepic.GetComponent<Image>();
                profilePic.sprite = FacebookManager.Instance.ProfilePic;
            }
            else
            {
                StartCoroutine("WaitForProfilePic");
            }
            if (LoginHandler.getEmail() == "")
            {
                StartCoroutine("WaitForFirebaseAuth");
            }
            //We don't want to reference the gameobjects that are specific to one scene in FacebookManager singleton
            //DialogLoggedIn.SetActive(true);
            //DialogLoggedOut.SetActive(false);
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
