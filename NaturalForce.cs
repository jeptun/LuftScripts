using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalForce : MonoBehaviour
{

    [SerializeField] AudioClip audioSource;
    [SerializeField] GameObject vehicle;
    private AudioSource windeSoundSource;
    [SerializeField] float xForce = 0;
    [SerializeField] float yForce = 0;
    [SerializeField] float zForce = 0;
    // Start is called before the first frame update
    void Start()
    {
        windeSoundSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
                windeSoundSource.PlayOneShot(audioSource);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            windeSoundSource.Stop();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("windAHoj");
            vehicle.GetComponent<Rigidbody>().AddForce(xForce, yForce, zForce);

        }
    }
}