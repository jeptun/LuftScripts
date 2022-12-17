using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropColliderDetection : MonoBehaviour
{

    [SerializeField] AudioClip audioSource;
    [SerializeField] GameObject dropObstacle;
    private AudioSource obstacleSoundSource;

    private bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        obstacleSoundSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!activated)
            {
                activated = true;
                obstacleSoundSource.PlayOneShot(audioSource);
                dropObstacle.SetActive(true);
                dropObstacle.GetComponent<Rigidbody>().AddForce(100, 0, 0);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            obstacleSoundSource.Stop();
            activated = false;
        }
    }
}
