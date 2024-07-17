//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class camMove : MonoBehaviour
//{

//    float xAxisClamp;

//    [SerializeField]
//    public Transform camTarget;
//    public float positionSpeed = 0.02f;
//    public float rotationSpeed = 0.01f;

//    void Start()
//    {
//        Cursor.lockState = CursorLockMode.Locked;
//    }

//    void Update()
//    {
//        //RotateView();
//        transform.position = Vector3.Lerp(transform.position, camTarget.position, positionSpeed);
//        transform.rotation = Quaternion.Lerp(transform.rotation, camTarget.rotation, rotationSpeed);
//    }
//}


//void RotateView()
//{
//    float mouseX = Input.GetAxis("Mouse X");
//    float mouseY = Input.GetAxis("Mouse Y");

//    float rotAmountX = mouseX * rotationSpeed;
//    float rotAmountY = mouseY * rotationSpeed;

//    xAxisClamp -= rotAmountY;

//    Vector3 rotPlayerObj = playerObj.transform.rotation.eulerAngles;
//    Vector3 rotPlayer = cam.transform.rotation.eulerAngles;

//    rotPlayerObj.x -= rotAmountY;
//    //rotPlayerObj.x = 0;
//    //rotPlayerObj.z = 0;
//    //rotPlayerObj.x = 0;
//    //rotPlayerObj.y = 0;
//    rotPlayer.y += rotAmountX;
//    //rotPlayer.x -= rotAmountY;

//    playerObj.rotation = Quaternion.Euler(rotPlayerObj);
//    cam.rotation = Quaternion.Euler(rotPlayer);

//}
//}
