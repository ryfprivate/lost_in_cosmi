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
        int percentage = (int)(thrust / CannonLaunchController.maxThrust * 100);
        thrustText.text = percentage.ToString() + "%";
    }

    void onDestroy()
    {
        GameEvents.current.onTriggerCharge -= UpdateText;
    }
}
