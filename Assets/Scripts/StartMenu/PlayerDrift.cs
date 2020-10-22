using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrift : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator death;
    private float speed = 10f;

    void Start()
    {
        death.speed = 0;
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }
}
