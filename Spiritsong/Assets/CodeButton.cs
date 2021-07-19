using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeButton : MonoBehaviour
{
    [SerializeField] int buttonVal;
    [SerializeField] FlutePuzzle fp;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "player")
            fp.ButtonPressed(buttonVal);
    }
}
