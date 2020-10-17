using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAimController : MonoBehaviour
{
    public CannonLaunchController pl;
    public Transform cannon;
    private bool rotating;
    void Start()
    {
        GameEvents.current.onStartAim += Rotate;

        rotating = false;
    }

    void Update()
    {
        if (rotating)
        {
            GameEvents.current.StartAim();

            if (Input.GetMouseButtonUp(0))
            {
                GameEvents.current.StopAim();
                rotating = false;
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                if (ClickedOnHead())
                {
                    if (!pl.charging)
                    {
                        rotating = true;
                    }
                }
            }
        }
    }

    bool ClickedOnHead()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.transform.name == "Head")
            {
                return true;
            }
        }
        return false;
    }

    void Rotate()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        cannon.transform.rotation = rotation;
    }
}