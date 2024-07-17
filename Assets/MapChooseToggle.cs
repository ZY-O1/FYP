using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class MapChooseToggle : MonoBehaviourPunCallbacks
{
    public Toggle Factory;
    public Toggle Lab;
    public Toggle SnowyLand;

    public GameObject fac;
    public GameObject lab;
    public GameObject snow;
    public static int map;

    void Start()
    {
      Factory.isOn = true;
      Lab.isOn = false;
      SnowyLand.isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Factory.isOn)
        {
            map = 1;
            fac.SetActive(true);
            lab.SetActive(false);
            snow.SetActive(false);
        }
        else if (Lab.isOn)
        {
            map = 2;
            fac.SetActive(false);
            lab.SetActive(true);
            snow.SetActive(false);
        }
        else if (SnowyLand.isOn)
        {
            map = 3;
            fac.SetActive(false);
            lab.SetActive(false);
            snow.SetActive(true);
        }
        else
        {
            map = 0;
        }
     }
}
