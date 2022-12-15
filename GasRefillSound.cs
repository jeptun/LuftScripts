using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasRefillSound : MonoBehaviour
{
    //public GameObject vehicle;
    //public GameObject fuelStation;
    [SerializeField] AudioClip mainEngineOff;
    [SerializeField] bool activated = false;

    AudioSource fuelStationSoundSource;
 
        private void Start()
    {
        fuelStationSoundSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (activated)
        {
            fuelStationSoundSource.UnPause();
         
        }
        else if (!activated)
        {
            fuelStationSoundSource.Pause();
            
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (!activated)
            {
                activated = true;
                fuelStationSoundSource.PlayOneShot(mainEngineOff);
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        
        fuelStationSoundSource.Pause();
        activated = false;
    }

}
