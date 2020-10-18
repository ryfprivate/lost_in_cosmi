using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractee : MonoBehaviour
{
    public Rigidbody2D rb;
    void FixedUpdate()
    {
        if (Attractor.Attractors != null)
        {
            foreach (Attractor attractor in Attractor.Attractors)
            {
                if (attractor.name != this.name)
                {
                    Attract(attractor);
                }
            }
        }
    }

    void Attract(Attractor other)
    {
        Rigidbody2D bigRb = other.rb;
        Vector2 direction = other.transform.position - transform.position;
        float distance = direction.magnitude;
        float gravityRadius = (other.transform.localScale.x * other.gravityScale) / 2;

        if (distance > gravityRadius)
        {
            return;
        }
        // Debug.LogFormat("Attracting {0} to {1}", this, other);

        float mag = Attractor.G * (rb.mass * bigRb.mass) / Mathf.Pow(distance, 2);

        rb.AddForce(direction.normalized * mag);
    }
}
