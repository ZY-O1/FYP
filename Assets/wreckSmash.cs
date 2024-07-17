using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wreckSmash : MonoBehaviour
{
    public static float spinTimer = 30f;
    public Transform wreck;
    public float spinForce;


    // Update is called once per frame
    void Update()
    {
        if (spinTimer > 0)
        {
            spinTimer -= Time.deltaTime;
        }
        else
        {
            spinTimer = 30f;
        }

        if (spinTimer >= 20 && spinTimer <= 30)
        {
            wreck.Rotate(0, 15 * Time.deltaTime, 0);
        }
        else if (spinTimer >= 10 && spinTimer <= 20)
        {
            wreck.Rotate(0, -15 * Time.deltaTime, 0);
        }
        else
        {
            wreck.Rotate(0, spinForce * Time.deltaTime, 0);
        }
    }
}
