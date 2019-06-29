using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{

	Rigidbody rigidBody;
	public float TurnRate = 50.0f;
	public float Force = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
		rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		ProcessInput();
    }

	private void ProcessInput()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			rigidBody.AddRelativeForce(Vector3.up * Force * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.forward * TurnRate * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(-Vector3.forward * TurnRate * Time.deltaTime);
		}
	}
}
