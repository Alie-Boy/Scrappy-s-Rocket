using UnityEngine.SceneManagement;
using UnityEngine;

public class Rocket : MonoBehaviour
{

	Rigidbody rigidBody;
	AudioSource audioSource;

	enum State { Alive, Dying, Transcending };
	State state = State.Alive;

	[SerializeField] float ThrustForce = 15.0f;
	[SerializeField] float TurnRate = 50.0f;

    void Start()
    {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
    }

	void OnCollisionEnter(Collision collision)
	{
		if (state != State.Alive) { return; }

		switch (collision.gameObject.tag)
		{
			case "Friendly":
				break;
			case "Finish":
				state = State.Transcending;
				Invoke("LoadNextScene", 1.0f);
				break;
			default:
				state = State.Dying;
				Invoke("LoadFirstScene", 1.0f);
				break;
		}
	}

	private void LoadFirstScene()
	{
		SceneManager.LoadScene(0);
	}

	private void LoadNextScene()
	{
		SceneManager.LoadScene(1);
	}

	void Update()
    {
		if (state != State.Dying)
		{
			Rotate();
		}
    }

	void FixedUpdate()
	{
		if (state != State.Dying)
		{
			Thrust();
		}
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
