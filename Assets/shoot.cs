//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Audio;
//using Photon.Pun;
//using Photon.Realtime;

//[RequireComponent(typeof(AudioSource))]
//public class shoot : MonoBehaviourPunCallbacks
//{
//    public AudioClip shootSound;
//    public bool autoShoot;
//    public int bulletNum;
//    public AudioMixerGroup mast;
//    public Transform gunTransform;

//    PhotonView view;

//    [SerializeField] public bool reflect;

//    void Start()
//    {
//        bulletNum = 1;
//        view = GameObject.FindWithTag("Player").GetComponent<PhotonView>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (view.IsMine)
//        {
//            if (GetComponentInParent<TPControl>().hunterDie)
//            {
//                bulletNum = 1;
//            }

//            if (GameObject.Find("PlayerX").GetComponent<TPControl>().startGame && Input.GetButton("Fire1")
//                && GameObject.Find("PlayerX").GetComponent<characterData>().teamNum == 2 
//                && GameObject.Find("PlayerX").GetComponent<characterData>().hp > 0 && bulletNum > 0 || autoShoot)
//            {
//                view.RPC("RPC_Shoot", RpcTarget.All);
//            }
//        }
//    }

//    //IEnumerator delayShootSound()
//    //{
//    //    yield return new WaitForSeconds(1.5f);
//    //    this.GetComponent<AudioSource>().Stop();
//    //}

//    [PunRPC]
//    void RPC_Shoot()
//    {
//        AudioSource audio = GetComponent<AudioSource>();
//        audio.outputAudioMixerGroup = mast;
//        audio.clip = shootSound;
//        StartCoroutine("Fade");    
//    }

//    IEnumerator Fade()
//    {
//        yield return new WaitForSeconds(0.1f);
//        this.GetComponent<AudioSource>().Play();
//        Ray ray = new Ray(gunTransform.position, gunTransform.forward);
//        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
//        {
//            var hiderHealth = hit.collider.GetComponent<characterData>();
//            if (hiderHealth)
//            {
//                hiderHealth.LossHP(10);
//            }
//            else
//            {
//                GameObject.Find("PlayerX").GetComponent<healthDisplay>().takeDmg(5);
//            }
//        }
//    }
//}
