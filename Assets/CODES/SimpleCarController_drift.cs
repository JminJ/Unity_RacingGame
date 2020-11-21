using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController_drift : MonoBehaviour {

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
		frontDriverW.motorTorque = m_verticalInput * motorForce + 380*m_verticalInput;
		frontPassengerW.motorTorque = m_verticalInput * motorForce + 380*m_verticalInput;
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
        Debug.Log(_pos);
		_collider.GetWorldPose(out _pos, out _quat);
        
		_transform.position = _pos;
		_transform.rotation = _quat;
	}

	public void drift()
	{
		colliderRWL = rearDriverT.GetComponent<WheelCollider>();
		colliderRWR = rearPassengerT.GetComponent<WheelCollider>();

		fFirictionRWL = colliderRWL.forwardFriction;
		sFirictionRWL = colliderRWL.forwardFriction;
		fFirictionRWR = colliderRWR.forwardFriction;
		sFirictionRWR = colliderRWR.forwardFriction;


		//shift 키가 눌리면 드리프트를 한다
		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			fFirictionRWL.stiffness = handBreakSlipRate;
			colliderRWL.forwardFriction = fFirictionRWL;

			sFirictionRWL.stiffness = handBreakSlipRate;
			colliderRWL.sidewaysFriction = fFirictionRWL;

			fFirictionRWR.stiffness = handBreakSlipRate;
			colliderRWR.forwardFriction = fFirictionRWR;

			sFirictionRWR.stiffness = handBreakSlipRate;
			colliderRWR.sidewaysFriction = fFirictionRWR;
		}
		//shift 키가 떼지면 마찰을 복원한다
		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			fFirictionRWL.stiffness = slipRate;
			colliderRWL.forwardFriction = fFirictionRWL;

			sFirictionRWL.stiffness = slipRate;
			colliderRWL.sidewaysFriction = fFirictionRWL;

			fFirictionRWR.stiffness = slipRate;
			colliderRWR.forwardFriction = fFirictionRWR;

			sFirictionRWR.stiffness = slipRate;
			colliderRWR.sidewaysFriction = fFirictionRWR;
		}
	}

	private void FixedUpdate()
	{
		GetInput();
		Steer();
		Accelerate();
		UpdateWheelPoses();
		drift();
	}

	private float m_horizontalInput;
	private float m_verticalInput;
	private float m_steeringAngle;

	public WheelCollider frontDriverW, frontPassengerW;
	public WheelCollider rearDriverW, rearPassengerW;
	public WheelCollider colliderRWL;
	public WheelCollider colliderRWR;
	public WheelFrictionCurve fFirictionRWL;
	public WheelFrictionCurve sFirictionRWL;
	public WheelFrictionCurve fFirictionRWR;
	public WheelFrictionCurve sFirictionRWR;
	public Transform frontDriverT, frontPassengerT;
	public Transform rearDriverT, rearPassengerT;
	public float maxSteerAngle = 30;
	public float motorForce = 50;

	private float slipRate = 1.0f;
	private float handBreakSlipRate = 0.4f;

}