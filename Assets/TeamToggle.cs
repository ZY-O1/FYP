using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class TeamToggle : MonoBehaviourPunCallbacks
{
    public Toggle hunter;
    public Toggle hider;

    void Start()
    {
        hider.isOn = true;
        hunter.isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hider.isOn)
        {
            SwapTeam.team = 1;
            //Debug.Log(SwapTeam.team);
        }
        else if (hunter.isOn)
        {
            SwapTeam.team = 2;
            //Debug.Log(SwapTeam.team);
        }

        if (!hider.isOn)
        {
            hunter.isOn = true;
        }
        else if (!hunter.isOn)
        {
            hider.isOn = true;
        }
    }
}