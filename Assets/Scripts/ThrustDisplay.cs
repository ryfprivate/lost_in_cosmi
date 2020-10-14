using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrustDisplay : MonoBehaviour
{
    public Text thrustText;
    public PlayerLaunch pl;

    // Update is called once per frame
    void Update()
    {
        if (thrustText)
        {
            // Debug.Log(pl.chargeDistance);
            thrustText.text = pl.chargeDistance.ToString();
        }
    }
}
