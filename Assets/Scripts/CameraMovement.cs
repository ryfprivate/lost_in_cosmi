using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static bool locked;
    public static bool expanding;
    public Transform player;
    public float camBoundary;
    public float dragSpeed;
    public Camera cam;
    public float maxCamSize;
    private Vector3 dragOrigin;


    void Start()
    {
        locked = false;
        maxCamSize = 8;
        camBoundary = 21f;
        dragSpeed = 0.2f;
        expanding = false;
    }

    void Update()
    {
        if (expanding)
        {
            if (cam.orthographicSize > maxCamSize)
            {
                expanding = false;
            }
            else
            {
                cam.orthographicSize = cam.orthographicSize + 1 * Time.deltaTime;
            }
        }

        if (locked) return;

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

        float newX = transform.position.x - move.x;
        if (newX >= -8 && newX <= camBoundary)
        {
            transform.Translate(-move, Space.World);
        }
    }

    void checkBoundary()
    {

    }
}
