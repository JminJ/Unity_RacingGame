using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public static string loginName; 
    public int sceneIndex;

    string id;
    string password;

    Text idError;
    Text passwordError;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Confirm()
    {
        id = GameObject.Find("IdField").GetComponent<InputField>().text;
        password = GameObject.Find("PasswordField").GetComponent<InputField>().text;

        idError = GameObject.Find("IdError").GetComponent<Text>();
        passwordError = GameObject.Find("PasswordError").GetComponent<Text>();

        if (password.Length == 0)
        {
            passwordError.text = "비밀번호 미기재";
        }
        else
        {
            passwordError.text = "";
        }

        if (id.Length == 0)
        {
            idError.text = "아이디 미기재";
        }
        else
        {
            idError.text = "";
            StartCoroutine(GetRequest("localhost:3000/api/get_user?id=" + id, idError, "존재하지 않는 아이디"));
        }
    }

    IEnumerator GetRequest(string uri, Text error, string problem)
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

            if (!result)
            {
                error.text = problem;
            }

            if ((idError.text + passwordError.text).Length == 0)
            {
                if (uri.Contains("id") && uri.Contains("password"))
                {
                    loginName = webRequest.downloadHandler.text;
                    SceneManager.LoadScene(sceneIndex);
                }
                else
                {
                    StartCoroutine(GetRequest(string.Format("localhost:3000/api/get_user?id={0}&password={1}", id, password), passwordError, "비밀번호 불일치"));
                }
            }
        }
    }
}