using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour {

	void Start()
	{
		rigid = GetComponent<Rigidbody>();
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
		frontDriverW.motorTorque = m_verticalInput * motorForce + 15000*m_verticalInput;
		frontPassengerW.motorTorque = m_verticalInput * motorForce + 15000*m_verticalInput;
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

	private void FixedUpdate()
	{
		GetInput();
		Steer();
		Accelerate();
		UpdateWheelPoses();
		Jump();
		timer = timer + Time.deltaTime;
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

	private float m_horizontalInput;
	private float m_verticalInput;
	private float m_steeringAngle;


	public int JumpPower;
	private float timer = 1;
	int waitingTime = 1;
	private Rigidbody rigid;
	public WheelCollider frontDriverW, frontPassengerW;
	public WheelCollider rearDriverW, rearPassengerW;
	public Transform frontDriverT, frontPassengerT;
	public Transform rearDriverT, rearPassengerT;
	public float maxSteerAngle = 30;
	public float motorForce = 50;
}