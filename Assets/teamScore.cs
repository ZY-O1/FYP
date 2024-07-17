using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class teamScore : MonoBehaviour
{

    public Text teamHiderT;
    public Text teamHunterT;
    result r;

    void Start()
    {
       r = GameObject.Find("Result").GetComponent<result>();
    }

    void Update()
    {
        if (MenuManager.PvE)
        {
            teamHiderT.text = r.teamHider.ToString();
            teamHunterT.text = r.teamHunter.ToString();
        }
    }
}
