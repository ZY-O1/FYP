using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockAim : MonoBehaviour
{
    public Transform cam;

    void LateUpdate()
    {
        this.transform.rotation = cam.transform.rotation; // restore original rotation with new Y
    }

}