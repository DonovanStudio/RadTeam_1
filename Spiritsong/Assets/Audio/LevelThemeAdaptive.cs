using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThemeAdaptive : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    [FMODUnity.EventRef]
    public string fmodEvent;

    [SerializeField]
    [Range(0f, 1.5f)]
    private float time;

    private void OnEnable()
    {
        PlayerController.StartJump += JumpSoundOn;
        PlayerController.EndJump += JumpSoundOff;
        PlayerController.StartDash += DashSoundOn;
        PlayerController.EndDash += DashSoundOff;
    }

    private void OnDisable()
    {
        PlayerController.StartJump -= JumpSoundOn;
        PlayerController.EndJump -= JumpSoundOff;
        PlayerController.StartDash -= DashSoundOn;
        PlayerController.EndDash -= DashSoundOff;
    }

    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }

    // Update is called once per frame
    void Update()
    {
        instance.setParameterByName("Time", time);
        //time = AudioManager.instance.GetJumpParameter();
    }

    void JumpSoundOn()
    {
        time = 1.5f;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Jump SFX");
    }

    void JumpSoundOff()
    {
        time = 0f;
    }

    void DashSoundOn()
    {

    }

    void DashSoundOff()
    {

    }
}
