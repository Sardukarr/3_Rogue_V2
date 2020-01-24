using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{

    [SerializeField] int thrustPower=1500;
    [SerializeField] int rotatePower=150;
    [SerializeField] float timeToDie = 2.5f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip victorySound;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem victoryParticles;


    Rigidbody rigidBody;
    AudioSource audioSource;
    enum State {  Alive, Dying, Transcending};
    State state = State.Alive;
    bool ColisionEnabled=true;
    //   float timeToDie = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
       // thrustPower = 50;
      //  rotatePower = 50;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

    }

    private void ProcessInput()
    {
        if (state == State.Alive)
        {
            RespondToThrust();
            Rotate();
        }
        //  else
        // audioSource.Stop();
        if (Debug.isDebugBuild)
            RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
            LoadNextScene();
        else if (Input.GetKey(KeyCode.C))
        {
             ColisionEnabled = !ColisionEnabled;
        }

    }   

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || !ColisionEnabled)
            return;
        switch (collision.collider.tag)
        {
            case "Friendly":
                print("sukcess "+this.name);
                //do nothing
                break;
            case "Finish":
                SuccesSequence();
                break;
            default:
                DeathSequence();
                break;

        }
    }

    private void DeathSequence()
    {
        Invoke("LoadCurrentScene", timeToDie);
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        state = State.Dying;
        deathParticles.Play();
        mainEngineParticles.Stop();
    }

    private void SuccesSequence()
    {
        Invoke("LoadNextScene", timeToDie);
        audioSource.Stop();
        audioSource.PlayOneShot(victorySound);
        state = State.Transcending;
        victoryParticles.Play();
    }

    private void LoadCurrentScene()
    {
        var pos = transform;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      //  var wrackage=GameObject.Instantiate(GameObject.Find("V2_Wrackage"));
      //  wrackage.transform.position = pos.position;
      //  wrackage.transform.rotation = pos.rotation;
       // transform.
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % (SceneManager.sceneCountInBuildSettings));
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; // take manual control

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotatePower);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * Time.deltaTime * rotatePower);
        }
        rigidBody.freezeRotation = false; //resume physics control
                                                                          
        rigidBody.constraints = RigidbodyConstraints.FreezeRotationX // reapply constraints
            | RigidbodyConstraints.FreezeRotationY
            | RigidbodyConstraints.FreezePositionZ;
    }
     
    private void RespondToThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * thrustPower);
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(mainEngine);
            mainEngineParticles.Play();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }
}
