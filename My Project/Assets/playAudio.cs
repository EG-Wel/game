using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAudio : MonoBehaviour
{
    //public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        //source = GetComponent<AudioSource>();
    }

    public void Play(AudioSource source)
    {
        source.Play();
    }
}
