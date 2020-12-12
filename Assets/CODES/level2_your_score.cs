using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class level2_your_score : MonoBehaviour
{
    public Text Finish;
    bool check = false;

    // Start is called before the first frame update
    void Start()
    {
        Finish.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
        if (car_for_racing.points >= 4 && other.gameObject.CompareTag("Player"))
        {
            Finish.gameObject.SetActive(true);
            Finish.text = "Your Score is " + car_for_racing.timer;

            if (!check)
            {
                StartCoroutine(GetRequest(string.Format("localhost:3000/api/delete_track?track={0}&name={1}&record={2}", SelectTrack.track ,Login.loginName, car_for_racing.timer)));
                check = true;
            }
        }
    }
    void go_to_main()
    {
        SceneManager.LoadScene(4);
        check = false;
    }

    IEnumerator GetRequest(string uri)
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

            if (uri.Contains("delete"))
            {
                switch (SelectTrack.track)
                {
                    case 1:
                        StartCoroutine(GetRequest(string.Format("localhost:3000/api/add_track1?name={0}&record={1}", Login.loginName, car_for_racing.timer)));
                        break;

                    case 2:
                        StartCoroutine(GetRequest(string.Format("localhost:3000/api/add_track2?name={0}&record={1}", Login.loginName, car_for_racing.timer)));
                        break;
                }
            }
        }
    }
}
