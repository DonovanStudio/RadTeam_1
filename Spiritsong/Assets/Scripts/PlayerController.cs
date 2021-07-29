using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public float coyoteTime = .5f;
    float countdown;
    [SerializeField] float upwardGravity = -16;
    [SerializeField] float downwardGravity = -9.81f;
    [SerializeField] float lookSensitivity = 1.0f;

    // Character Controller
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 playerMoveInput;
    private bool shouldJump = false;
    bool jumping;
    //private bool shouldDash = true;
    private float rotDividerRecip;

    // Ability Flags
    private bool jumpUnlocked = false;
    private bool dashUnlocked = false;

    // Ability Variable Storage
    AbilityVariableStorage abilityVar;

    //Level References
    [Header("Level Attributes")]
    public GameObject violin;
    [HideInInspector] public int orbs = 0;

    // Audio
    public GameObject backgroundMusic;
    FMOD.Studio.Bus MasterBus;
    public delegate void JumpAction();
    public static event JumpAction StartJump;
    public static event JumpAction EndJump;
    public delegate void DashAction();
    public static event DashAction StartDash;
    public static event DashAction EndDash;

    // UI
    [SerializeField] GameObject endPrompt;

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
            // You must be sure the character controller is disabled when to attempt to set the transform directly
            controller.enabled = false;
            transform.position = GameManager.instance.GetPosition();
            transform.rotation = GameManager.instance.GetRotation();
            jumpUnlocked = GameManager.instance.GetJump();
            dashUnlocked = GameManager.instance.GetDash();
            controller.enabled = true;
        }
        else
        {
            GameManager.instance.gameStarted = true;
        }

        GameManager.instance.SceneFinishedLoading();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        if (GameManager.instance.GetOrb1Status() && GameManager.instance.GetOrb2Status() && GameManager.instance.GetOrb3Status() && this.violin !=null)
        {
            this.violin.SetActive(true);
        }
    }

    private void HandleMovement()
    {
        if (controller.isGrounded)
        {
            playerVelocity.y = 0.0f;
            if (EndJump != null)
                EndJump();
        }

        playerVelocity.x = playerMoveInput.x * moveSpeed;
        playerVelocity.z = playerMoveInput.z * moveSpeed;

        playerVelocity = transform.TransformDirection(playerVelocity);

        // Jumping
        bool jumpable = controller.isGrounded || !jumping;
        if (jumpUnlocked && shouldJump && controller.isGrounded)
        {
            if (StartJump != null)
                StartJump();
            playerVelocity.y = jumpHeight;
            gravity = upwardGravity;
            jumping = true;
        }
        if (jumping && playerVelocity.y <= 0f)
        {
            jumping = false;
            gravity = downwardGravity;
        }
        shouldJump = false;
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        playerMoveInput.z = value.Get<float>();
    }

    public void OnStrafe(InputValue value)
    {
        playerMoveInput.x = value.Get<float>();
    }

    public void OnJump()
    {
        Terrain levelTerrain = Terrain.activeTerrain;
        TerrainData terrainData = levelTerrain.terrainData;
        float x = (transform.position.x - levelTerrain.transform.position.x) / terrainData.size.x;
        float z = (transform.position.z - levelTerrain.transform.position.z) / terrainData.size.z;
        float angle = terrainData.GetSteepness(x, z);
        
        if (jumpUnlocked && angle < 45f)
        {
            shouldJump = true;
        }
    }

    public void OnDash()
    {
        if (dashUnlocked)
        {
            if (StartDash != null)
                StartDash();
            StartCoroutine(TimedDash());
        }
    }

    private IEnumerator TimedDash()
    {
        float start = Time.time;

        while (Time.time < start + dashTime)
        {
            controller.Move(transform.forward * dashSpeed * Time.deltaTime);
            yield return null;
        }

        if (EndDash != null)
            EndDash();
    }

    public void OnLook(InputValue value)
    {
        Vector3 deltaRotation = new Vector3(0, value.Get<Vector2>().x, 0);
        deltaRotation *= rotDividerRecip;
        transform.Rotate(deltaRotation * lookSensitivity);

        Vector3 cameraRotation = Camera.main.transform.rotation.eulerAngles;
        cameraRotation.x -= value.Get<Vector2>().y * rotDividerRecip;
        cameraRotation.x = (cameraRotation.x + 180f) % 360f;
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, (minCameraAngle + 180), (maxCameraAngle + 180));
        cameraRotation.x -= 180f;
        Camera.main.transform.rotation = Quaternion.Euler(cameraRotation);
    }

    public void OnOpenHub()
    {
        if (!GameManager.instance.GetLoadingStatus())
        {
            GameManager.instance.StartSceneLoading();
            GameManager.instance.SavePlayerData(transform.position, transform.rotation, jumpUnlocked, dashUnlocked);
            Destroy(backgroundMusic);
            MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            SceneManager.LoadScene(1);
        }
    }

    // unlock abilities when collecting(colliding with) instruments
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Jump")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Violin Spirit Pickup");
            jumpUnlocked = true;
            abilityVar.jumpMechanic = true;
        }
        if (other.gameObject.tag == "Dash")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Flute Spirit Pickup");
            dashUnlocked = true;
            abilityVar.dashMechanic = true;
        }
        if (other.gameObject.tag == "End")
        {
            endPrompt.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
        }
        if (other.gameObject.tag == "orb")
        {
            if (other.gameObject.name == "small spirit 1")
            {
                GameManager.instance.Orb1Collected();
            }
            if (other.gameObject.name == "small spirit 2")
            {
                GameManager.instance.Orb2Collected();
            }
            if (other.gameObject.name == "small spirit 3")
            {
                GameManager.instance.Orb3Collected();
            }

            CollectOrb(this);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Small Spirit Pickup");
            Destroy(other.gameObject);
        }
    }

    public static void CollectOrb(PlayerController player)
    {
        player.orbs++;
        if (player.orbs >= 3)
            player.violin.SetActive(true);
    }

    public void CloseEndPrompt()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Menu SFX");
        endPrompt.SetActive(false);
    }

    public void GoToEndScene()
    {
        GameManager.instance.ResetGame();
        Destroy(backgroundMusic);
        MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Menu SFX");
        SceneManager.LoadScene(3);
    }
}