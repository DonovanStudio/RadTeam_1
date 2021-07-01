using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityVariableStorage : MonoBehaviour
{
    public bool walkMechanic;
    public bool jumpMechanic;
    public bool dashMechanic;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
