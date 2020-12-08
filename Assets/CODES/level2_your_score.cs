using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class level2_your_score : MonoBehaviour
{
    public Text Finish;

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
        if(car_for_racing.points >= 4 && other.gameObject.CompareTag("Player"))
		{
			Finish.gameObject.SetActive(true);
			Finish.text = "Your Score is "+car_for_racing.timer;
		}
    }
    /* void go_to_main()
    {
        SceneManager.LoadScene("CarSelect");
    } */
}
