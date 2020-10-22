using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform parent;
    private float orbitSpeed;

    void Start()
    {
        orbitSpeed = 10.0f;
    }

    void Update()
    {
        transform.position = RotatePointAroundPivot(transform.position,
                           parent.position,
                           Quaternion.Euler(0, 0, orbitSpeed * Time.deltaTime));
    }

    Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle)
    {
        return angle * (point - pivot) + pivot;
    }
}
