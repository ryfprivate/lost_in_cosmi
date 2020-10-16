using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextThrustController : MonoBehaviour
{
    public Text thrustText;
    void Start()
    {
        GameEvents.current.onTriggerCharge += UpdateText;
    }

    void UpdateText(float thrust)
    {
        thrustText.text = thrust.ToString();
    }

    void onDestroy()
    {
        GameEvents.current.onTriggerCharge -= UpdateText;
    }
}
