using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteAnimationManager : MonoBehaviour
{
    bool isFacingRight;
    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        isFacingRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Set the animation parameter = isFacingRight
        animator.SetBool("Facing Right", isFacingRight);
    }

    public void Strafe(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isFacingRight =! isFacingRight; // Toggle direction
        }
    }
}
