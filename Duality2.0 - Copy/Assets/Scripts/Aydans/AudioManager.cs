using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
   

    public AudioClip[] onBorn;
    public AudioClip[] onDeath;



    public AudioSource audioSource;
    
   


    public void PlayOnBorn()
    {
        if(onBorn.Length > 0)
               audioSource.PlayOneShot(onBorn[Random.Range(0, onBorn.Length)]);
    }

    public void PlayOnDeath()
    {
        if (onDeath.Length > 0)
            audioSource.PlayOneShot(onDeath[Random.Range(0, onDeath.Length)]);
    }

}
