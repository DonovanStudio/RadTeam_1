using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject layer1;
    public GameObject layer2;
    public GameObject layer3;
    public float jumpDuration = 1.0f;
    public float dashDuration = 1.0f;

    private AudioSource baseLayer;
    private AudioSource jumpLayer;
    private AudioSource dashLayer;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        baseLayer = layer1.GetComponent<AudioSource>();
        jumpLayer = layer2.GetComponent<AudioSource>();
        jumpLayer.volume = 0;
        dashLayer = layer3.GetComponent<AudioSource>();
        dashLayer.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JumpUnlocked()
    {
        jumpLayer.volume = 0.1f;
    }

    public void PlayJumpSound()
    {
        jumpLayer.volume = 1;
        StartCoroutine("StopJumpSound");
    }

    private IEnumerator StopJumpSound()
    {
        yield return new WaitForSeconds(jumpDuration);
        jumpLayer.volume = 0.1f;
    }

    public void DashUnlocked()
    {
        dashLayer.volume = 0.1f;
    }

    public void PlayDashSound()
    {
        dashLayer.volume = 1;
        StartCoroutine("StopDashSound");
    }

    private IEnumerator StopDashSound()
    {
        yield return new WaitForSeconds(dashDuration);
        dashLayer.volume = 0.1f;
    }

    public void PauseAllMovementSounds()
    {
        baseLayer.Pause();
        jumpLayer.Pause();
        dashLayer.Pause();
    }

    public void ResumeMovementSounds()
    {
        baseLayer.Play();
        jumpLayer.Play();
        dashLayer.Play();
    }

}
