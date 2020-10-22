using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Destination")
        {
            Debug.Log("Reached goal body part");
            GameEvents.current.DestinationCollision(col.gameObject);
        }
    }
}
