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
    public float rotationDivider = 5.0f;
    public float minCameraAngle = -170f;
    public float maxCameraAngle = 170f;
    public float bobRadius = 1.0f;

    // Character Controller
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 playerMoveInput;
    private bool shouldJump = false;
    private float rotDividerRecip;

    // Ability Flags
    private bool jumpUnlocked = false;

    private Transform tramsform;

    private void Awake()
    {
        rotDividerRecip = 1 / rotationDivider;
    }

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

    public void OnLook(InputValue value)
    {
        Vector3 deltaRotation = new Vector3(0, value.Get<Vector2>().x, 0);
        deltaRotation *= rotDividerRecip;
        tramsform.Rotate(deltaRotation);

        Vector3 cameraRotation = Camera.main.transform.rotation.eulerAngles;
        cameraRotation.x -= value.Get<Vector2>().y * rotDividerRecip;
        cameraRotation.x = (cameraRotation.x + 180f) % 360f;
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, (minCameraAngle + 180), (maxCameraAngle + 180));
        cameraRotation.x -= 180f;
        Camera.main.transform.rotation = Quaternion.Euler(cameraRotation);
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
