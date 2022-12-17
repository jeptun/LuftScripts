using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Coins : MonoBehaviour
{
    [SerializeField] int coinValue = 10;
    [SerializeField] AudioClip coinAudioSource;
    [SerializeField] Text uiScore;
    [SerializeField] ParticleSystem coinParticleSystem;

    public MeshRenderer coinMesh;
    private AudioSource audioSource;

    [SerializeField] bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!activated)
            {
                var em = coinParticleSystem.emission;
                var dur = coinParticleSystem.main.duration;
                activated = true;
                ScoringSystem.score += coinValue;
                em.enabled = true;
                audioSource.PlayOneShot(coinAudioSource);
                coinParticleSystem.Play();
                Destroy(coinMesh);
                Invoke(nameof(DestroyCoin), dur);
               

            }
        }
    }
   private void DestroyCoin()
    {
        Destroy(gameObject);
    }
}
