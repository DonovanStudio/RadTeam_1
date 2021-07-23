using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeButton : MonoBehaviour
{
    [SerializeField] int buttonVal;
    [SerializeField] FlutePuzzle fp;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Playing Puzzle Sound");
        puzzle2Audio();
        if (other.transform.tag == "Player")
            fp.ButtonPressed(buttonVal);
            
    }
    public void puzzle2Audio()
    {
        if (buttonVal == 1){
            FMODUnity.RuntimeManager.PlayOneShot("event:/Puzzle 2 Oneshots/Note 1");
        }else if (buttonVal == 2){
            FMODUnity.RuntimeManager.PlayOneShot("event:/Puzzle 2 Oneshots/Note 2");
        }
        else if (buttonVal == 3){
            FMODUnity.RuntimeManager.PlayOneShot("event:/Puzzle 2 Oneshots/Note 3");
        }
        else if (buttonVal == 4){
            FMODUnity.RuntimeManager.PlayOneShot("event:/Puzzle 2 Oneshots/Note 5");
        }
    }
}
