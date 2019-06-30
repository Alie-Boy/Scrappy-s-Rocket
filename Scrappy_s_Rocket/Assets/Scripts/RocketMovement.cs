using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{

	Rigidbody rigidBody;
	AudioSource audioSource;

	[SerializeField] float ThrustForce = 15.0f;
	[SerializeField] float TurnRate = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
		Rotate();
    }

	void FixedUpdate()
	{
		Thrust();
	}

	private void Thrust()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			rigidBody.AddRelativeForce(Vector3.up * ThrustForce);
			if (audioSource.isPlaying == false)
			{
				audioSource.Play();
			}
		}
		else
		{
			audioSource.Stop();
		}
	}

	private void Rotate()
	{
		rigidBody.freezeRotation = true;

		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.forward * TurnRate * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(-Vector3.forward * TurnRate * Time.deltaTime);
		}

		rigidBody.freezeRotation = false;
	}
}
