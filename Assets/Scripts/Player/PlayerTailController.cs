using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTailController : MonoBehaviour
{
    public Rigidbody2D rb;
    void Start()
    {
        GameEvents.current.onTriggerLaunch += Launch;
    }

    void Launch(float thrust)
    {
        rb.AddForce(transform.up * thrust, ForceMode2D.Impulse);
    }
}
