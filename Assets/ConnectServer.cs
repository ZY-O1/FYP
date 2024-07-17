using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class ConnectServer : MonoBehaviourPunCallbacks
{
    public GameObject timeOutBtn;

    private void Start()
    {
        timeOutBtn.SetActive(false);
        //PhotonNetwork.GameVersion = "1";
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("LoadingToConnect");
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            StartCoroutine(LoadIntoLobby());
        }
        StartCoroutine(TimeOut());
        //PhotonNetwork.ConnectToRegion(asia);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected");
        Debug.Log(sceneNo.sceneNum);
        if (sceneNo.sceneNum == 7 && PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
        }

    }

    public override void OnJoinedLobby()
    {
       PhotonNetwork.LoadLevel("JoinRoom");
       Debug.Log("In Lobby");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from photon :" + cause.ToString());
        PhotonNetwork.Reconnect();
        StartCoroutine(TimeOut());

    }

    public void quitTimeOut()
    {
        PhotonNetwork.LeaveLobby();
        SceneManager.LoadScene("Menu");
    }

    IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(20.0f);
        timeOutBtn.SetActive(true);
    }

    IEnumerator LoadIntoLobby()
    {
        yield return new WaitForSeconds(3.0f);
        OnConnectedToMaster();
    }
}
