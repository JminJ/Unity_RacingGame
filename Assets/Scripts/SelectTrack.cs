using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectTrack : MonoBehaviour
{
    public static int track = 0;
    public static bool isTutorial = false;

    public GameObject track1;
    public GameObject track2;
    public GameObject track3;

    bool check = false;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (track == 0)
        {
            track = 1;
        }
    }

    void Update()
    {
        if (!check)
        {
            switch (track)
            {
                case 1:
                    track1.SetActive(true);
                    track2.SetActive(false);
                    track3.SetActive(false);
                    break;

                case 2:
                    track1.SetActive(false);
                    track2.SetActive(true);
                    track3.SetActive(false);
                    break;

                case 3:
                    track1.SetActive(false);
                    track2.SetActive(false);
                    track3.SetActive(true);
                    break;

                default:
                    Debug.Log("Error");
                    break;
            }

            check = true;
        }
    }

    public void PreviousTrack()
    {
        if (track == 1)
        {
            track = 3;
        }
        else { track--; }
        check = false;
    }

    public void NextTrack()
    {
        if (track == 3)
        {
            track = 1;
        }
        else { track++; }
        check = false;
    }

    public void Play()
    {
        SceneManager.LoadScene(5);
    }
}
