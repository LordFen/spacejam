using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip[] tabClips;
	// Use this for initialization
	void Start () {
        PlayNextSong();
	}

    void PlayNextSong()
    {
        audioSource.Stop();
        Debug.Log("NextSong");
        audioSource.clip=tabClips[Random.Range(0, tabClips.Length)];
        audioSource.Play();
        Invoke("PlayNextSong", audioSource.clip.length);
    }
	

}
