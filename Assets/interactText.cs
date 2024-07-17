using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interactText : MonoBehaviour
{
    public static bool textF= false;
    public static string messageF;
    private float timerF = 0.0f;
    Text interact;

    // Start is called before the first frame update
    void Start()
    {
        interact = GetComponent<Text>();
        timerF = 0.0f;
        textF = false;
        interact.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (textF)
        {
            interact.enabled = true;
            interact.text = messageF;
            timerF += Time.deltaTime;
        }

        if (timerF >= 1)
        {
            textF = false;
            interact.enabled = false;
            timerF = 0.0f;
        }
    }
}