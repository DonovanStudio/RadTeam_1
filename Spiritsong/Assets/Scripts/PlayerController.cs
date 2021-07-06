using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Exposed variables
    public float moveSpeed = 1.0f;
    public float dashSpeed = 1.0f;
    public float jumpHeight = 1.0f;
    public float gravity = -1.0f;
    public float rotationDivider = 5.0f;
    public float minCameraAngle = -170f;
    public float maxCameraAngle = 170f;
    public float dashTime = 1.0f;

    // Character Controller
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 playerMoveInput;
    private bool shouldJump = false;
    //private bool shouldDash = true;
    private float rotDividerRecip;

    // Ability Flags
   
    private bool jumpUnlocked = false;
    private bool dashUnlocked = false;

    // Ability Variable Storage
    AbilityVariableStorage abilityVar;

    // Audio
    public GameObject backgroundMusic;
    FMOD.Studio.Bus MasterBus;

    private void Awake()
    {
        rotDividerRecip = 1 / rotationDivider;
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        abilityVar = FindObjectOfType<AbilityVariableStorage>();
        MasterBus = FMODUnity.RuntimeManager.GetBus("Bus:/");

        if (GameManager.instance.gameStarted)
        {
            transform.position = GameManager.instance.GetPosition();
            transform.rotation = GameManager.instance.GetRotation();
            jumpUnlocked = GameManager.instance.GetJump();
            dashUnlocked = GameManager.instance.GetDash();
        }
        else
        {
            GameManager.instance.gameStarted = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (controller.isGrounded)
        {
            playerVelocity.y = 0.0f;
        }

        playerVelocity.x = playerMoveInput.x * moveSpeed;
        playerVelocity.z = playerMoveInput.z * moveSpeed;

        playerVelocity = transform.TransformDirection(playerVelocity);

        // Jumping
        if (jumpUnlocked && shouldJump && controller.isGrounded)
        {
            Debug.Log("Jump");
            playerVelocity.y += jumpHeight;
            AudioManager.instance.SetJumpParameter(1.5f);
            StartCoroutine(JumpSoundReset());
        }

        shouldJump = false;
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private IEnumerator JumpSoundReset()
    {
        yield return new WaitForSeconds(1.5f);
        AudioManager.instance.SetJumpParameter(0f);
    }

    public void OnMove(InputValue value)
    {
        //Debug.Log("Play forward and back sound");
        playerMoveInput.z = value.Get<float>();
    }

    public void OnStrafe(InputValue value)
    {
        //Debug.Log("Play left/right sound");
        playerMoveInput.x = value.Get<float>();
    }

    public void OnJump()
    {
        if (jumpUnlocked)
        {
            //Debug.Log("Play jump sound");
            //AudioManager.instance.PlayJumpSound();
            shouldJump = true;
        }
    }

    public void OnDash()
    {
        if (dashUnlocked)
        {
            Debug.Log("Play Dash Sound");
            //AudioManager.instance.PlayDashSound();
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
            controller.Move(transform.forward * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnLook(InputValue value)
    {
        Vector3 deltaRotation = new Vector3(0, value.Get<Vector2>().x, 0);
        deltaRotation *= rotDividerRecip;
        transform.Rotate(deltaRotation);

        Vector3 cameraRotation = Camera.main.transform.rotation.eulerAngles;
        cameraRotation.x -= value.Get<Vector2>().y * rotDividerRecip;
        cameraRotation.x = (cameraRotation.x + 180f) % 360f;
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, (minCameraAngle + 180), (maxCameraAngle + 180));
        cameraRotation.x -= 180f;
        Camera.main.transform.rotation = Quaternion.Euler(cameraRotation);
    }

    public void OnOpenHub()
    {
        GameManager.instance.SavePlayerData(transform.position, transform.rotation, jumpUnlocked, dashUnlocked);
        Destroy(backgroundMusic);
        MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene("Hub");
    }

    // unlock abilities when collecting(colliding with) instruments
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Jump")
        {
            jumpUnlocked = true;
            abilityVar.jumpMechanic = true;
            AudioManager.instance.JumpUnlocked();
        }
        if (other.gameObject.tag == "Dash")
        {
            dashUnlocked = true;
            abilityVar.dashMechanic = true;
            AudioManager.instance.DashUnlocked();
        }
        if (other.gameObject.tag == "End")
        {
            Destroy(backgroundMusic);
            MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            SceneManager.LoadScene("EndScene");
        }
        if (other.gameObject.tag == "Ground")
        {
           
        }
    }
}