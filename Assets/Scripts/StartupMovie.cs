using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]

public class StartupMovie : MonoBehaviour {
    public MovieTexture movie;
    private AudioSource audio;

    void Start() 
    {
        GetComponent<RawImage>().texture = movie as MovieTexture;
        audio = GetComponent<AudioSource>();
        audio.clip = movie.audioClip;
        movie.Play();
        audio.Play();
    }

    void LateUpdate()
    {
        if (!movie.isPlaying)
        {
            float fadeTime = GameObject.Find("Main Camera").GetComponent<Fading>().BeginFade(1);
            StartCoroutine(changeLevelFade(fadeTime));
        }
    }

    private IEnumerator changeLevelFade(float fadeTime)
    {
        yield return new WaitForSeconds(fadeTime);
        Application.LoadLevel("Menu");
    }
}
