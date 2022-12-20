using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour
{



    [SerializeField] AudioClip crash;

    private AudioSource audioSource;
    bool isTransitioning = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                Debug.Log("Finish");
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            case "Gold":
                Debug.Log("gold");
                break;
            default:
                StartCrashSequence();
                Debug.Log("Sorri");
                break;
        }
    }

    //Todo Sound Particle
    void StartCrashSequence()
    {
        isTransitioning = true;
        Debug.Log("uilllihlihl");
        audioSource.PlayOneShot(crash);
    }
}
