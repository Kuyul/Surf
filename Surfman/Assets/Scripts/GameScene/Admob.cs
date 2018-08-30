using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class Admob : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardBasedVideo;
    //Only show interstitial ad on death (5x)
    private int deathCount = 0;

    //Singleton Admob
    public static Admob Instance;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    private void Start()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-3529204849708317~6637326357";
#elif UNITY_IPHONE
                                    string appId = "ca-app-pub-3529204849708317~8162431790";
#else
                                    string appId = "unexpected_platform";
#endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        this.RequestBanner();
        //Initialise Interstitial Ads
        this.RequestInterstitial();
        interstitial.OnAdClosed += HandleRewardInterstitialClosed;
        //Initialise Rewarded video
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

        this.RequestRewardBasedVideo();
    }

    // Update is called once per frame
    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3529204849708317/8272288608";
#elif UNITY_IPHONE
                                    string adUnitId = "ca-app-pub-3529204849708317/5448638549";
#else
                                    string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        //bannerView.LoadAd(request);
    }

    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3529204849708317/1997674232";
#elif UNITY_IPHONE
                        string adUnitId = "ca-app-pub-3529204849708317/1317821843";
#else
                        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    private void RequestRewardBasedVideo()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
                    string adUnitId = "ca-app-pub-3529204849708317/6003008983";
#else
                    string adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }

    public void ShowInterstitial()
    {
        //Show interstitial Ad on death (5x)
        if (deathCount < 5)
        {
            deathCount++;
        }
        else
        {
            if (interstitial.IsLoaded())
            {
                deathCount = 0;
                Debug.Log("Ad Is Loaded");
                interstitial.Show();
            }
            else
            {
                Debug.Log("Ad is not Loaded");
            }
        }
    }

    //Rewarded Ads Handler
    public void HandleRewardInterstitialClosed(object sender, EventArgs args)
    {
        RequestInterstitial();
    }

    public void ShowRewardBasedVideo()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            Debug.Log("Reward based video is loaded");
            rewardBasedVideo.Show();
        }
        else
        {
            Debug.Log("Reward Based video is not loaded");
            RequestRewardBasedVideo();
        }
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardBasedVideoOpened event received");
        this.RequestRewardBasedVideo();
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardBasedVideoClosed event received");
        this.RequestRewardBasedVideo();
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {

    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }
}

