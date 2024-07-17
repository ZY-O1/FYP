using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FPSDisplay : MonoBehaviour
{
    private float updatePeriod = 5.0f;
    private float nextUpdate = 0.0f;
    private float framess = 0.0f;
    private float fps = 0.0f;

    // Update is called once per frame
    void Update()
    {
        framess++;
        if (Time.time > nextUpdate)
        {
            fps = Mathf.Round(framess / updatePeriod);
            GetComponent<Text>().text = "FPS " + fps;
            nextUpdate = Time.time + updatePeriod;
            framess = 0;
        }
    }
}
