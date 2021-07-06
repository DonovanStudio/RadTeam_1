using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class HintsManager : MonoBehaviour
{
    AbilityVariableStorage abilityVar;

    // Start is called before the first frame update
    void Start()
    {
        abilityVar = FindObjectOfType<AbilityVariableStorage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.name == "Hint0Trigger")
        {
            abilityVar.hintsAvailable[0] = true;
        }

        if (this.gameObject.name == "Hint1Trigger")
        {
            abilityVar.hintsAvailable[1] = true;
        }

        if (this.gameObject.name == "Hint2Trigger")
        {
            abilityVar.hintsAvailable[2] = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (this.gameObject.name == "Hint0Trigger")
        {
            abilityVar.hintsAvailable[0] = false;
        }

        if (this.gameObject.name == "Hint1Trigger")
        {
            abilityVar.hintsAvailable[1] = false;
        }

        if (this.gameObject.name == "Hint2Trigger")
        {
            abilityVar.hintsAvailable[2] = false;
        }
    }
}
