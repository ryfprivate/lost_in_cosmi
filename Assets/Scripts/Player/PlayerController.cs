using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    void Start()
    {
        GameEvents.current.onDestinationCollision += Success;
        GameEvents.current.onObstacleCollision += Explode;
        GameEvents.current.onLeaveGameArea += Implode;

        gameObject.SetActive(false);

        xMin = -10;
        xMax = 30;
        yMin = -10;
        yMax = 10;
    }

    void Update()
    {
        if (transform.position.x > xMax || transform.position.x < xMin || transform.position.y > yMax || transform.position.y < yMin)
        {
            GameEvents.current.LeaveGameArea();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Destination")
        {
            GameEvents.current.DestinationCollision();
        }
        if (col.gameObject.tag == "Obstacle")
        {
            GameEvents.current.ObstacleCollision();
        }
    }

    void Success()
    {
        Debug.Log("SUCCESSS");
    }

    void Implode()
    {
        Debug.Log("DEADDDD");
        GameEvents.current.onLeaveGameArea -= Implode;
    }

    void Explode()
    {
        Debug.Log("BOOOM");
        GameEvents.current.onObstacleCollision -= Explode;
    }
}
