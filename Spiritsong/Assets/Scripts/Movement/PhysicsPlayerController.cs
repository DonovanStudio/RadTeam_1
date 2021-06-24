using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsPlayerController : MonoBehaviour
{
    // Exposed variables
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float dashSpeed = 1.0f;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float dashTime = 1.0f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float floorSensitivity = 1f;
    
    // Ability Use Check
    private bool shouldJump = false;
    private bool shouldDash = false;
    
    // Ability Flags
    private bool jumpUnlocked = false;
    private bool dashUnlocked = false;

    // Rigidbody
    private Rigidbody rb;
    private bool isGrounded = false;
    float direction = 0;

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
        //Do a dash
        if (shouldDash)
        {
            moveMe(direction * dashSpeed);
            shouldDash = false;
        }
        // Jumping
        RaycastHit hit;
        isGrounded = Physics.SphereCast(transform.position, floorSensitivity, Vector3.forward, out hit, 1000, 3);
        Debug.Log("Can jump is " + isGrounded);
        if (jumpUnlocked && shouldJump && isGrounded)
        {
            rb.AddForce(jumpHeight * Vector3.up);
            Debug.Log("Jump");
        }

        shouldJump = false;

        //walking
        moveMe(direction);
    }

    public void OnMove(InputValue value)
    {
        //getting the value of the input (-1 or 1)
        //backwards because the camera is on wrong
        direction = -value.Get<float>();
    }
    private void moveMe(float direction)
    {
        Vector3 angleFacing = -Vector3.Cross(transform.up, Vector3.up);
        rb.AddForce(direction * moveSpeed * angleFacing);
        Debug.DrawRay(transform.position, angleFacing, Color.red, 10f);
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
        shouldDash = true;
    }

    //private IEnumerator TimedDash()
    //{
    //    float start = Time.time;

    //    while (Time.time < start + dashTime)
    //    {
    //        Debug.Log("Looping");
    //        //transform.Translate(playerVelocity * dashSpeed * Time.deltaTime);
    //        yield return null;
    //    }
    //}

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

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        isGrounded = true;
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        isGrounded = false;
    //    }
    //}
}
