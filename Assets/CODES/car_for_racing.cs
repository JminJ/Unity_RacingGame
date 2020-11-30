using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class car_for_racing : MonoBehaviour
{
   void Start()
	{
		Finish.gameObject.SetActive(false);
		rigid = GetComponent<Rigidbody>();
        colliderRWL = rearDriverW.GetComponent<WheelCollider>();
        colliderRWR = rearPassengerW.GetComponent<WheelCollider>();

        fFrictionRWL = colliderRWL.forwardFriction;
        sFrictionRWL = colliderRWL.sidewaysFriction;
        fFrictionRWR = colliderRWR.forwardFriction;
        sFrictionRWR = colliderRWR.sidewaysFriction;
	}
	public void GetInput()
	{
		m_horizontalInput = Input.GetAxis("Horizontal");
		m_verticalInput = Input.GetAxis("Vertical");
	}

	private void Steer()
	{
		m_steeringAngle = maxSteerAngle * m_horizontalInput/2;
		frontDriverW.steerAngle = m_steeringAngle;
		frontPassengerW.steerAngle = m_steeringAngle;
	}

	private void Accelerate()
	{
		frontDriverW.motorTorque = 1500*m_verticalInput;
		frontPassengerW.motorTorque = 1500*m_verticalInput;
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

    private void Drift()
    {
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            fFrictionRWL.stiffness = handBreakSlipRate;
            colliderRWL.forwardFriction = fFrictionRWL;

            sFrictionRWL.stiffness = handBreakSlipRate;
            colliderRWL.sidewaysFriction = sFrictionRWL;

            fFrictionRWR.stiffness = handBreakSlipRate;
            colliderRWR.forwardFriction = fFrictionRWR;

            sFrictionRWR.stiffness = handBreakSlipRate;
            colliderRWR.sidewaysFriction = sFrictionRWR;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            fFrictionRWL.stiffness = slipRate;
            colliderRWL.forwardFriction = fFrictionRWL;

            sFrictionRWL.stiffness = slipRate;
            colliderRWL.sidewaysFriction = sFrictionRWL;

            fFrictionRWR.stiffness = slipRate;
            colliderRWR.forwardFriction = fFrictionRWR;


            sFrictionRWR.stiffness = slipRate;
            colliderRWR.sidewaysFriction = sFrictionRWR;
        }
    }

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("checkpoint"))
		{
			other.gameObject.SetActive(false);
			points ++;
		}
		if(points >= 4 && other.gameObject.CompareTag("lastPoint"))
		{
			lasttime = timer;
			Finish.gameObject.SetActive(true);
			Finish.text = "Your Score : "+lasttime;
		}
	}

	void retimer()
	{
		timer += Time.deltaTime;
		point.text = "Time : "+timer;
	}

	private void FixedUpdate()
	{
		retimer();
		GetInput();
		Steer();
		Accelerate();
		UpdateWheelPoses();
        Drift();	
	}

	private float m_horizontalInput;
	private float m_verticalInput;
	private float m_steeringAngle;

	private float lasttime = 0;
	private float timer = 0;
	private int points = 0;
	private Rigidbody rigid;
	public WheelCollider frontDriverW, frontPassengerW;
	public WheelCollider rearDriverW, rearPassengerW;
	public Transform frontDriverT, frontPassengerT;
	public Transform rearDriverT, rearPassengerT;
	public float maxSteerAngle = 30;
	public float motorForce = 50;
	public Text point;
	public Text Finish;
    WheelCollider colliderRWL;
    WheelCollider colliderRWR;
    WheelFrictionCurve fFrictionRWL;
    WheelFrictionCurve sFrictionRWL;
    WheelFrictionCurve fFrictionRWR;
    WheelFrictionCurve sFrictionRWR;
    float slipRate = 2.0f;
    float handBreakSlipRate = 0.4f;
}
