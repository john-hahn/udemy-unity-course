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

    void ProcessThrust() 
    {
        if (Input.GetKey(KeyCode.Space))
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
        else
        {
            mainThrusterParticles.Stop();
            audioSource.Stop();
        }
    }

    void ProcessRotation() 
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationRate);
            if (!rightThrusterParticles.isPlaying)
            {
                rightThrusterParticles.Play();
            }
        }
        
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationRate);
            if (!leftThrusterParticles.isPlaying)
            {
                leftThrusterParticles.Play();
            }
        }

        else
        {
            leftThrusterParticles.Stop();
            rightThrusterParticles.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame) 
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
