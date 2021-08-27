using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustRate = 1000f;
    [SerializeField] float rotationRate = 1000f;
    [SerializeField] AudioClip engineThrust;

    [SerializeField] ParticleSystem mainThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    // Processes the main thrusting through the input of spacebar
    void ProcessThrust() 
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    // Processes the rotation through the input of 'A' or 'D' for left and right
    // rotation respectively
    void ProcessRotation() 
    {
        if (Input.GetKey(KeyCode.A))
        {
            StartLeftRotation();
        }
        
        else if (Input.GetKey(KeyCode.D))
        {
            StartRightRotation();
        }

        else
        {
            StopRotation();
        }
    }

    // Starts main thrusting by adding force in the positive-y direction and 
    // plays the particle system and audio clip corresponding to it
    void StartThrusting() 
    {
        rb.AddRelativeForce(Vector3.up * thrustRate * Time.deltaTime);
        if (!mainThrusterParticles.isPlaying)
        {
            mainThrusterParticles.Play();
        }
            
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(engineThrust);
        }
    }

    // Stops particle system and audio regarding the main thrusting
    void StopThrusting() 
    {
        mainThrusterParticles.Stop();
        audioSource.Stop();
    }

    // Starts left rotation by rotating in the positive-z direction and 
    // plays the particle system corresponding to it
    void StartLeftRotation() 
    {
        ApplyRotation(rotationRate);
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }

    // Starts right rotation by rotating in the negative-z direction and 
    // plays the particle system corresponding to it
    void StartRightRotation() 
    {
        ApplyRotation(-rotationRate);
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }

    // Stops all particle systems regarding rotation
    void StopRotation() 
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }

    // Helper function to apply rotation in the z-direction by the inputted
    // value
    void ApplyRotation(float rotationThisFrame) 
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
