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

    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
        
    }

    // Update is called once per frame
    void Update()
    {
        instance.setParameterByName("Time", time);
        time = AudioManager.instance.GetJumpParameter();
    }
}
