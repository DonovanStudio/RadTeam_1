using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class AbilityVariableStorage : MonoBehaviour
{
    public bool walkMechanic;
    public bool jumpMechanic;
    public bool dashMechanic;
    public bool[] hintsAvailable;

    private static AbilityVariableStorage instance;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        walkMechanic = true;
    }
}
