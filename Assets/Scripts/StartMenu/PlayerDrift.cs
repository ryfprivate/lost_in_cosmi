using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrift : MonoBehaviour
{
    public Rigidbody2D rb;
    private float speed = 10f;
    void Start()
    {
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }
}
