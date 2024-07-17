//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class destroyBullet : MonoBehaviour
//{
//    TPControl tc;
//    shoot s;
//    [SerializeField] public int desT;

//    // Start is called before the first frame update
//    void Start()
//    {
//        tc = GameObject.FindWithTag("Player").GetComponent<TPControl>();
//        //cd = GameObject.FindWithTag("Player").GetComponent<characterData>();
//    }



//    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
//    {
//        TPControl tc = other.GetComponent<TPControl>();
//        if (!tc && other.gameObject.tag != "Bullet")
//        {
//            shoot s = GameObject.FindWithTag("spawnB").GetComponent<shoot>();
//            s.reflect = true;
//            //Debug.Log(reflect);
//            Destroy(this.gameObject, desT);
//        }
//    }
//}
