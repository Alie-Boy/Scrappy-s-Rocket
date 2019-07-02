using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
	[SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
	[SerializeField] float period = 2f;
	[Range(0f, 1f)] [SerializeField] float movementFactor;

	Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
		startingPos = transform.position;
		if (period <= 0) { period = 1f; }
    }

    // Update is called once per frame
    void Update()
    {
		float cycles = Time.time / period;
		const float TAU = Mathf.PI * 2f;

		float sineOutput = Mathf.Sin(cycles * TAU);
		movementFactor = sineOutput / 2f + 0.5f;	// changing [-1, +1] to [0, 1]

		Vector3 offset = movementVector * movementFactor;
		transform.position = startingPos + offset;
    }
}
