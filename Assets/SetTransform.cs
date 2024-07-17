using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransform : MonoBehaviour
{
    public GameObject playerObj;
    // Update is called once per frame
    void Update()
    {
        playerObj = GameObject.Find("Player");
        playerObj.transform.parent = this.transform;
    }
}
