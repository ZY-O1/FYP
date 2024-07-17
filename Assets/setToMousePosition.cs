using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class setToMousePosition : MonoBehaviourPunCallbacks
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private LayerMask lm;
    Vector3 pos;
    public float speed = 1f;

    private void Start()
    {
        mainCam = GameObject.Find("MAINCAM").GetComponent<Camera>();
    }

    private void Update()
    {
        //Ray ray = this.mainCam.ScreenPointToRay(Input.mousePosition);
        pos = Input.mousePosition;
        pos.z = speed;
        transform.position = this.mainCam.ScreenToWorldPoint(pos);
        //if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, lm))
        //{
        //    this.transform.position = raycastHit.point;
        //}
    }
}
