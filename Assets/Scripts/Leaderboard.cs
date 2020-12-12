using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Leaderboard : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GetRequest(string.Format("localhost:3000/api/get_track?track={0}&place={1}", SelectTrack.track, 1), 1));
        StartCoroutine(GetRequest(string.Format("localhost:3000/api/get_track?track={0}&place={1}", SelectTrack.track, 2), 2));
        StartCoroutine(GetRequest(string.Format("localhost:3000/api/get_track?track={0}&place={1}", SelectTrack.track, 3), 3));
        StartCoroutine(GetRequest(string.Format("localhost:3000/api/get_track_player?track={0}&name={1}", SelectTrack.track, Login.loginName), 4));
    }

    IEnumerator GetRequest(string uri, int place)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            bool result;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
                result = false;
            }

            Debug.Log(pages[page] + ": Res: " + webRequest.downloadHandler.text);
            result = !(webRequest.downloadHandler.text == "Internal Server Error");

            if (result)
            {
                if (place == 4)
                {
                    GameObject.Find("Player").GetComponent<Text>().text = "당신 " + webRequest.downloadHandler.text + "초";
                }
                else
                {
                    GameObject.Find("Place" + place).GetComponent<Text>().text = place + "위 " + webRequest.downloadHandler.text + "초";
                }
            }
            else
            {
                if (place == 4)
                {
                    GameObject.Find("Player").GetComponent<Text>().text = "당신\n기록 없음";
                }
                else
                {
                    GameObject.Find("Place" + place).GetComponent<Text>().text = place + "위\n기록 없음";
                }
            }
        }
    }
}
