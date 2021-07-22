using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector3 playerPositon = new Vector3();
    private Quaternion playerRotation = new Quaternion();
    private bool playerHasJump = false;
    private bool playerHasDash = false;

    public bool gameStarted = false;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void ResetGame()
    {
        playerPositon = Vector3.zero;
        playerRotation = Quaternion.identity;
        playerHasJump = false;
        playerHasDash = false;
        gameStarted = false;
    }
}
