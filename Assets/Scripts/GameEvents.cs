using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    void Awake()
    {
        current = this;
    }

    // When the level starts
    public event Action onLevelStart;
    public void LevelStart()
    {
        if (onLevelStart != null)
        {
            onLevelStart();
        }
    }

    // When the player holds on the engine (charge)
    public event Action<float> onTriggerCharge;

    public void TriggerCharge(float thrust)
    {
        if (onTriggerCharge != null)
        {
            onTriggerCharge(thrust);
        }
    }

    // When the player releases after holding on the engine
    public event Action<float> onTriggerLaunch;
    public void TriggerLaunch(float thrust)
    {
        if (onTriggerLaunch != null)
        {
            onTriggerLaunch(thrust);
        }
    }

    // When the player holds on the head (starts aiming)
    public event Action onStartAim;
    public void StartAim()
    {
        if (onStartAim != null)
        {
            onStartAim();
        }
    }

    // When the player lets go of the head (stops aiming)
    public event Action onStopAim;
    public void StopAim()
    {
        if (onStopAim != null)
        {
            onStopAim();
        }
    }

    // When the player collides with an obstacle
    public event Action onObstacleCollision;
    public void ObstacleCollision()
    {
        if (onObstacleCollision != null)
        {
            onObstacleCollision();
        }
    }

    // When the player collides with the destination
    public event Action<GameObject> onDestinationCollision;
    public void DestinationCollision(GameObject dest)
    {
        if (onDestinationCollision != null)
        {
            onDestinationCollision(dest);
        }
    }

    // When the player leaves the game area (reaches the boundaries)
    public event Action onLeaveGameArea;
    public void LeaveGameArea()
    {
        if (onLeaveGameArea != null)
        {
            onLeaveGameArea();
        }
    }
}
