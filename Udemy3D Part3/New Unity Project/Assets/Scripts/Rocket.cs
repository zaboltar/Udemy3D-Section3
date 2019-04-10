using UnityEngine;
using UnityEngine.SceneManagement;


public class Rocket : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEnginePartSys;
    [SerializeField] ParticleSystem successPartSys;
    [SerializeField] ParticleSystem deathPartSys;

    enum State { Alive, Dying, Trascending}
    State state = State.Alive;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    void OnCollisionEnter (Collision collision)
    {
        if (state != State.Alive) {return;} //ignore collisions upon certain death 

        switch (collision.gameObject.tag)
        {
            case "friendly":
                // do nothing
                break;

            case "fuel":
                //Destroy(collision.gameObject);
                break;

            case "slayer":
                break;

            case "finish":
                StartSuccessSequence();
                break;

            default:
                StartDeathSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        state = State.Trascending;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successPartSys.Play();
        Invoke("LoadNextLevel", 2f); //parametrize time
    }

    void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathPartSys.Play();
        Invoke("LoadFirstLevel", 2f);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1); // ToDo:Allow more than 2 lvls -.-
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }



    void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space)) //can thrust while rotating
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            mainEnginePartSys.Stop();
        }
    }

    private void RespondToRotateInput()
    {
        rb.freezeRotation = true; // take manual control of rotation
    
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rb.freezeRotation = false;    // resume physics control of rotation
    }

    void ApplyThrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust);

        if (!audioSource.isPlaying) // so it doesnt repeat >.<
        {
            //thrust.Play();
            audioSource.PlayOneShot(mainEngine);
        }
        mainEnginePartSys.Play();
    }
    /*void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "fuel")
        {
            fuelGathered += 1;
            Destroy(col.gameObject);
        }
    }*/
}
