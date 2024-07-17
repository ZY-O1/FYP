using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class healthDisplay : MonoBehaviourPunCallbacks
{
     PhotonView view;
   // public Transform target;
    //public bool inRange;
    //public GameObject tank;

    private int hpShow;
    Slider health;
    //characterData cd;

    void Start()
    {
        view = GameObject.FindWithTag("Player").GetComponent<PhotonView>();
        health = this.GetComponent<Slider>();
        //rsp = GameObject.FindWithTag("spawn").GetComponent<randomSpawnPoint>();
        //cd = GameObject.FindWithTag("Player").GetComponent<characterData>();
    }

    // Update is called once per frame
    void Update()
    {
        health.value = hpShow;
        this.GetComponentInChildren<Text>().text = "HP " + (int)health.value;
    }

    public void resetHP(int resetHP)
    {
        hpShow = resetHP;
        health.value = hpShow;
        health.maxValue = hpShow;
    }

    public void takeDmg(int dmg)
    {
        hpShow -= dmg;
        health.value = hpShow;
    }

    public void restoreHP(int dmg)
    {
        hpShow += dmg;
        health.value = hpShow;
    }
}

