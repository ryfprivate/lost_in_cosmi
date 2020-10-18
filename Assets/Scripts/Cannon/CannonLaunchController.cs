using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonLaunchController : MonoBehaviour
{
    public static float maxThrust = 40f;
    public GameObject cannon;
    public GameObject player;
    public Animator fire;
    public bool charging;
    private float thrust;
    private float holdDownStartTime;
    private float animSpeed;

    void Start()
    {
        GameEvents.current.onTriggerLaunch += DisableCannon;
        GameEvents.current.onTriggerLaunch += CalibratePlayer;
        GameEvents.current.onTriggerLaunch += Launch;

        animSpeed = 0.8f;
        fire.speed = 0f;
        thrust = 10f;
        charging = false;
    }

    void Update()
    {
        if (charging)
        {
            float holdDownTime = Time.time - holdDownStartTime;
            thrust = CalculateThrust(holdDownTime);
            GameEvents.current.TriggerCharge(thrust);

            if (Input.GetMouseButtonUp(0))
            {
                GameEvents.current.TriggerLaunch(thrust);
                charging = false;
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                if (ClickedOnEngine())
                {
                    charging = true;
                    fire.speed = animSpeed;
                    holdDownStartTime = Time.time;
                }
            }
        }
    }

    bool ClickedOnEngine()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.transform.name == "Tail")
            {
                return true;
            }
        }
        return false;
    }

    float CalculateThrust(float holdTime)
    {
        float maxHoldDownTime = 2f;
        float holdTimeNormalized = Mathf.Clamp01(holdTime / maxHoldDownTime);
        return holdTimeNormalized * maxThrust;
    }

    void DisableCannon(float thrust)
    {
        GameObject head = cannon.transform.Find("Head").gameObject;
        GameObject tail = cannon.transform.Find("Tail").gameObject;
        head.SetActive(false);
        tail.SetActive(false);
    }

    void CalibratePlayer(float thrust)
    {
        player.SetActive(true);
        Vector3 rotation = new Vector3(cannon.transform.eulerAngles.x, cannon.transform.eulerAngles.y, cannon.transform.eulerAngles.z);
        player.transform.eulerAngles = rotation;
    }

    void Launch(float thrust)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.AddForce(player.transform.up * thrust, ForceMode2D.Impulse);
    }
}
