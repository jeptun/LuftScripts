using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinisPoint : MonoBehaviour
{
    [SerializeField] float delayLevelFinishTime = 2f;
    [SerializeField] ParticleSystem successParticles;
    //-----------------------------------------------
    // SOUND
    private AudioSource finishSoundSource;
    //-----------------------------------------------
    [SerializeField] bool activated = false;
    void Start()
    {
        finishSoundSource = GameObject.FindGameObjectWithTag("Finish").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!activated) {
                activated = true;
                FinishSequence();
            }
        }
    }
    void FinishSequence()
    {
        finishSoundSource.Play();
        successParticles.Play();
        //GetComponent<Movement>().enabled = false;
        Invoke(nameof(NextLevel), delayLevelFinishTime);
    }
    void NextLevel()
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
