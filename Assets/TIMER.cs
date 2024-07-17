using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class TIMER : MonoBehaviourPunCallbacks
{
    public int matchLength = 300;
    public static float currentMatchTimer;
    public static bool timeOut;
    public static bool startCount;
    private Coroutine timerCoroutine;
    string min;
    string sec;
    ExitGames.Client.Photon.Hashtable CustomeValue;

    // Start is called before the first frame update
    void Start()
    {
        startCount = false;
        timeOut = false;
        //StartCoroutine(Timer());
        RefreshTimerUI();

        //if (!MenuManager.PvE)
        //{
        //    photonView.RPC(nameof(RPC_Time), RpcTarget.AllBuffered);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        min = ((int)currentMatchTimer / 60).ToString("00");
        sec = ((int)currentMatchTimer % 60).ToString("00");
        if (MenuManager.PvE)
        {
            if (currentMatchTimer > 0 && startCount)
            {
                currentMatchTimer -= Time.deltaTime;
            }
            else if (currentMatchTimer < 0)
            {
                EndGame();
            }
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (currentMatchTimer > 0)
                {
                    currentMatchTimer -= Time.deltaTime;
                }
                else
                {
                    EndGame();
                }
            }
        }
        GetComponent<Text>().text = $"{min}:{sec}";
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(12.0f);
        startCount = true;
    }


    IEnumerator startGameH()
    {
        yield return new WaitForSeconds(22.0f);
        startCount = true;
    }

    void EndGame()
    {
        GameObject.Find("Result").GetComponent<result>().timeRunsOut(true);
        if (timerCoroutine != null) StopCoroutine(timerCoroutine);
        currentMatchTimer = 0;
    }

    private void RefreshTimerUI()
    {
        currentMatchTimer = matchLength;
        if (SwapTeam.team == 1)
            StartCoroutine(startGame());
        else
            StartCoroutine(startGameH());
    }

}
