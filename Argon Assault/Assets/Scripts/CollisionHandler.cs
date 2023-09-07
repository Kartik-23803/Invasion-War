using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem crashVFX;
    [SerializeField] float loadDelay = 2f;
    void OnTriggerEnter(Collider other)
    {
        CrashSequence();
    }

    void CrashSequence()
    {
        crashVFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    // void LoadNextLevel()
    // {
    //     int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    //     int nextSceneIndex = currentSceneIndex + 1;
    //     if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
    //     {
    //         nextSceneIndex = 0;
    //     }
    //     SceneManager.LoadScene(nextSceneIndex);        
    // }
}
