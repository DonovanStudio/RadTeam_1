using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPump_EditorOnly : MonoBehaviour
{
    //To be used in editor to test the impact of forces on the player.
    [Header("Select the following to push the wheel")]
    [SerializeField] bool smallGust, largeGust;
    [SerializeField] float[] gustSizes = { 5f, 15f };
    [SerializeField] bool gustAutoTurnOff = true;
    //Storing myself
    Rigidbody rigidbody1;

    private void Awake()
    {
        rigidbody1 = gameObject.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (smallGust)
        {
            rigidbody1.AddForce(gustSizes[0] * Vector3.left);
            turnOffGust(smallGust);
        }
        if (largeGust)
        {
            rigidbody1.AddForce(gustSizes[1] * Vector3.left);
            turnOffGust(largeGust);
        }
    }
    
    /// <summary>
    /// Turns off a gust after being pressed
    /// </summary>
    /// <param name="gust"></param>
    void turnOffGust(bool gust)
    {
        if (gustAutoTurnOff)
            gust = false;
    }
}
