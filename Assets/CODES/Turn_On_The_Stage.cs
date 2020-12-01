using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Turn_On_The_Stage : MonoBehaviour
{
    bool bTurnLeft = false;
    bool bTurnRight = false;
    private Quaternion turn = Quaternion.identity;
    public static int charactorNum = 0;
    int value = 0;
    // Start is called before the first frame update
    void Start()
    {
         turn.eulerAngles = new Vector3(0, value, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(bTurnLeft)
        {
            Debug.Log("Left");
            charactorNum++;
            if (charactorNum == 5)
                charactorNum = 0;
            value -= 90;
            // 각도를 90도 뺍니다.
            bTurnLeft = false;
            // 부울 변수를 취소합니다.
        }
        if(bTurnRight)
        {
            Debug.Log("Right");
            charactorNum--;
            if (charactorNum == -1)
                charactorNum = 4;
            value += 90;
            // 각도를 90도 더합니다.
            bTurnRight = false;
            // 부울 변수를 취소합니다.
        }
        turn.eulerAngles = new Vector3(0, value, 0);
        // 각도를 잽니다.
        transform.rotation = Quaternion.Slerp(transform.rotation, turn, Time.deltaTime * 5.0f);
        // 돌립니다.
	}

    public void turnLeft()
    {
        bTurnLeft = true;
        bTurnRight = false;
        // 다른 버튼을 누를때의 컨트롤
    }

    public void turnRight()
    {
        bTurnRight = true;
        bTurnLeft = false;
        // 다른 버튼을 누를때의 컨트롤
    }

    public void turnStage()
    {
        // 스테이지 전환을 위한 함수
        SceneManager.LoadScene("OnTheStage");
    }
}


