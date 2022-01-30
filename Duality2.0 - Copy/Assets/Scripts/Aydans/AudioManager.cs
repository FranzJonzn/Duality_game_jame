using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioClip impact;
    AudioSource audioSource;
    
   
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
     
    private void Awake()
    {
        audioSource.PlayOneShot(impact, 0.7F);
    }
}
