using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateJoinRoom : MonoBehaviourPunCallbacks
{
    public GameObject lobby;
    public GameObject room;
    public InputField createInput;
    public InputField joinInput;
    //public Toggle ready;
    public GameObject playBtn;

    public Text errorShow;
    public Text roomID;
    public RoomItem roomItemPrefab;
    List<RoomItem> roomItemsList = new List<RoomItem>();
    public Transform contentObject;

    public List<PlayerItem> playerList = new List<PlayerItem>();
    public PlayerItem playerItemPrefab;
    public Transform playerObject;

    public float timeDelay = 1.5f;
    float nextUpdateTime;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2 /*&& ready.isOn*/)
        {
            playBtn.SetActive(true);
        }
        else
        {
            playBtn.SetActive(false);
        }
    }

    public void OnClickStartGame()
    {
        Debug.Log(MapChooseToggle.map);
        if (MapChooseToggle.map == 1)
        {
            PhotonNetwork.LoadLevel("Factory");
        }
        
        if (MapChooseToggle.map == 2)
        {
            PhotonNetwork.LoadLevel("Lab");
        }

        if (MapChooseToggle.map == 3)
        {
            PhotonNetwork.LoadLevel("SnowyLand");
        }

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Lobby");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
            Debug.Log("In Lobby");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from photon :" + cause.ToString());
        PhotonNetwork.ReconnectAndRejoin();
    }

    public void CreateRoom()
    {
        if (createInput.text.Length >= 1 && MapChooseToggle.map != 0)
        {
            Debug.Log("Creating");
            PhotonNetwork.CreateRoom(createInput.text, new RoomOptions() { MaxPlayers = 10, BroadcastPropsChangeToAll = true});
        }

        if (MapChooseToggle.map == 0)
        {
            errorShow.text = "Input Not Valid!";
        }
        else
        {
            errorShow.text = "";
        }
    }

    public void JoinManual()
    {
        if (joinInput.text.Length >= 1)
        {
            Debug.Log("Joining...");
            PhotonNetwork.JoinRoom(joinInput.text);
        }
    }

    public void JoinRoom(string roomName)
    {
        if (roomName.Length >= 1)
        {
            Debug.Log("Joining");
            PhotonNetwork.JoinRoom(roomName);
        }
    }

    //public void Ready()
    //{
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        foreach (Player player in PhotonNetwork.CurrentRoom.Players)
    //        {
    //            player.CustomProperties["ready"] = ready.isOn;
    //        }
    //    }
    //}

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        lobby.SetActive(true);
        room.SetActive(false);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joining Room");
        lobby.SetActive(false);
        room.SetActive(true);
        roomID.text = "Room ID: \n" + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
        //if (MapChooseToggle.map == 1)
        //{
        //    PhotonNetwork.LoadLevel("Factory");
        //}

        //if (MapChooseToggle.map == 2)
        //{
        //    PhotonNetwork.LoadLevel("Lab");
        //}

        //if (MapChooseToggle.map == 3)
        //{
        //    PhotonNetwork.LoadLevel("SnowyLand");
        //}
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeDelay;
        }
    }

    void UpdateRoomList(List<RoomInfo> list)
    {
        foreach (RoomItem item in roomItemsList)
        {
            Destroy(item.gameObject);
        }

        roomItemsList.Clear();

        foreach (RoomInfo room in list)
        {
            RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.SetRoomName(room.Name);
            roomItemsList.Add(newRoom);
        }
    }

    void UpdatePlayerList()
    {
        foreach (PlayerItem item in playerList)
        {
            Destroy(item.gameObject);
        }
        playerList.Clear();

        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerObject);
            newPlayerItem.SetPlayerInfo(player.Value);
            playerList.Add(newPlayerItem);
        }
    }
}
