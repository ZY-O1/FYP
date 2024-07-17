using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectionCam : MonoBehaviour
{
    public Camera camera;
    public float horizontalFoV = 90.0f;

    void Update()
    {
        float halfWidth = Mathf.Tan(0.5f * horizontalFoV * Mathf.Deg2Rad);

        float halfHeight = halfWidth * Screen.height / Screen.width;

        float verticalFoV = 2.0f * Mathf.Atan(halfHeight) * Mathf.Rad2Deg;

        camera.fieldOfView = verticalFoV;
    }
}
