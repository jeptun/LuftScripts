using UnityEngine.Audio;
using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;
     void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
    }

     void Start()
    {
        Play("MainMusic");
       // Play("Obstacle1");
    }
    public void Play(string name)
    {
       Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Pridej Main zvuk" + name);
            return;
        }

        //if (PauseMenu.GameIsPaused)
        //{
        //    s.source.pitch *= 0.5f;
        //}
        s.source.Play();
    }

    //public void StopPlaying(string sound)
    //    {
    //     Sound s = Array.Find(sounds, item => item.name == sound);
    //    if (s == null)
    //    {
    //         Debug.LogWarning("Sound: " + name + " not found!");
    //         return;
    //    }

    //     s.source.Stop ();
    //    }
}
