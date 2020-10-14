using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaunch : MonoBehaviour
{
    public Rigidbody2D rb;
    public float chargeDistance;
    public float thrust;
    private bool charging;

    void Start()
    {
        chargeDistance = 0;
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
                    charging = true;
                }
            }
        }

        if (charging)
        {
            Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Debug.LogFormat("position: {0}", transform.position);
            chargeDistance = Vector2.Distance(transform.position, currentMousePos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (charging)
            {
                Launch();
            }

            charging = false;
        }
    }

    public void Launch()
    {
        rb.AddForce(transform.up * thrust, ForceMode2D.Impulse);
    }
}
