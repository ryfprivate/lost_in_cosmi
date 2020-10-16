using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaunchController : MonoBehaviour
{
    const float maxChargeDist = 1f;
    const float maxThrust = 30f;
    public Rigidbody2D rb;
    public float thrust;
    public bool charging;
    private float holdDownStartTime;

    void Start()
    {
        GameEvents.current.onTriggerLaunch += Launch;

        thrust = 10f;
        charging = false;
    }

    void Update()
    {
        if (charging)
        {
            float holdDownTime = Time.time - holdDownStartTime;
            thrust = CalculateThrust(holdDownTime);
            GameEvents.current.TriggerCharge(thrust);

            if (Input.GetMouseButtonUp(0))
            {
                GameEvents.current.TriggerLaunch(thrust);
                charging = false;
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                if (ClickedOnEngine())
                {
                    charging = true;
                    holdDownStartTime = Time.time;
                }
            }
        }
    }

    bool ClickedOnEngine()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.transform.name == "Tail")
            {
                return true;
            }
        }
        return false;
    }

    float CalculateThrust(float holdTime)
    {
        float maxHoldDownTime = 2f;
        float holdTimeNormalized = Mathf.Clamp01(holdTime / maxHoldDownTime);
        return holdTimeNormalized * maxThrust;
    }

    void Launch(float thrust)
    {
        rb.AddForce(transform.up * thrust, ForceMode2D.Impulse);
    }
}
