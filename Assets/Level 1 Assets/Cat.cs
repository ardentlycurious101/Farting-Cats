using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cat : MonoBehaviour
{
    [SerializeField] float mainThrust = 200f;
    [SerializeField] float rotationThrust = 200f;

    [SerializeField] AudioClip meow;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem fireFartParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive, Dying, Transcending };
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: somwhere stop sound for death
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Deadly":
                StartDeathSequence();
                break;
            case "OK":
                print("OK");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                break;
        }
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadFirstLevel", 2.0f);
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel", 2.0f);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void RespondToThrustInput()
    {

        float thrustSpeed = Time.deltaTime * mainThrust;

        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            ApplyThrust(thrustSpeed);
        } 
        //else // commented out because mp3 file is very short, might sound unnatural.
        //{
        //    audioSource.Stop();
        //}
        else
        {
            fireFartParticles.Stop();
        }
    }

    private void ApplyThrust(float thrustSpeed)
    {
        rigidBody.AddRelativeForce(Vector3.up * thrustSpeed);
        if (!audioSource.isPlaying) // so it doesn't layer
        {
            audioSource.PlayOneShot(meow);
        }
        fireFartParticles.Play();
    }

    private void RespondToRotateInput()
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
;