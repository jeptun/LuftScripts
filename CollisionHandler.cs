using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 1f;
    [SerializeField] float delayLevelFinishTime = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                FinishSequence();
                Debug.Log("Finish");
                break;
            //case "Fuel":
            //    Debug.Log("Fuel");
            //    break;
            default:
                StartCrashSequence();
                Debug.Log("Sorry");
                break;
        }
    }
    //Todo Sound Particle
    void StartCrashSequence()
    {
        audioSource.PlayOneShot(crash);
       GetComponent<Movement>().enabled = false;
       Invoke(nameof(ReloadLevel), LevelLoadDelay);
    }
    void FinishSequence()
    {
        audioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(NextLevel), delayLevelFinishTime);
    }
    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
           nextSceneIndex = 0 ;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);     
    }
 
}
