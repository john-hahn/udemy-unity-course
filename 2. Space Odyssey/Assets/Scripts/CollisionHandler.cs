using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;
    [SerializeField] AudioClip levelSuccess;
    [SerializeField] AudioClip levelFail;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem failParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        //RespondToDebugKeys();
    }

/*     void RespondToDebugKeys() 
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    } */

    void OnCollisionEnter(Collision other) {
        if (isTransitioning || collisionDisabled)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;

            case "Finish":
                StartSuccessSequence();
                break;

            case "Fuel":
                Debug.Log("You picked up fuel");
                break;

            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence() 
    {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(levelSuccess);

        successParticles.Play();
    
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayTime);
    }

    void StartCrashSequence() 
    {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(levelFail);

        failParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayTime);
    }

    void ReloadLevel() 
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void LoadNextLevel() 
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings) 
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
}
