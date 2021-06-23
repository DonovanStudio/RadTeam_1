using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsPlayerController : MonoBehaviour
{
    // Exposed variables
    public float moveSpeed = 1.0f;
    public float dashSpeed = 1.0f;
    public float jumpHeight = 1.0f;
    public float dashTime = 1.0f;
    public float rotationSpeed = 1f;

    // Ability Use Check
    private bool shouldJump = false;
    private bool shouldDash = true;
    
    // Ability Flags
    private bool jumpUnlocked = false;
    private bool dashUnlocked = false;

    // Rigidbody
    private Rigidbody rb;
    private bool isGrounded = false;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Jumping
        if (jumpUnlocked && shouldJump && isGrounded)
        {
            Debug.Log("Jump");
        }

        shouldJump = false;
    }

    public void OnMove(InputValue value)
    {
        rb.AddForce(moveSpeed * Vector3.forward);
        Debug.Log("Play forward and back sound");
    }

    public void OnStrafe(InputValue value)
    {
        Debug.Log("Play left/right sound");
    }

    public void OnJump()
    {
        Debug.Log("Play jump sound");
        shouldJump = true;
    }

    public void OnDash()
    {
        Debug.Log("Play Dash Sound");
        if (dashUnlocked)
        {
            StartCoroutine(TimedDash());
        }
    }

    private IEnumerator TimedDash()
    {
        float start = Time.time;

        while (Time.time < start + dashTime)
        {
            Debug.Log("Looping");
            //transform.Translate(playerVelocity * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnLook(InputValue value)
    {
        rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(0f, rotationSpeed * value.Get<Vector2>().x, 0f));
        Debug.Log("Looking");
    }

    // unlock abilities when collecting(colliding with) instruments
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Jump")
        {
            jumpUnlocked = true;
        }
        if (other.gameObject.tag == "Dash")
        {
            dashUnlocked = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
