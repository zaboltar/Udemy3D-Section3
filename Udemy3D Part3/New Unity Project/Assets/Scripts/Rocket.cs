using UnityEngine;
using UnityEngine.SceneManagement;


public class Rocket : MonoBehaviour
{
    Rigidbody rb;
    AudioSource thrust;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
   // public int fuelGathered;

    enum State { Alive, Dying, Trascending}
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thrust = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
    }

    void OnCollisionEnter (Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        } //ignore collisions upon certain death 

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
                Debug.Log("TouchDown!");
                state = State.Trascending;
                Invoke("LoadNextLevel", 1f); //parametrize time
                
                break;

            default:
                //kill player & reload
                state = State.Dying;
                Invoke("LoadFirstLevel", 1f);
                SceneManager.LoadScene(0);
                break;
              
            
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1); // ToDo:Allow more than 2 lvls -.-
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }



    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) //can thrust while rotating
        {
            rb.AddRelativeForce(Vector3.up * mainThrust);

            if (!thrust.isPlaying) // so it doesnt repeat >.<
            {
                thrust.Play();
            }

        }
        else
        {
            thrust.Stop();
        }
    }

    private void Rotate()
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

    /*void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "fuel")
        {
            fuelGathered += 1;
            Destroy(col.gameObject);
        }
    }*/
}
