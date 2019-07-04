using UnityEngine.SceneManagement;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	bool collisionsDisabled = false;

	Rigidbody rigidBody;
	AudioSource audioSource;

	bool isTransitioning = false;

	[SerializeField] float ThrustForce = 15.0f;
	[SerializeField] float TurnRate = 50.0f;

	[SerializeField] AudioClip mainEngine;
	[SerializeField] AudioClip levelCompleteJingle;
	[SerializeField] AudioClip DeathExplosion;

	[SerializeField] ParticleSystem mainEngineParticles;
	[SerializeField] ParticleSystem levelCompleteParticles;
	[SerializeField] ParticleSystem DeathExplosionParticles;

	void Start()
    {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
    }

	void OnCollisionEnter(Collision collision)
	{
		if ( isTransitioning || collisionsDisabled ) { return; }

		switch (collision.gameObject.tag)
		{
			case "Friendly":
				break;
			case "Finish":
				isTransitioning = true;
				audioSource.Stop();
				audioSource.PlayOneShot(levelCompleteJingle);
				mainEngineParticles.Stop();
				levelCompleteParticles.Play();
				Invoke("LoadNextScene", 1.0f);
				break;
			default:
				isTransitioning = true;
				audioSource.Stop();
				audioSource.PlayOneShot(DeathExplosion);
				mainEngineParticles.Stop();
				DeathExplosionParticles.Play();
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
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int nextLevelIndex = (currentSceneIndex >= SceneManager.sceneCountInBuildSettings - 1) ? 0: ++currentSceneIndex;
		SceneManager.LoadScene(nextLevelIndex); // TODO: parameterize the level index
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
		if (Debug.isDebugBuild)
		{ 
			if (Input.GetKeyDown(KeyCode.N))
			{
				LoadNextScene();
			}
			else if (Input.GetKeyDown(KeyCode.C))
			{
				collisionsDisabled = !collisionsDisabled;
			}
		}
		if (isTransitioning == false)
		{
			Rotate();
		}
    }

	void FixedUpdate()
	{
		if (isTransitioning == false)
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
				audioSource.PlayOneShot(mainEngine, 0.8f);
			}
			mainEngineParticles.Play();
		}
		else
		{
			audioSource.Stop();
			mainEngineParticles.Stop();
		}
	}

	private void Rotate()
	{
		float frameIndependentTurnRate = TurnRate * Time.deltaTime;
		if (Input.GetKey(KeyCode.A))
		{
			RotateManually(frameIndependentTurnRate);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			RotateManually(-frameIndependentTurnRate);
		}
	}

	private void RotateManually(float frameIndependentTurnRate)
	{
		rigidBody.freezeRotation = true;
		transform.Rotate(Vector3.forward * frameIndependentTurnRate);
		rigidBody.freezeRotation = false;
	}
}
