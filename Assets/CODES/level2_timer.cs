﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level2_timer : MonoBehaviour
{
    public Text Timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer.text = "Time : "+car_for_racing.timer;
    }
}
