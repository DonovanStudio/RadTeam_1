using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterRotation : MonoBehaviour
{

    // This counter rotates the cube as it spins along with the cylinder
    void Update()
    {
        Vector3 parentRotation = transform.parent.rotation.eulerAngles;

        // Changed to zero Quaternion to maintain axes
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
