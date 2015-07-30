using UnityEngine;
using System.Collections;

public class StartupMovie : MonoBehaviour {
    public MovieTexture myMovie;
    private float duration = 0;

    void Start() 
    { 
        myMovie.Play(); 
    }

    void Update()
    {
        //if (duration < myMovie.duration)
        //{
        //    duration++;
        //}
        //else
        //{
        //    Application.LoadLevel("Menu");
        //}
    }
}
