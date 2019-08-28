using System;
using UnityEngine;

[DisallowMultipleComponent]

public class Oscillator : MonoBehaviour
{
	[SerializeField] Vector3 movementVector = new Vector3(0f, 15f, 0f);
	[SerializeField] float period = 1.5f;

    [Range(0,1)]
    [SerializeField]
    float movementFactor;

    Vector3 startingPos;

    void Start()
    {
        startingPos = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; } // to protect from NaN

        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSineWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSineWave / 2f + 0.5f;
		Vector3 offset = movementVector * movementFactor;
		transform.position = startingPos + offset;
	}
}
