using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSoundBehaviour : MonoBehaviour
{
    public AudioSource audioSource;
    public Morpheus morpheus;
    private bool startedPlaying;
    private bool playedMorpheus = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
        startedPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startedPlaying && !audioSource.isPlaying && !playedMorpheus)
        {
            GameObject.FindWithTag("Player").GetComponent<Neo>().canMove = true;
            morpheus.playSpeech();
            playedMorpheus = true;
        }
    }
    
}
