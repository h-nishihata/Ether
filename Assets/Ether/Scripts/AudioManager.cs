using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource source;


    void Start()
    {
        source = this.GetComponent<AudioSource>();

    }

    public void Play(int numClip)
    {
        source.clip = clips[numClip];
        source.Play();
    }
}
