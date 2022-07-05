/*
 GSheetStore.cs
 Store data to Google Sheet
 Author: Akihiko SHIRAI (Twitter@o_ob, github@kaitas)
 Created at: 2022/7/5
 */

using System;
using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;

public class GSheetStore : MonoBehaviour
{
    //    [SerializeField] string sheetID;
    //    [SerializeField] string sheetName;
    public string API; // Deploy ID
    public string StoringSheetName;
    private string _text;

    void Start()
    {
        Debug.Log("GSheet Data Store demo. Push [s] to save...");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("s"))
        {
            // most simple code to store data via get request
            var uuid = GetUUID();
            string write_url = "https://script.google.com/macros/s/" + API + "/exec?action=c&s=" + StoringSheetName + "&time=20220705&id=test&lat=34.37281&lng=135.3018&UnityText=testtest";
            Debug.Log(write_url);
            UnityWebRequest request = UnityWebRequest.Get(write_url);
            UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = request.SendWebRequest(); // yeild sitekudasai
            Debug.Log("Done, please check your store on the sheet.");
        }

    }
    //To have unique cell, we used UUID.
    string GetUUID()
    {
        var guid = System.Guid.NewGuid();
        return guid.ToString();
    }

    // For GPS geo coding
    IEnumerator Write(string UnityText)
    {
        //IDをランダムで生成（被る可能性あり）
        var uuid = GetUUID();
        if (!Input.location.isEnabledByUser)
            yield break;
        Input.location.Start();
        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        // Service didn't initialize in 20 seconds
        if (maxWait <= 0)
        {
            Debug.Log("Timed out");
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        else
        {
            var localDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(Input.location.lastData.timestamp).ToLocalTime();
            string write_url = "https://script.google.com/macros/s/"+API+"/exec?action=c&s=" + StoringSheetName + "&time=" + localDate + "&id=test&lat=" + Input.location.lastData.latitude + "&lng=" + Input.location.lastData.longitude+"&UnityText="+UnityText;
            Debug.Log(write_url);
            UnityWebRequest request = UnityWebRequest.Get(write_url);
            yield return request.SendWebRequest();
        }
        Input.location.Stop();
    }
}

