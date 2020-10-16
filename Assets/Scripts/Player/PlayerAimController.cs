using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    public PlayerLaunchController pl;
    public Transform player;
    private bool rotating;
    void Start()
    {
        GameEvents.current.onTriggerAim += Rotate;

        rotating = false;
    }

    void Update()
    {
        if (rotating)
        {
            GameEvents.current.TriggerAim();

            if (Input.GetMouseButtonUp(0))
            {
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
        player.transform.rotation = rotation;
    }
}