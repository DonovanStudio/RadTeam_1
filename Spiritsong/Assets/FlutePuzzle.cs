using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlutePuzzle : MonoBehaviour
{
    [SerializeField] int[] code;
    int correct;
    public void ButtonPressed(int num)
    {
        if (num == code[correct])
            correct++;
        if (correct == code.Length)
            PuzzleComplete();
    }
    void PuzzleComplete()
    {

    }
}
