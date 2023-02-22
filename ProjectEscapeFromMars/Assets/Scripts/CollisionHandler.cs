using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
   [SerializeField] float leveDelay = 1f;
   [SerializeField] AudioClip successAudio;

   [SerializeField] AudioClip crashAudio;


   [SerializeField] ParticleSystem successParticles;

   [SerializeField] ParticleSystem crashParticle;
   

    AudioSource audioSource;

    bool isTransitioning = false;

    bool collisionDisabled = false;
void Start()
    {
      audioSource = GetComponent<AudioSource>();
    }

void Update()
{
    RespondToDebugKeys();
}

void RespondToDebugKeys()
{
    if (Input.GetKeyDown(KeyCode.L))
    {
        LoadNextLevel();
    }
    else if (Input.GetKeyDown(KeyCode.C))
    {
        collisionDisabled = !collisionDisabled;
    }
}
     void OnCollisionEnter(Collision other) 
    { 
        if (isTransitioning || collisionDisabled)
        {
            return;
        }

        switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("This thing is Friendly");
                break;
                case "Finish":
                   StartSuccessSequence();
                break;
                default:
                  StartCrash();
                break;
            }
    }


    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
         Invoke("LoadNextLevel",leveDelay);

    }

    void StartCrash()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
       Invoke("ReloadLevel",leveDelay);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
         int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    
}

