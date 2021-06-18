using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class ExpressionsManager : MonoBehaviour
{
    /* Attached this script to an image of a character on the UI. */

    public Sprite[] characterSprites; // 0 = Neutral, 1 = Happy, 2 = Angry, 3 = Sad, etc. (add more as we come up with them)
    Animator anim;
    Image currentExpression;

    private void Awake()
    {
        currentExpression = this.GetComponent<Image>();
        anim = this.GetComponent<Animator>();
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

    [YarnCommand("turnL")]
    public void TurnLeft()
    {
        anim.SetBool("Turn Right", false);
        anim.SetBool("Facing Right", false);
        anim.SetBool("Turn Left", true);
        anim.SetBool("Hopping", false);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    [YarnCommand("turnR")]
    public void TurnRight()
    {
        anim.SetBool("Turn Right", true);
        anim.SetBool("Facing Right", true);
        anim.SetBool("Turn Left", false);
        anim.SetBool("Hopping", false);
        transform.rotation = new Quaternion (0, 180, 0, 0);
    }

    [YarnCommand("hop")]
    public void Hop()
    {
        anim.SetBool("Turn Right", false);
        anim.SetBool("Turn Left", false);
        anim.SetBool("Hopping", true);
    }

    public void PauseClip()
    {
        anim.SetBool("Turn Right", false);
        anim.SetBool("Turn Left", false);
        anim.SetBool("Hopping", false);
    }
}
