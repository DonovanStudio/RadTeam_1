using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class NameTagControls : MonoBehaviour
{
    public GameObject[] nameTags;

    [YarnCommand("NPCTag")]
    public void ShowNPCTag()
    {
        nameTags[0].SetActive(true);
        nameTags[1].SetActive(false);
    }

    [YarnCommand("PCTag")]
    public void ShowPlayerTag()
    {
        nameTags[0].SetActive(false);
        nameTags[1].SetActive(true);
    }

}
