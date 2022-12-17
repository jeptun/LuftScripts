using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Coins : MonoBehaviour
{
    [SerializeField] GameObject coin;
    [SerializeField] GameObject vehicle;
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
                var em = coinParticleSystem.emission;
                var dur = coinParticleSystem.main.duration;
                activated = true;
                ScoringSystem.score +=  1;
                em.enabled = true;
                audioSource.PlayOneShot(coinAudioSource);
                coinParticleSystem.Play();
              //  uiScore.text = score.ToString();
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
