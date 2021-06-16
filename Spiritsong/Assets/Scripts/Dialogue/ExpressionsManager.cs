using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class ExpressionsManager : MonoBehaviour
{
    /* Attached this script to an image of a character on the UI. */

    public Sprite[] characterSprites; // 0 = Neutral, 1 = Happy, 2 = Angry, 3 = Sad, etc. (add more as we come up with them)
    Image currentExpression;

    private void Awake()
    {
        currentExpression = this.GetComponent<Image>();
    }

    [YarnCommand("neutral")]
    public void SetExpressionNeutral()
    {
        currentExpression.sprite = characterSprites[0];
    }

    [YarnCommand("happy")]
    public void SetExpressionHappy()
    {
        currentExpression.sprite = characterSprites[1];
    }

    [YarnCommand("angry")]
    public void SetExpressionAngry()
    {
        currentExpression.sprite = characterSprites[2];
    }

    [YarnCommand("sad")]
    public void SetExpressionSad()
    {
        currentExpression.sprite = characterSprites[3];
    }
}
