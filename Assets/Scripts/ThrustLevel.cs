using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrustLevel : MonoBehaviour
{
    public Text thrustText;
    public float thrustCounter;

    // Update is called once per frame
    void Update()
    {
        if (thrustText)
        {
            thrustText.text = thrustCounter.ToString();
        }
    }
}
