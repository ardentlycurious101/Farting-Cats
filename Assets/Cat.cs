using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] float mainThrust = 200f;
    [SerializeField] float rotationThrust = 200f;

    Rigidbody rigidBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Deadly":
                print("Deadly");
                break;
            case "OK":
                print("OK");
                break;
            case "Fuel":
                print("Fuel");
                break;
        }
    }


    private void Thrust()
    {

        float thrustSpeed = Time.deltaTime * mainThrust;

        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustSpeed);
            if (!audioSource.isPlaying) // so it doesn't layer
            {
                audioSource.Play();
            }
        }
        //else // commented out because mp3 file is very short, might sound unnatural.
        //{
        //    audioSource.Stop();
        //}

    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; // take manual control of rotation
        float rotationSpeed = Time.deltaTime * rotationThrust;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }

        rigidBody.freezeRotation = false; // resume physics control of rotation

    }

}
