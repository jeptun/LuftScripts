using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasRefillSound : MonoBehaviour
{
    //public GameObject vehicle;
    //public GameObject fuelStation;
   // [SerializeField] AudioClip mainEngineOff;
    [SerializeField] bool activated = false;
    [SerializeField] ParticleSystem refillParticleSystem;

    private AudioSource fuelStationSoundSource;
 
        private void Start()
    {
        fuelStationSoundSource = GameObject.FindGameObjectWithTag("Fuel").GetComponent<AudioSource>();
        refillParticleSystem = GetComponent<ParticleSystem>();
    }

    //private void Update()
    //{
    //    if (activated)
    //    {
    //        fuelStationSoundSource.UnPause();
         
    //    }
    //    else if (!activated)
    //    {
    //        fuelStationSoundSource.Pause();
            
    //    }
    //}

    private void OnTriggerEnter(Collider collider)
    {
       
        if (collider.CompareTag("Player"))
        {
            if (!activated)
            {
                activated = true;
                fuelStationSoundSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {

        fuelStationSoundSource.Stop();
        activated = false;
    }

}
