using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using System.Collections.Generic;
using UnityEngine.Analytics;
using System;

public class Ads : MonoBehaviour
{
    public GUIText mGuiText;
    public GUIText mGuiText2;
    public GUIText mGuiText3;
    public static int mAd1Count = 0;
    public static int mAd2Count = 0;
    public int mPoints = 0;

    // Use this for initialization
    void Start()
    {
        mPoints = PlayerPrefs.GetInt("diamonds");

    }

    // Update is called once per frame
    void Update()
    {
        if(null!=mGuiText3)
        {
            mGuiText3.text = mPoints.ToString();
        }

    }
    public float showSplashTimeout = 5.0F;
    private bool allowQuitting = false;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void OnApplicationQuit()
    {
        if (Application.loadedLevelName.ToLower() != "finalsplash")
            StartCoroutine("DelayedQuit");

        if (!allowQuitting)
            Application.CancelQuit();

    }
    IEnumerator DelayedQuit()
    {
        Application.LoadLevel("finalsplash");
        //string playtime = Math.Round(Time.time, 2).ToString();
        //var ret = Analytics.CustomEvent("PlayTime2", new Dictionary<string, object> { { "overall playtime", playtime }, { "exittime", DateTime.Now.ToString() }, { "ad1clix", mAd1Count }, { "ad2clix", mAd2Count } });
        //Debug.Log("AnalyticsReturn: PlayTime2 " + ret.ToString());

        yield return new WaitForSeconds(showSplashTimeout);
        allowQuitting = true;
        Application.Quit();
    }


    public void Exitt()
    {
        StartCoroutine("DelayedQuit");
        if (!allowQuitting)
            Application.CancelQuit();
    }

    IEnumerator MyMethod()
    {
        Debug.Log("Before Waiting 2 seconds");
        string currentTime = Math.Round(Time.time, 2).ToString();

        var ret = Analytics.CustomEvent("PlayTime", new Dictionary<string, object> { { "overall playtime", currentTime }, { "exittime", DateTime.Now.ToString() }, { "ad1clix", mAd1Count }, { "ad2clix", mAd2Count } });
        Debug.Log("AnalyticsReturn: PlayTime" + ret.ToString());
        yield return new WaitForSeconds(1);
        Debug.Log("After Waiting 2 Seconds");
    }

    public void ShowAd()
    {
        mAd1Count += 1;
        string currentTime = Math.Round(Time.time, 2).ToString();
        Analytics.CustomEvent("Ad1Click", new Dictionary<string, object> { { "time", currentTime } });
        Analytics.Transaction("12345abcde", 0.99m, "USD", null, null);
        Gender gender = Gender.Female;
        Analytics.SetUserGender(gender);
        int birthYear = 2014;
        Analytics.SetUserBirthYear(birthYear);


        if (mGuiText != null)
        { mGuiText.text = currentTime; }
        if (Advertisement.IsReady("defaultZone"))
        {
            Advertisement.Show("defaultZone");
            if (mGuiText2 != null)
            {
                mGuiText2.text = "defaultZone";
            }
        }


    }
    public void ShowAd2()
    {
        mAd2Count += 1;
        string currentTime = Math.Round(Time.time, 2).ToString();
        Analytics.CustomEvent("Ad2Click", new Dictionary<string, object> { { "time", currentTime } });

        if (mGuiText != null)
        { mGuiText.text = currentTime; }
        if (Advertisement.IsReady("rewardedVideoZone"))
        {

            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideoZone", options);
            if (mGuiText2 != null)
            {
                mGuiText2.text = "rewardedVideoZone";
            }

        }

    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                mPoints += 10;
                PlayerPrefs.SetInt("diamonds", mPoints);
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}


