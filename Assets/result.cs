using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class result : MonoBehaviourPunCallbacks
{

    [SerializeField]
    public int teamHunter;
    public int teamHider;
    public bool offline;

    [SerializeField]
    public bool hideWin;
    public bool huntWin;
    public bool atWin;

    PhotonView view;
    characterData cd;
    UIManager ui;
    SwapTeam st;
    MenuManager m;

    public bool testWin;
    public bool timeOut;

    [SerializeField]
    public bool setPointHid;
    public bool setPointHun;
    public static bool winHunt;
    public bool run;
    public bool runOnce;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        m = GameObject.Find("MENUMANAGER").GetComponent<MenuManager>();
        run = true;
        runOnce = true;
        if (m.scene == "FactoryE" || m.scene == "LabE" || m.scene == "SnowyLandE")
        {
           st = GameObject.Find("Play").GetComponent<SwapTeam>();
           ui = GameObject.Find("Play").GetComponent<UIManager>();
    
        }
        else
        {
            teamHunter = 0;
            teamHider = 8;
        }
        var photonViews = UnityEngine.Object.FindObjectsOfType<PhotonView>();
        foreach (var view in photonViews)
        {
            var player = view.IsMine;
            //Objects in the scene don't have an owner, its means view.owner will be null
            if (player != null)
            {
                var playerPrefabObject = view.gameObject;
                st = playerPrefabObject.GetComponent<SwapTeam>();
                ui = playerPrefabObject.GetComponent<UIManager>();
            }
        }
        hideWin = false;
        huntWin = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (SwapTeam.team == 1 && runOnce)
        {
            teamHunter = 0;
            teamHider = 1;
            runOnce = false;
        }
        else if (SwapTeam.team == 2 && runOnce)
        {
            teamHunter = 0;
            teamHider = 8;
            runOnce = false;
        }

        if (MenuManager.PvE && run)
        {
            winLossE();
        }
        else
        {
            if (photonView.IsMine)
            {
                winLoss();
            }
        }
    }

    public void winLoss()
    {
        if (atWin && teamHunter >= 5|| timeOut)
        {
            winHunt = false;
            st.GameOver(true);
            ui.endLogoHide();
            StartCoroutine(GameOver());
            run = false;
        }
        else if (teamHider <= 0)
        {
            winHunt = true;
            Debug.Log(winHunt);
            //st.GameOver(true);
            ui.endLogoHunt();
            StartCoroutine(GameOver());
            run = false;
        }

        if (testWin)
        {
            StartCoroutine(GameOver());
        }
    }

    public void winLossE()
    {
        if (SwapTeam.team == 1)
        {
            if (atWin && teamHunter >= 5 || timeOut)
            {
                st.GameOver(true);
                ui.endLogoHide();
                winHunt = false;
                StartCoroutine(GameOverE());
            }
            else if (teamHider <= 0)
            {
                st.GameOver(true);
                ui.endLogoHunt();
                winHunt = true;
                StartCoroutine(GameOverE());
            }
        }
        else if (SwapTeam.team == 2)
        {
            if (teamHunter >= 1 || timeOut)
            {
                st.GameOver(true);
                ui.endLogoHide();
                winHunt = false;
                StartCoroutine(GameOverE());
            }
            else if (teamHider <= 0)
            {
                st.GameOver(true);
                ui.endLogoHunt();
                winHunt = true;
                StartCoroutine(GameOverE());
            }
        }

        if (testWin)
        {
            StartCoroutine(GameOverE());
        }
    }


    public void timeRunsOut(bool time)
    {
        timeOut = time;
    }

    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        GameObject thePlayer = other.gameObject;
        if (thePlayer.GetComponent<SwapTeam>().teamNum == 1) //on the object you want to interact
        {
            atWin = true;
        }
    }


    private void OnTriggerExit(Collider other) // to see when the player enters the collider
    {
        //if (other.gameObject.tag == "hider") //on the object you want to interact
        //{
         atWin = false;
        //}
    }

    IEnumerator GameOver()
    {
        GameObject[] playerObj = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in playerObj)
        {
           player.GetComponent<MoveBehaviour>().enabled = false;
        }
        yield return new WaitForSeconds(5.0f);
        PhotonNetwork.LoadLevel("result");
    }

    IEnumerator GameOverE()
    {
        GameObject.FindWithTag("Player").GetComponent<MoveBehaviour>().enabled = false;
        yield return new WaitForSeconds(5.0f);
        m.backToStage();
    }

    public void huntDie()
    {
        Debug.Log("someonedie");
        teamHunter += 1;
    }

    public void hideDie()
    {
        Debug.Log("someonedie");
        teamHider -= 1;
    }

}
