using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextThrustController : MonoBehaviour
{
    public Text thrustText;
    private Vector3 startPos;
    private Vector3 target;
    private float t;
    private float animTime;
    private bool charging;

    void Start()
    {
        GameEvents.current.onTriggerCharge += UpdateText;
        GameEvents.current.onTriggerCharge += ShiftText;

        charging = false;
        t = 0;
        animTime = 0.66f;
    }

    void UpdateText(float thrust)
    {
        int percentage = (int)(thrust / CannonLaunchController.maxThrust * 100);
        thrustText.text = percentage.ToString() + "%";
    }

    void ShiftText(float thrust)
    {
        if (!charging)
        {
            startPos = transform.position;
            target = startPos - 0.21f * transform.up;
            charging = true;
        }

        t += Time.deltaTime / animTime;

        transform.position = Vector3.Lerp(startPos, target, t);
    }

    void onDestroy()
    {
        GameEvents.current.onTriggerCharge -= UpdateText;
    }
}
