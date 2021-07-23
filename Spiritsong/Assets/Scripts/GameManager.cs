using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector3 playerPositon = new Vector3();
    private Quaternion playerRotation = new Quaternion();
    private bool playerHasJump = false;
    private bool playerHasDash = false;
    private bool collectedOrb1 = false;
    private bool collectedOrb2 = false;
    private bool collectedOrb3 = false;

    private bool sceneIsLoading = false;

    public bool gameStarted = false;
    public GameObject violinSpirit;

    AbilityVariableStorage abilityVar;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        abilityVar = FindObjectOfType<AbilityVariableStorage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePlayerData(Vector3 pos, Quaternion rot, bool jump, bool dash)
    {
        playerPositon = pos;
        playerRotation = rot;
        playerHasJump = jump;
        playerHasDash = dash;
    }

    public Vector3 GetPosition()
    {
        return playerPositon;
    }

    public Quaternion GetRotation()
    {
        return playerRotation;
    }

    public bool GetJump()
    {
        return playerHasJump;
    }

    public bool GetDash()
    {
        return playerHasDash;
    }

    public bool GetOrb1Status()
    {
        return collectedOrb1;
    }

    public bool GetOrb2Status()
    {
        return collectedOrb2;
    }

    public bool GetOrb3Status()
    {
        return collectedOrb3;
    }

    public void Orb1Collected()
    {
        collectedOrb1 = true;
    }

    public void Orb2Collected()
    {
        collectedOrb2 = true;
    }

    public void Orb3Collected()
    {
        collectedOrb3 = true;
    }

    public void ResetGame()
    {
        playerPositon = Vector3.zero;
        playerRotation = Quaternion.identity;
        playerHasJump = false;
        playerHasDash = false;
        gameStarted = false;
        collectedOrb1 = false;
        collectedOrb2 = false;
        collectedOrb3 = false;

        abilityVar.jumpMechanic = false;
        abilityVar.dashMechanic = false;
    }

    public bool GetLoadingStatus()
    {
        return sceneIsLoading;
    }

    public void StartSceneLoading()
    {
        sceneIsLoading = true;
    }

    public void SceneFinishedLoading()
    {
        sceneIsLoading = false;
    }
}
