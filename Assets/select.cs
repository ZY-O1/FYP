using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class select : MonoBehaviour
{
    private Renderer renderer;
    //public GameObject go;
    public static bool turnRed;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (turnRed)
        {
            this.renderer.material.color = Color.red;
            interactText.messageF = "F to Set Trap";
            interactText.textF = true;
        }
        else
        {
            this.renderer.material.color = Color.white;
        }
    }
}
