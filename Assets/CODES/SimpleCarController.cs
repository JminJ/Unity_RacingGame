using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SimpleCarController : MonoBehaviour {

	bool check = false;

	void Start()
	{
		rigid = GetComponent<Rigidbody>();
		PlayerScore.gameObject.SetActive(false);
	}
	public void GetInput()
	{
		m_horizontalInput = Input.GetAxis("Horizontal");
		m_verticalInput = Input.GetAxis("Vertical");
	}

	private void Steer()
	{
		m_steeringAngle = maxSteerAngle * m_horizontalInput;
		frontDriverW.steerAngle = m_steeringAngle;
		frontPassengerW.steerAngle = m_steeringAngle;
	}

	private void Accelerate()
	{
		frontDriverW.motorTorque = m_verticalInput * motorForce + 25000*m_verticalInput;
		frontPassengerW.motorTorque = m_verticalInput * motorForce + 25000*m_verticalInput;
	}

	private void UpdateWheelPoses()
	{
		UpdateWheelPose(frontDriverW, frontDriverT);
		UpdateWheelPose(frontPassengerW, frontPassengerT);
		UpdateWheelPose(rearDriverW, rearDriverT);
		UpdateWheelPose(rearPassengerW, rearPassengerT);
	}

	private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
	{
		Vector3 _pos = _transform.position;
		Quaternion _quat = _transform.rotation;
        //Debug.Log(_pos);
		_collider.GetWorldPose(out _pos, out _quat);
        
		_transform.position = _pos;
		_transform.rotation = _quat;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("goods"))
		{
			other.gameObject.SetActive(false);
			points++;
		}
	}

	private void FixedUpdate()
	{
		CurrentPoint.text = "Point : "+points;
		retimer();
		GetInput();
		Steer();
		Accelerate();
		UpdateWheelPoses();
		timer = timer + Time.deltaTime;
		Jump();
		checkPonint();
	}
	void Jump()
	{		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(timer > waitingTime)
			{
				rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
			}	
			timer = 0;
		}
	}

	void retimer()
	{
		if(LimitTime >= 0)
		{
			LimitTime -= Time.deltaTime;
			text_Timer.text = "남은 시간 : "+Mathf.Round(LimitTime);
		}
		else
		{
			text_Timer.gameObject.SetActive(false);
			PlayerScore.gameObject.SetActive(true);
			PlayerScore.text = "Your Point is... "+points;
			for(int i = 0; i < 9; i++){
				Historics[i].gameObject.SetActive(false);
			}

			if (!check)
			{
				StartCoroutine(GetRequest(string.Format("localhost:3000/api/delete_track?track={0}&name={1}&record={2}", 3, Login.loginName, points)));
				check = true;
			}
		}
		
	}

	void checkPonint(){
        
        if(transform.position.y < -66)
		{
        	 transform.position = new Vector3(-521f, -5f, -468f);
        }   
    }

	IEnumerator GetRequest(string uri)
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

			if (uri.Contains("delete"))
            {
				StartCoroutine(GetRequest(string.Format("localhost:3000/api/add_track3?name={0}&record={1}", Login.loginName, points)));
			}
		}
	}

	private float m_horizontalInput;
	private float m_verticalInput;
	private float m_steeringAngle;

	GameObject player;

	//private bool pointcheck = false;
	private int points = 0;
	public int JumpPower;
	private float timer = 1;
	int waitingTime = 1;
	public float LimitTime;
	public Text text_Timer;
	public Text CurrentPoint;
	public Text PlayerScore;
	private Rigidbody rigid;
	public WheelCollider frontDriverW, frontPassengerW;
	public WheelCollider rearDriverW, rearPassengerW;
	public Transform frontDriverT, frontPassengerT;
	public Transform rearDriverT, rearPassengerT;
	public float maxSteerAngle = 30;
	public float motorForce = 50;
	public GameObject []Historics = new GameObject[9];
}