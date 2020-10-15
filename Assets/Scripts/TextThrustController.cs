using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextThrustController : MonoBehaviour
{
    public Text thrustText;
    void Start()
    {
        GameEvents.current.onRocketTriggerCharge += UpdateText;
    }

    void UpdateText(float chargeDistance)
    {
        thrustText.text = chargeDistance.ToString();
    }
}
