using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    const float G = 0.0667408f;
    public static List<Attractor> Attractors;
    public Rigidbody2D rb;

    public Transform gravityField;
    public float gravityScale = 2f;

    void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if (gravityField)
        {
            gravityField.localScale = new Vector3(gravityScale, gravityScale, 1);
        }
    }

    void FixedUpdate()
    {
        foreach (Attractor attractor in Attractors)
        {
            if (attractor != this)
            {
                // Attract(attractor);
            }
        }
    }

    void OnEnable()
    {
        if (Attractors == null)
        {
            Attractors = new List<Attractor>();
        }

        Attractors.Add(this);
    }

    void OnDisable()
    {
        Attractors.Remove(this);
    }
}
