using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimplecameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        objectToFollow = GameObject.FindWithTag("Player").GetComponent<Transform>();
		are_you.gameObject.SetActive(false);
		Yes.gameObject.SetActive(false);
		No.gameObject.SetActive(false);
        //this.transform.parent = objectToFollow;
        //transform.position = new Vector3(0,0,0);
		
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAtTarget();
		MoveToTarget();
		Go_To_MainPage();
    }

    public void MoveToTarget()
	{
		Vector3 _targetPos = objectToFollow.position + 
							 objectToFollow.forward * offset.z + 
							 objectToFollow.right * offset.x + 
							 objectToFollow.up * offset.y;
		transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);
	}

    public void LookAtTarget()
	{
		Vector3 _lookDirection = objectToFollow.position - transform.position;
		Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);
	}

	public void Go_To_MainPage()
	{
		if(Input.GetKeyDown(KeyCode.G))
		{
			are_you.gameObject.SetActive(true);
			Yes.gameObject.SetActive(true);
			No.gameObject.SetActive(true);
		}
	}

	public void yes_go()
	{
		if (SelectTrack.isTutorial)
		{
			SceneManager.LoadScene(0);
			SelectTrack.isTutorial = false;
		}
		else
		{
			SceneManager.LoadScene(4);
		}
	}
	public void no_go()
	{
		are_you.gameObject.SetActive(false);
		Yes.gameObject.SetActive(false);
		No.gameObject.SetActive(false);
	}


    private Transform objectToFollow;
	public Vector3 offset;
	public float followSpeed = 10;
	public float lookSpeed = 10;

	public Text are_you;
	public Button Yes;
	public Button No;
}
