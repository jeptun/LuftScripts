using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private MeshRenderer visual;
    private SphereCollider sphereColl;
    // private ParticleSystem collectParticle;
    private bool collected = false;

    private void Awake()
    {
        visual = this.GetComponentInChildren<MeshRenderer>();
    
       // collectParticle = this.GetComponentInChildren<ParticleSystem>();
       // collectParticle.Stop();
    }

    private void OnTriggerEnter()
    {
        if (!collected)
        {
           // collectParticle.Play();
            CollectCoin();
        
        }
    }

    private void CollectCoin()
    {
        collected = true;
        visual.gameObject.SetActive(false);
        GameEventsManager.instance.CoinCollected();
    }
}
