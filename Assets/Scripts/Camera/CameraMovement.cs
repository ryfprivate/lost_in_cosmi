using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 initialMousePosition;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    private bool movementLocked = false;

    void Start()
    {
        GameEvents.current.onStartAim += LockMovement;
        GameEvents.current.onStopAim += UnlockMovement;
    }

    void Update()
    {
        if (movementLocked) return;

        // Start of the touch
        if (Input.GetMouseButtonDown(0))
        {
            initialMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        // if (Input.touchCount == 2)
        // {
        //     Touch touchZero = Input.GetTouch(0);
        //     Touch touchOne = Input.GetTouch(1);

        //     Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;

        // }


        // During the touch
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = initialMousePosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void LockMovement()
    {
        movementLocked = true;
    }

    void UnlockMovement()
    {
        movementLocked = false;
    }

    void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}
