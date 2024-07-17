//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class lookAt : MonoBehaviour
//{
//    // Update is called once per frame
//    void Update()
//    {
//        if (GameObject.FindGameObjectWithTag("Player").GetComponent<pickUp>().hasItem == true)
//        {
//            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
//            float midPoint = (transform.position - Camera.main.transform.position).magnitude * 5.0f;
//            transform.LookAt(mouseRay.origin + mouseRay.direction * midPoint);
//            transform.Rotate(90, 0, 0);
//        }
//    }
//}
