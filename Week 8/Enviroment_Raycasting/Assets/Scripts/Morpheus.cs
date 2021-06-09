using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Morpheus : MonoBehaviour
{
    public AudioSource audioSource;
    private bool startedTalking = false;
    private bool startedJumpSequence = false;
    public Animator anim;
    public AudioClip jumpSound;
    public AudioClip landSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void playSpeech()
    {
        Debug.Log("Playing morpheus speech");
        audioSource.Play();
        startedTalking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startedTalking && !audioSource.isPlaying && !startedJumpSequence)
        {
            startedJumpSequence = true;
            startJump();
        }
    }

    private void startJump()
    {
        anim.enabled = true;
        anim.SetBool("Jumping", true);
        Debug.Log("start jump");
    }
    
    private void playJumpSound()
    {
        audioSource.clip = jumpSound;
        audioSource.Play();
    }
    private void playLandSound()
    {
        audioSource.clip = landSound;
        audioSource.Play();
    }

    private void startNeoBanter()
    {
        GameObject.FindWithTag("Player").GetComponent<Neo>().playBanter();
    }
}
