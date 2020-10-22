﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Level level;
    public Transform cannon;
    public Animator death;
    private float deathAnimSpeed;

    void Start()
    {
        GameEvents.current.onDestinationCollision += Success;
        GameEvents.current.onObstacleCollision += Explode;
        GameEvents.current.onLeaveGameArea += Implode;

        transform.position = cannon.position;
        gameObject.SetActive(false);
        deathAnimSpeed = death.speed;
        death.speed = 0;
    }

    void Update()
    {
        if (transform.position.x > level.xMax || transform.position.x < level.xMin
        || transform.position.y > level.yMax || transform.position.y < level.yMin)
        {
            GameEvents.current.LeaveGameArea();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Destination")
        {
            Debug.Log("Reached goal");
            GameEvents.current.DestinationCollision(col.gameObject);
        }
        if (col.gameObject.tag == "Obstacle")
        {
            GameEvents.current.ObstacleCollision();
        }
    }

    void Success(GameObject dest)
    {
        Debug.LogFormat("SUCCESSS {0}", dest);
        GameEvents.current.onDestinationCollision -= Success;
    }

    void Implode()
    {
        Debug.Log("DEADDDD");
        death.speed = deathAnimSpeed;
        GameEvents.current.onLeaveGameArea -= Implode;
    }

    void Explode()
    {
        Debug.Log("BOOOM");
        GameEvents.current.onObstacleCollision -= Explode;
        GameEvents.current.onDestinationCollision -= Success;
    }
}
