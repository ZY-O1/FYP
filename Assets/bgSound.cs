//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Photon.Pun;
//using Photon.Realtime;

//public class bgSound : MonoBehaviourPunCallbacks
//{
//    public AudioSource audioS;
//    public AudioClip whackSound;
//    PhotonView view;

//    // Update is called once per frame
//    void Update()
//    {
//        AudioSource audioS = GetComponent<AudioSource>();
//        if (GameObject.Find("PlayerX").GetComponent<TPControl>().whacked)
//        {
//            audioS.PlayOneShot(whackSound);
//            audioS.pitch = 1.0f;
//            audioS.loop = false;
//            GameObject.Find("PlayerX").GetComponent<TPControl>().whacked = false;
//        }
//    }
//}
