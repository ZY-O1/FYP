using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleToBtn : MonoBehaviour
{
    bool pressed = true;
    void OnGUI()
    {
        pressed = GUILayout.Toggle(pressed, "Ready", "Button");
    }
}
