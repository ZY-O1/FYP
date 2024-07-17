//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class camRotate : MonoBehaviour
//{
//    public Vector2 turn;
//    public float sensitivity = 0.5f;

//    void Start()
//    {
//        Cursor.lockState = CursorLockMode.Locked; 
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        turn.x += Input.GetAxis("Mouse X") * sensitivity;
//        turn.y += Input.GetAxis("Mouse Y") * sensitivity;

//        if (turn.y < -90)
//        {
//            turn.y = -90;
//        }
//        else if (turn.y > 90)
//        {
//            turn.y = 90;
//        }


//        transform.localRotation = Quaternion.Euler(-turn.y, -turn.x, 0);
//    }
//}
