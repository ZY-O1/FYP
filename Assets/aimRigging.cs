//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Animations.Rigging;
//using Photon.Pun;
//using Photon.Realtime;

//public class aimRigging : MonoBehaviourPunCallbacks
//{
//    PhotonView view;
//    [SerializeField] private Rig aimRig;
//    private float aimRigWeight;
//    characterData cd;

//    void Start()
//    {
//        view = GameObject.FindWithTag("Player").GetComponent<PhotonView>();
//        cd = GameObject.FindWithTag("Player").GetComponent<characterData>();
//    }

//    private void Update()
//    {
//        if (!view.IsMine)
//        {
//            return;
//        }

//        if (view.IsMine)
//        {
//            if (cd.hp <= 0)
//            {
//                aimStop();
//            }

//            if (Input.GetMouseButton(0) && cd.teamNum == 2)
//            {
//                aimStart();
//            }
//            else
//            {
//                aimStop();
//            }
//            aimRig.weight = Mathf.Lerp(aimRig.weight, aimRigWeight, Time.deltaTime * 20f);
//        }
//    }

//    private void aimStart()
//    {
//        aimRigWeight = 1f;
//    }

//    private void aimStop()
//    {
//        aimRigWeight = 0f;
//    }
//}
