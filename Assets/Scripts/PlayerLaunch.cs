using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaunch : MonoBehaviour
{
    public float thrust = 10f;
    public Rigidbody2D rb;
    public float chargeDistance = 0;
    private bool charging = false;

    public void Launch()
    {
        rb.AddForce(transform.up * thrust, ForceMode2D.Impulse);
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
}
