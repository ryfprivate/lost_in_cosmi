using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractee : MonoBehaviour
{
    const float G = 0.667408f;
    public Rigidbody2D rb;
    void FixedUpdate()
    {
        foreach (Attractor attractor in Attractor.Attractors)
        {
            if (attractor.name != this.name)
            {
                Attract(attractor);
            }
        }
    }

    void Attract(Attractor other)
    {
        Rigidbody2D rb2 = other.rb;
        Vector2 direction = other.transform.position - transform.position;
        float distance = direction.magnitude;
        float gravityRadius = (other.transform.localScale.x * other.gravityScale) / 2;

        if (distance == 0 || distance > gravityRadius)
        {
            return;
        }

        float mag = G * (rb.mass * rb2.mass) / Mathf.Pow(distance, 2);

        rb.AddForce(direction.normalized * mag);
    }

    void Rotate(Attractor other)
    {
        Vector2 lookDirection = other.transform.position - transform.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle + 90);
    }
}
