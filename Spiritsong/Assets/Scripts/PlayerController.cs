using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float jumpForce = 1.0f;

    // Movement Input Values
    private float playerMoveInput;
    private float playerStrafeInput;
    private bool shouldJump = false;

    private Transform tramsform;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        tramsform = this.transform;
        rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Position based movement
        tramsform.position += Vector3.forward * playerMoveInput * moveSpeed;
        transform.position += Vector3.right * playerStrafeInput * moveSpeed;

        if (shouldJump)
        {
            Debug.Log("Jump");
            rigidbody.AddForce(Vector3.up * jumpForce);
            shouldJump = false;
        }
    }

    public void OnMove(InputValue value)
    {
        playerMoveInput = value.Get<float>();
    }

    public void OnStrafe(InputValue value)
    {
        playerStrafeInput = value.Get<float>();
    }

    public void OnJump()
    {
        shouldJump = true;
    }
}
