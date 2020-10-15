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

    // When the player starts charging the rocket
    public event Action<float> onRocketTriggerCharge;

    public void RocketTriggerCharge(float chargeDistance)
    {
        if (onRocketTriggerCharge != null)
        {
            onRocketTriggerCharge(chargeDistance);
        }
    }
}
