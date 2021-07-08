using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class ExpressionsManager : MonoBehaviour
{
    /* Attached this script to an image of a character on the UI. */
    [SerializeField] string[] elementsLegend;
    public Sprite[] characterSprites; // 0 = Neutral, 1 = Happy, 2 = Angry, 3 = Additional Expressions
    Animator anim;
    Image currentExpression;

    private void Awake()
    {
        currentExpression = this.GetComponent<Image>();
        anim = this.GetComponent<Animator>();
    }

    [YarnCommand("Expression1")]
    public void SetExpression1()
    {
        currentExpression.sprite = characterSprites[0];
    }

    [YarnCommand("Expression2")]
    public void SetExpression2()
    {
        currentExpression.sprite = characterSprites[1];
    }

    [YarnCommand("Expression3")]
    public void SetExpression3()
    {
        currentExpression.sprite = characterSprites[2];
    }

    [YarnCommand("Expression4")]
    public void SetExpression4()
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
