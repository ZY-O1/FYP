using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class SwapTeam : MonoBehaviourPunCallbacks, IPunObservable
{
    //public static GameObject LocalPlayerInstance;
    public static int team;
    public int teamNum;
    public bool disappear;
    public bool end = false;
    private Animator anim;
    public Avatar hide;
    public Avatar hide2;
    public Avatar hunt;
    public bool resetProp = false;
    public bool canChange;
    public int propNum;
    public bool skillCD;
    public int huntCount;
    public GameObject player;

    public GameObject hiderUI;
    public GameObject huntUI;
    public GameObject releaseH;
    public bool add;
    public bool getTeam;
    public int hunterCount;
    public static int character;

    characterData cd;
    public GameObject countDown;

    void Start()
    {
        end = false;
        this.add = true;
        this.canChange = true;
        this.GetComponent<MoveBehaviour>().enabled = false;
        anim = GetComponent<Animator>();
        cd = GetComponent<characterData>();

        if (!MenuManager.PvE)
        {
            photonView.RPC(nameof(teamSwap), RpcTarget.AllBufferedViaServer);
            photonView.RPC(nameof(changeHunt), RpcTarget.AllBufferedViaServer);
            photonView.RPC(nameof(changeHide), RpcTarget.AllBufferedViaServer);
            photonView.RPC(nameof(propUpdate), RpcTarget.All);
            photonView.RPC(nameof(deactivateMeshDie), RpcTarget.All);
            photonView.RPC(nameof(resetP), RpcTarget.All);
        }

        if (MenuManager.tuto)
        {
            this.teamNum = 1;
            this.GetComponent<MoveBehaviour>().enabled = true;
            changeHide();
        }
        
        if(!MenuManager.tuto)
        {
            Debug.Log(team);
            teamSwapE();
        }
    }
    

    void Update()
    {
        //Debug.Log($"'{name}' is Visible to '{Camera.current.name}'");
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    this.teamNum = 1;
        //}
        //else if (Input.GetKeyDown(KeyCode.K))
        //{
        //    this.teamNum = 2;
        //}


        if (MenuManager.PvE)
        {
            if (this.teamNum == 2 && add)
            {
                countDown.GetComponent<countDownStart>().startNum(20);
                StartCoroutine(hunterStart());
                changeHunt();
                add = false;
            }
            else if (this.teamNum == 1 && add)
            {
                countDown.GetComponent<countDownStart>().startNum(10);
                StartCoroutine(hiderStart());
                changeHide();                   
                add = false;
            }

            if (this.teamNum == 1 && cd.hp > 0)
            {
                propUpdate();

                if (cd.hp <= 0)
                {
                    deactivateMeshDie();
                }

            }     
        }
        else if (!MenuManager.PvE && !MenuManager.tuto)
        {
            if (photonView.IsMine)
            {
                teamSwap();
                if (this.teamNum == 2 && add && hunterCount < 2)
                {
                    GetComponentInChildren<countDownStart>().startNum(20);
                    StartCoroutine(hunterStart());
                    hunterCount++;
                    changeHunt();
                    add = false;
                }
                else if (this.teamNum == 1 && add)
                {
                    GetComponentInChildren<countDownStart>().startNum(10);
                    StartCoroutine(hiderStart());
                    changeHide();
                    add = false;
                }

                if (this.teamNum == 1)
                {
                    propUpdate();
                }
            }
        }

        if (end)
        {
            GetComponent<MoveBehaviour>().enabled = false;
        }
       
    }

    public void propUpdate()
    {
        if (this.canChange)
        {
            if (Input.GetKeyDown(KeyCode.C) && propNum == 0)
            {
                player.transform.GetChild(1).transform.GetChild(0 + character).gameObject.SetActive(false);
                player.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(true);
                propNum = 1;
                this.canChange = false;
            }
            else if (Input.GetKeyDown(KeyCode.C) && propNum == 1)
            {
                player.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(false);
                player.transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(true);
                propNum = 2;
                this.canChange = false;
            }
            else if (Input.GetKeyDown(KeyCode.C) && propNum == 2)
            {
                player.transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(false);
                player.transform.GetChild(1).transform.GetChild(4).gameObject.SetActive(true);
                propNum = 3;
                this.canChange = false;
            }
            else if (Input.GetKeyDown(KeyCode.C) && propNum == 3)
            {
                player.transform.GetChild(1).transform.GetChild(4).gameObject.SetActive(false);
                player.transform.GetChild(1).transform.GetChild(0 + character).gameObject.SetActive(true);
                this.canChange = false;
                propNum = 0;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (photonView.IsMine)
                {
                    photonView.RPC(nameof(deactivateMesh), RpcTarget.All);
                }
            }
        }

        if (!this.canChange)
        {
            StartCoroutine(changeProp());
        }

        if (this.resetProp)
        {
            resetP();
        }
    }

    public void teamSwapE()
    {
            if (team == 1)
            {
                this.teamNum = 1;
            }
            else if (team == 2)
            {
                this.teamNum = 2;
            }
    }

    [PunRPC]
    public void teamSwap()
    {
        {
            if (team == 1)
            {
                team = 2;
                this.teamNum = 1;
            }
            else if (team == 2 && huntCount < 2)
            {
                team = 1;
                this.teamNum = 2;
                huntCount++;
            }
            else if (huntCount >= 2)
            {
                team = 1;
                this.teamNum = 1;
            }
        }
    }

    [PunRPC]
    public void changeHunt()
    {
        this.huntUI.SetActive(true);
        this.anim.avatar = hunt;
        player.transform.GetChild(0).gameObject.SetActive(true);
        player.transform.GetChild(1).gameObject.SetActive(false);
        this.hiderUI.SetActive(false);
        this.GetComponent<AimBehaviourBasic>().enabled = true;
        cd.SetTeam(this.teamNum);
    }

    [PunRPC]
    public void changeHide()
    {
        Debug.Log("CanchangeHide");
        if (character == 0)
        {
            this.anim.avatar = hide;
        }
        else
        {
            this.anim.avatar = hide2;
        }
        player.transform.GetChild(1).gameObject.SetActive(true);
        player.transform.GetChild(0).gameObject.SetActive(false);
        player.transform.GetChild(1).transform.GetChild(0 + character).gameObject.SetActive(true);
        this.GetComponent<AimBehaviourBasic>().enabled = false;
        this.hiderUI.SetActive(true);
        cd.SetTeam(this.teamNum);
    }


    [PunRPC]
    public void deactivateMesh()
    {
        disappear = true;
        player.transform.GetChild(1).gameObject.SetActive(false);
        StartCoroutine(skillEffectLast());
    }

    [PunRPC]
    public void resetP()
    {
        player.transform.GetChild(1).transform.GetChild(0 + character).gameObject.SetActive(true);
        player.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(false);
        player.transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(false);
        player.transform.GetChild(1).transform.GetChild(4).gameObject.SetActive(false);
    }


    [PunRPC]
    public void deactivateMeshDie()
    {
        player.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void resetProps(bool reset)
    {
        this.resetProp = reset;
    }

    public void GameOver(bool move)
    {
        if (this.teamNum == 1 && result.winHunt && !end)
        {
            end = move;
        }
        else if (this.teamNum == 1 && !result.winHunt && !end)
        {
            int earnMoney = Random.Range(20, 50);
            CurrencyEarn.instanceC.earnCurrency(earnMoney);
            end = move;
        }
       
        if (this.teamNum == 2 && result.winHunt && !end)
        {
            int earnMoney = Random.Range(20, 50);
            CurrencyEarn.instanceC.earnCurrency(earnMoney);
            end = move;
        }
        else if (this.teamNum == 2 && !result.winHunt && !end)
        {
            end = move;
        }
    }

    IEnumerator changeProp()
    {
        yield return new WaitForSeconds(3.0f);
       this.canChange = true;
    }

    IEnumerator skillEffectLast()
    {
        yield return new WaitForSeconds(5.0f);
        player.transform.GetChild(1).gameObject.SetActive(true);
        disappear = false;
    }

    IEnumerator hunterStart()
    {
        GetComponentInChildren<countDownStart>().startNum(10);
        yield return new WaitForSeconds(12.0f);
        this.huntUI.SetActive(false);
        this.GetComponent<MoveBehaviour>().enabled = true;
        this.GetComponentInChildren<LaserGunScript>().checkShoot(true);
        releaseH.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        releaseH.SetActive(false);
    }

    IEnumerator hiderStart()
    {
        GetComponentInChildren<countDownStart>().startNum(10);
        yield return new WaitForSeconds(12.0f);
        this.GetComponent<MoveBehaviour>().enabled = true;
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(teamNum);
        }
        else
        {
            teamNum = (int)stream.ReceiveNext();
        }
    }
}

