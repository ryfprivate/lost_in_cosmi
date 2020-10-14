using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public float camBoundary = 21f;
    public float dragSpeed;
    private Vector3 dragOrigin;

    void Start()
    {
        dragSpeed = 0.2f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

        float newX = transform.position.x - move.x;
        if (newX >= 0 && newX <= camBoundary)
        {
            transform.Translate(-move, Space.World);
        }
    }
}
