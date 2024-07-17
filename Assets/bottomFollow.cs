using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottomFollow : MonoBehaviour
{
    public Vector3 target;
    public Transform plane;
    public Transform player;
    // Update is called once per frame
    void Update()
    {
        player.position = this.player.position;
        this.target = new Vector3(player.position.x, player.position.y, player.position.z);
        plane.position = this.target;
    }
}
