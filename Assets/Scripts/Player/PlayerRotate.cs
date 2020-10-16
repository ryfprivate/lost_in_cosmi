using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public PlayerLaunch pl;
    private bool rotating;

    void Start()
    {
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
}
