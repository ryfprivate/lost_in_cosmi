using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Level level;
    private float zoomOutMin = 1;
    private float zoomOutMax = 8;
    private Vector3 initialMousePosition;
    private bool movementLocked = false;
    private float camHeight;
    private float camWidth;

    void Start()
    {
        GameEvents.current.onStartAim += LockMovement;
        GameEvents.current.onStopAim += UnlockMovement;

        camHeight = 2f * Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    void Update()
    {
        if (movementLocked) return;

        // Start of the touch
        if (Input.GetMouseButtonDown(0))
        {
            initialMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = initialMousePosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float newX, newY;
            if (level)
            {
                float xMin = level.xMin + camWidth / 2;
                float yMin = level.yMin + camHeight / 2;
                float xMax = level.xMax - camWidth / 2;
                float yMax = level.yMax - camHeight / 2;

                newX = Mathf.Clamp(Camera.main.transform.position.x + direction.x, xMin, xMax);
                newY = Mathf.Clamp(Camera.main.transform.position.y + direction.y, yMin, yMax);
            }
            else
            {
                newX = Camera.main.transform.position.x + direction.x;
                newY = Camera.main.transform.position.y + direction.y;
            }

            Camera.main.transform.position = new Vector3(newX, newY, Camera.main.transform.position.z);
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
