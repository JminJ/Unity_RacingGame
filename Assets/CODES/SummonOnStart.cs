using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonOnStart : MonoBehaviour
{
    int charactors;
    public GameObject []Cars = new GameObject[5];
    GameObject respawn = null;

	// Use this for initialization
	void Start () {
        startStage();
	}
	
    void startStage()
    {
        charactors = Turn_On_The_Stage.charactorNum;
        respawn = GameObject.FindGameObjectWithTag("respawn");
        // 위치를 위한 오브젝트
        for(int i=0; i<5; i++)
            if (charactors == i) Instantiate(Cars[i],respawn.transform.position , Quaternion.identity);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
