using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using TMPro;
using UnityEngine.UI;

public class TextExpressions : MonoBehaviour
{
    public TextMeshProUGUI text;

    [YarnCommand("Small")]
    public void SmallText()
    {
        text.fontSize = 50;
    }

    [YarnCommand("Reset")]
    public void ResetTextSize()
    {
        text.fontSize = 65;
    }
}
