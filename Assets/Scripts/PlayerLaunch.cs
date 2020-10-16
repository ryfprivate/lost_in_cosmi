using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaunch : MonoBehaviour
{
    const float maxChargeDist = 1f;
    const float maxThrust = 30f;
    public float thrust;
    public bool charging;
    private float holdDownStartTime;

    void Start()
    {
        thrust = 10f;
        charging = false;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.transform.name == "Tail")
                {
                    if (!charging)
                    {
                        charging = true;
                        holdDownStartTime = Time.time;
                    }
                }
            }
        }

        if (charging)
        {
            float holdDownTime = Time.time - holdDownStartTime;
            thrust = CalculateThrust(holdDownTime);
            GameEvents.current.TriggerCharge(thrust);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (charging)
            {
                GameEvents.current.TriggerLaunch(thrust);
            }
            charging = false;
        }
    }

    float CalculateThrust(float holdTime)
    {
        float maxHoldDownTime = 2f;
        float holdTimeNormalized = Mathf.Clamp01(holdTime / maxHoldDownTime);
        return holdTimeNormalized * maxThrust;
    }
}



// CameraMovement.locked = true;
// Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
// Vector2 difference = (Vector2)transform.position - currentMousePos;
// // Vector2 axis = (Vector2)Vector3.Project(new Vector3(difference.x, difference.y, 0), transform.up);
// // Debug.LogFormat("difference {0}, forward {1}, axis {2}", difference, transform.up, axis.magnitude);
// if (difference.x > 0 && difference.y > 0)
// {
//     float mag = Vector2.Distance(transform.position, currentMousePos);
//     if (mag <= maxChargeDist)
//     {
//         chargeDistance = Vector2.Distance(transform.position, currentMousePos);
//         GameEvents.current.RocketTriggerCharge(chargeDistance);
//         // chargeDistance = axis.magnitude;
//         thrust = chargeDistance * maxThrust;

//         Vector3 initialScale = sprite.localScale;
//         sprite.localScale = new Vector3(initialScale.x, chargeDistance, initialScale.z);
//     }
// }
