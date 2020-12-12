using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loggedin : MonoBehaviour
{
    void Start()
    {
        Debug.Log(Login.loginName);
        this.gameObject.GetComponent<Text>().text = Login.loginName + "로 플레이 중";
    }
}
