﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class Admob : MonoBehaviour
{
    private BannerView bannerView;
    // Use this for initialization
    void Start()
    {
        #if UNITY_ANDROID
                string appId = "ca-app-pub-3529204849708317~6637326357";
        #elif UNITY_IPHONE
                            string appId = "ca-app-pub-3940256099942544~1458002511";
        #else
                            string appId = "unexpected_platform";
        #endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        this.RequestBanner();
    }

    // Update is called once per frame
    private void RequestBanner()
    {
        #if UNITY_ANDROID
                string adUnitId = "ca-app-pub-3529204849708317/8272288608";
        #elif UNITY_IPHONE
                            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
                            string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }
}
