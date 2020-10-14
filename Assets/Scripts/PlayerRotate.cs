using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public PlayerLaunch pl;
    public Transform obj;
    private bool rotating;

    void Start()
    {
        rotating = false;
    }

    void Update()
    {
        if (rotating)
        {
            CameraMovement.locked = true;
            RotatePlayer();
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.transform.name == "Head")
                {
                    if (!pl.charging)
                    {
                        rotating = true;
                    }
                }
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            CameraMovement.locked = false;
            rotating = false;
        }
    }

    void RotatePlayer()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        obj.transform.rotation = rotation;
    }
}
