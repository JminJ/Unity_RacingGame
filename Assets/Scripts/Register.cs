using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{
    public int sceneIndex;

    public void Confirm()
    {
        string name = GameObject.Find("NameField").GetComponent<InputField>().text;
        string id = GameObject.Find("IdField").GetComponent<InputField>().text;
        string password = GameObject.Find("PasswordField").GetComponent<InputField>().text;
        string passwordCheck = GameObject.Find("PasswordCheckField").GetComponent<InputField>().text;

        Text nameError = GameObject.Find("NameError").GetComponent<Text>();
        Text idError = GameObject.Find("IdError").GetComponent<Text>();
        Text passwordError = GameObject.Find("PasswordError").GetComponent<Text>();
        Text passwordCheckError = GameObject.Find("PasswordCheckError").GetComponent<Text>();
        Text submitError = GameObject.Find("PasswordCheckError").GetComponent<Text>();

        if (name.Length == 0)
        {
            nameError.text = "닉네임 미기재";
        }
        else
        {
            nameError.text = "";
            StartCoroutine(GetRequest("localhost:3000/api/get_user?name=" + name, nameError, "닉네임 중복"));
        }

        if (id.Length == 0)
        {
            idError.text = "아이디 미기재";
        }
        else
        {
            idError.text = "";
            StartCoroutine(GetRequest("localhost:3000/api/get_user?id=" + id, idError, "아이디 중복"));
        }

        if (password.Length == 0)
        {
            passwordError.text = "비밀번호 미기재";
        }
        else
        {
            passwordError.text = "";
        }

        if (passwordCheck.Length == 0)
        {
            passwordCheckError.text = "비밀번호 확인 미기재";
        }
        else
        {
            if (password != passwordCheck)
            {
                passwordCheckError.text = "비밀번호 불일치";
            }
            else
            {
                passwordCheckError.text = "";
                StartCoroutine(GetRequest(string.Format("localhost:3000/api/add_user?name={0}&id={1}&password={2}", name, id, password), submitError, "회원가입 실패"));
            }
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
            result = webRequest.downloadHandler.text == "OK";

            if (result)
            {
                if (uri.Contains("add_user"))
                {
                    SceneManager.LoadScene(sceneIndex);
                }

                error.text = problem;
            }
        }
    }
}
