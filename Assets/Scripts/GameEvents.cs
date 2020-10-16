using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    void Awake()
    {
        current = this;
    }

    // When the player holds on the engine (charge)
    public event Action<float> onTriggerCharge;

    public void TriggerCharge(float thrust)
    {
        if (onTriggerCharge != null)
        {
            onTriggerCharge(thrust);
        }
    }

    // When the player released after holding on the engine
    public event Action<float> onTriggerLaunch;
    public void TriggerLaunch(float thrust)
    {
        if (onTriggerLaunch != null)
        {
            onTriggerLaunch(thrust);
        }
    }
}
