using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplecameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        objectToFollow = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //this.transform.parent = objectToFollow;
        //transform.position = new Vector3(0,0,0);
		
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAtTarget();
		MoveToTarget();
		
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

    private Transform objectToFollow;
	public Vector3 offset;
	public float followSpeed = 10;
	public float lookSpeed = 10;
}
