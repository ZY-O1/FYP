using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class characterData : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    public int healthChar;
    public int hp;
    public int teamNumC;
    public float dieCountDown = 15;

    Scene curG;
    string sceneG;
    public Transform cube;


    public bool dead;
    public Animator thisC;
    public Animator blood;
    result rs;
    /// </summary>

    public bool gameStart;
    public bool respawn;
    public bool scoreOnce;
    public Transform player;
    randomSpawnPoint rsp;

   // public int characterNum; //characterSelectSet
    
    public bool resetProp; //propToggler

    healthDisplay hd;
    SwapTeam st;

    void Start()
    {
        //coins = PlayerPrefs.GetInt("coins");
        gameStart = true;
        scoreOnce = true;
        curG = SceneManager.GetActiveScene();
        sceneG = curG.name;

            thisC = this.GetComponent<Animator>();
            hd = this.GetComponentInChildren<healthDisplay>();
            blood = this.GetComponent<Animator>();
            st = this.GetComponent<SwapTeam>();

        if (!MenuManager.PvE)
        {
            photonView.RPC(nameof(playerStatus), RpcTarget.AllBuffered);
        }

        //characterNum = PlayerPrefs.GetInt("characterSelected";
        rs = GameObject.FindWithTag("Result").GetComponent<result>();
        rsp = GameObject.FindWithTag("spawn").GetComponent<randomSpawnPoint>();
    }


    //void dieTimerRun(bool startRun)
    //{
    //    if (startRun)
    //    {
    //        this.dieCountDown -= Time.deltaTime;
    //    }
    //}

    public void SetTeam(int team)
    {
        Debug.Log("CharacterDATA");
        teamNumC = team;
    }

    public void lossHP(int update)
    {
        hp -= update;
        hd.takeDmg(update);
    }

    public void addHP(int update)
    {
        hp += update;
        hd.restoreHP(update);
    }

    public void resetHP(int resetHP)
    {
        hp = resetHP;
        hd.resetHP(resetHP);
    }

    //public void gameStatus(bool start)
    //{
    //    gameStart = start;
    //}

    public void respawning(bool r)
    {
        respawn = r;
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(hp);
            stream.SendNext(teamNumC);
        }
        else
        {
            hp = (int)stream.ReceiveNext();
            teamNumC = (int)stream.ReceiveNext();
        }
    }

    IEnumerator resetDie()
    {
        yield return new WaitForSeconds(10.0f);
        if (MenuManager.PvE)
        {
            this.transform.position = rsp.targetRespawn.position;
            if (teamNumC == 2)
            {
                this.GetComponent<MoveBehaviour>().enabled = true;
            }
            else
            {
                st.deactivateMeshDie();
            }
            this.scoreOnce = false;
            this.dieCountDown = 15f;
        }
        
        if(MenuManager.tuto)
        {
            thisC.SetBool("Die", false);
            this.GetComponentInChildren<healthDisplay>().resetHP(120);
            this.transform.position = cube.transform.position;
        }
    }

    void Update()
    {
        if (sceneG == "Factory" || sceneG == "SnowyLand" || sceneG == "Lab")
        {
            if (photonView.IsMine)
            {
                playerStatus();
            }
        }
        else
        {
            playerStatus();
        }
    }

    [PunRPC]
    public void playerStatus()
    {
            if (teamNumC == 1 && gameStart)
            {
                resetHP(120);
                Debug.Log(hp);
                this.GetComponentInChildren<healthDisplay>().resetHP(120);
                gameStart = false;
            }
            else if (teamNumC == 2 && gameStart)
            {
                resetHP(100);
                Debug.Log(hp);
                this.GetComponentInChildren<healthDisplay>().resetHP(100);
                gameStart = false;
            }

            if (respawn && teamNumC == 2)
            {
                this.GetComponent<AimBehaviourBasic>().enabled = true;
                resetHP(100);
                Debug.Log(hp);
                respawn = false;
            }

            if (hp <= 0 && this.dieCountDown > 0)
            {
                st.resetProps(true);
                thisC.SetBool("Aim", false);
                thisC.SetBool("Injure", true);
                blood.SetBool("isDying", true);
                this.GetComponent<AimBehaviourBasic>().enabled = false;
                this.dieCountDown -= Time.deltaTime;
            }
            else
            {
                st.resetProps(false);
                thisC.SetBool("Injure", false);
                blood.SetBool("isDying", false);
            }

        if (player.position.y < -2f)
        {
            Vector3 pos = player.position;
            pos.y = 10;
            player.position = pos;
        }

            if (dieCountDown <= 0 && this.scoreOnce)
            {
                if (teamNumC == 2)
                {
                    Debug.Log("hunterDie");
                    rs.huntDie();
                    this.scoreOnce = false;
                }
                else if (teamNumC == 1)
                {
                    Debug.Log("hiderDie");
                    rs.hideDie();
                    this.scoreOnce = false;
                }
                this.GetComponent<MoveBehaviour>().enabled = false;
                thisC.SetBool("Die", true);
                StartCoroutine(resetDie());
            }
            else if (teamNumC == 2 && dieCountDown > 0)
            {
                thisC.SetBool("Die", false);
            }
    }
}

