using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float jumpHeight = 1.0f;
    public float gravity = -1.0f;
    public float rotationDivider = 5.0f;
    public float minCameraAngle = -170f;
    public float maxCameraAngle = 170f;
    public float bobRadius = 1.0f;

    // Movement Input Values
    private float playerMoveInput;
    private float playerStrafeInput;
    private bool shouldJump = false;
    private float rotDividerRecip;

    private Transform tramsform;
    private Rigidbody rigidbody;

    private void Awake()
    {
        rotDividerRecip = 1 / rotationDivider;
    }

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
