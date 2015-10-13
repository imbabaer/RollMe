using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class sendQuitAnalytics : MonoBehaviour
{
    bool sent = false;
    // Use this for initialization
    void Start()
    {
        string playtime = Math.Round(Time.time, 2).ToString();

        var ret = Analytics.CustomEvent("PlayTime2", new Dictionary<string, object> { { "overall playtime", playtime }, { "exittime", DateTime.Now.ToString() }, { "ad1clix", Ads.mAd1Count }, { "ad2clix", Ads.mAd2Count } });
        Debug.Log("AnalyticsReturn: PlayTime2 " + ret.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (!sent)
        {
            Debug.Log(Analytics.CustomEvent("Neeeeeeeeew", new Dictionary<string, object> { { "bla1", 1 } }).ToString());
            sent = true;
        }
    }
}
