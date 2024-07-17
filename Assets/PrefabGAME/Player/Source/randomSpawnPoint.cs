using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class randomSpawnPoint : MonoBehaviourPunCallbacks
{
    public int randomNum;
    public Transform[] target;
    //public GameObject playerPrefab;
    public static GameObject[] playerList;
    public Transform targetRespawn;
    public AudioClip respawnS;
    public static bool changeR;
    GameObject army;
    public GameObject spawnArmy;
    public int armyNum;
    public GameObject propAi;
    public int propE;
    public static bool spawnA;

    [SerializeField]
    public bool respawn;
    public bool respawnHide;
    public bool canCount;
    public bool gameS;


    //public Transform spawn1;
    //public Transform spawn2;
    //public Transform spawn3;
    //public Transform spawn4;
    public Transform hiderRespawn;

    void Start()
    {
        spawnA = true;

        if (PhotonNetwork.IsConnected)
        {
            randomNum = Random.Range(0, target.Length - 1);
            Transform targets = target[randomNum];
          //  GameObject thisPlayer = PhotonNetwork.Instantiate(this.playerPrefab.name, targets.position, Quaternion.identity);
        }
        army = GameObject.FindWithTag("Enemy");
    }

    public void Update()
    {
        if (spawnA && MenuManager.PvE && SwapTeam.team == 1)
        {
            //Debug.Log("CanSpawn");
            if (armyNum < 5)
            {
                int ranNum = Random.Range(0, target.Length - 1);
                Transform targeto = target[ranNum];
                Instantiate(spawnArmy, targeto.position, Quaternion.identity);
                armyNum += 1;

            }
            else
            {
                spawnA = false;
                //armyNum = 0;
            }
        }

        if (spawnA && MenuManager.PvE && SwapTeam.team == 2)
        {
            Debug.Log("CanSpawn");
            if (propE < 8)
            {
                int propN = Random.Range(0, target.Length - 1);
                Transform targete = target[propN];
                Instantiate(propAi, targete.position, Quaternion.identity);
                propE += 1;
            }
            else
            {
                spawnA = false;
            }
        }

        if (changeR)
        {
            photonView.RPC(nameof(changeRespawn), RpcTarget.MasterClient);
            changeR = !changeR;
        }
    }


    //[PunRPC]
    //void RPC_GetTeam()
    //{
    //    thisTeam = st.teamNumSet;
    //    pv.RPC("RPC_SendTeam", RpcTarget.OthersBuffered, thisTeam);

    //}

    //[PunRPC]
    //void RPC_SendTeam(int whichTeam)
    //{
    //    thisTeam = whichTeam;
    //}

    //IEnumerator randomReset()
    //{
    //    yield return new WaitForSeconds(10.5f);
    //    random = true;
    //}


    [PunRPC]
    public void changeRespawn()
    {
        int randomNumR = Random.Range(0, target.Length - 1);
        targetRespawn = target[randomNumR];
    }
}
