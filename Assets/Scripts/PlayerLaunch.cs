using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaunch : MonoBehaviour
{
    const float maxChargeDist = 1f;
    const float maxThrust = 30f;
    public Rigidbody2D rb;
    public Transform sprite;
    public float chargeDistance;
    public float thrust;
    public bool charging;

    void Start()
    {
        chargeDistance = 0;
        thrust = 10f;
        charging = false;
        Vector3 initialScale = sprite.localScale;
        sprite.localScale = new Vector3(initialScale.x, 0, initialScale.z);
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
                    charging = true;
                }
            }
        }

        if (charging)
        {
            CameraMovement.locked = true;
            Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 difference = (Vector2)transform.position - currentMousePos;
            // Vector2 axis = (Vector2)Vector3.Project(new Vector3(difference.x, difference.y, 0), transform.up);
            // Debug.LogFormat("difference {0}, forward {1}, axis {2}", difference, transform.up, axis.magnitude);
            if (difference.x > 0 && difference.y > 0)
            {
                float mag = Vector2.Distance(transform.position, currentMousePos);
                if (mag <= maxChargeDist)
                {
                    chargeDistance = Vector2.Distance(transform.position, currentMousePos);
                    // chargeDistance = axis.magnitude;
                    thrust = chargeDistance * maxThrust;

                    Vector3 initialScale = sprite.localScale;
                    sprite.localScale = new Vector3(initialScale.x, chargeDistance, initialScale.z);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (charging)
            {
                Launch();
            }

            CameraMovement.locked = false;
            charging = false;
        }
    }

    public void Launch()
    {
        Debug.LogFormat("force: {0}", thrust);
        rb.AddForce(transform.up * thrust, ForceMode2D.Impulse);
    }
}
