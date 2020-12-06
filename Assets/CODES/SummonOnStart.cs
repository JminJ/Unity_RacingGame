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
        print(charactors);
        respawn = GameObject.FindGameObjectWithTag("respawn");
        print(respawn);
        // 위치를 위한 오브젝트
        for(int i=0; i<5; i++)
        {
            if (charactors == i)
            {
                print(Cars[i]);
                Instantiate(Cars[i],respawn.transform.position , Quaternion.Euler(0, -90.0f, 0));
            } 
        }      
    }

	// Update is called once per frame
	void Update () {
		
	}
}
