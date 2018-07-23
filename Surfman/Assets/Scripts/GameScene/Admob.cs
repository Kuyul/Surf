using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class Admob : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitial;
    //Only show interstitial ad on death (5x)
    private int deathCount = 0;

    //Singleton Admob
    private static Admob _instance;
    public static Admob Instance
    {
        get
        {
            //create a new gameobject in the scene
            if (_instance == null)
            {
                GameObject Admob = new GameObject("AdManager");
                Admob.AddComponent<Admob>();
            }

            return _instance;
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _instance = this;
    }

    // Use this for initialization
    private void Start()
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
        this.RequestInterstitial();
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

    public void RequestInterstitial()
    {
        #if UNITY_ANDROID
                string adUnitId = "ca-app-pub-3529204849708317/1997674232";
        #elif UNITY_IPHONE
                string adUnitId = "ca-app-pub-3940256099942544/4411468910";
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

    public void ShowInterstitial()
    {
        //Show interstitial Ad on death (5x)
        if (deathCount < 5)
        {
            deathCount++;
        }
        else
        {
            deathCount = 0;
            if (interstitial.IsLoaded())
            {
                interstitial.Show();
            }
        }
    }
}
