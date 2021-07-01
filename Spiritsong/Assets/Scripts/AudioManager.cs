using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : ScriptableObject
{
    public event Action<float> OnJumpStarted;
    public event Action<float> OnLanding;

    private float jumpParameter = 0f;

    public float Jump
    {
        get => jumpParameter;
        set
        {
            jumpParameter = 1.5f;
            OnJumpStarted?.Invoke(jumpParameter);
        }
    }

    public float Land
    {
        get => jumpParameter;
        set
        {
            jumpParameter = 0f;
            OnLanding?.Invoke(jumpParameter);
        }
    }


    //public float GetJumpParameter()
    //{
    //    return jumpParameter;
    //}

    //public void SetJumpParameter(float value)
    //{
    //    jumpParameter = value;
    //}

}
