using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThemeAdaptive : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    [FMODUnity.EventRef]
    public string fmodEvent;

    public int jumping;
    public int dashing;

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
        instance.setParameterByName("Jumping", jumping);
        //time = AudioManager.instance.GetJumpParameter();
        instance.setParameterByName("Dashing", dashing);
    }

    void JumpSoundOn()
    {
        jumping = 1;
        //Debug.Log("Playing Jump SFX");
        FMODUnity.RuntimeManager.PlayOneShot("event:/Jump SFX");
    }

    void JumpSoundOff()
    {
        jumping = 0;
    }

    void DashSoundOn()
    {
        dashing = 1;
    }

    void DashSoundOff()
    {
        dashing = 0;
    }
}
