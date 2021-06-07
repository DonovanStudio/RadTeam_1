using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Exposed variables
    public float moveSpeed = 1.0f;
    public float jumpHeight = 1.0f;
    public float gravity = -1.0f;

    // Character Controller
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 playerMoveInput;
    private bool shouldJump = false;

    // Ability Flags
    private bool jumpUnlocked = false;

    private Transform tramsform;

    // Start is called before the first frame update
    void Start()
    {
        tramsform = this.transform;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if(controller.isGrounded)
        {
            playerVelocity.y = 0.0f;
        }

        playerVelocity.x = playerMoveInput.x * moveSpeed;
        playerVelocity.z = playerMoveInput.z * moveSpeed;

        playerVelocity = transform.TransformDirection(playerVelocity);

        if (jumpUnlocked && shouldJump && controller.isGrounded)
        {
            Debug.Log("Jump");
            playerVelocity.y += jumpHeight;
        }

        shouldJump = false;
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        Debug.Log("Play forward and back sound");
        playerMoveInput.z = value.Get<float>();
    }

    public void OnStrafe(InputValue value)
    {
        Debug.Log("Play left/right sound");
        playerMoveInput.x = value.Get<float>();
    }

    public void OnJump()
    {
        Debug.Log("Play jump sound");
        shouldJump = true;
    }

    // unlock abilities when collecting(colliding with) instruments
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Jump")
        {
            jumpUnlocked = true;
        }
    }
}
