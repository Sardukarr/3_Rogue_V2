using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] int thrustPower=1500;
    [SerializeField] int rotatePower=150;
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
        Thrust();
        Rotate();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag)
        {
            case "Friendly":
                print("sukcess "+this.name);
                //do nothing
                break;
            default:
                print("dead " + this.name);
                break;

        }
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

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * thrustPower);
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
            audioSource.Stop();
      
    }
}
