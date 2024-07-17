using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Text roomName;
    CreateJoinRoom cjr;

    private void Start()
    {
        cjr = FindObjectOfType<CreateJoinRoom>();
    }

    public void SetRoomName(string room_Name)
    {
        roomName.text = room_Name;
    }

    public void OnClickItem() //Click To Join Room
    {
        cjr.JoinRoom(roomName.text);
    }
}
