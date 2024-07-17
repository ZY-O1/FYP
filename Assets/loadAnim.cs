using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class loadAnim : MonoBehaviour
{
    public Slider loadA;
    // Update is called once per frame

    void Start()
    {
        loadA.maxValue = 30;
    }

    void Update()
    {
        if (loadA.value < 30)
        {
            loadA.value += Time.deltaTime;
        }

        if (loadA.value >= 30)
        {
            loadA.value = 0;
        }
    }
}
