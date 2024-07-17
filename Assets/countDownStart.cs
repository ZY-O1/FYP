using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(Text))]
public class countDownStart : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public bool startCountV;
    public GameObject count;
    public float startC;

    // Start is called before the first frame update
    void LateStart()
    {
        startCountV = false;
        if (MenuManager.PvE)
        {
            count.SetActive(true);
        }
        else
        {
            count.SetActive(false);
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            string secV = ((int)startC % 60).ToString("00");
            releaseTime();
            GetComponent<Text>().text = secV;
            if (startC <= 0 && this.startCountV)
            {
                this.startCountV = false;
                this.count.SetActive(false);
            }
        }
    }

    public void startNum(float num)
    {
        startC = num;
        StartCoroutine(startGameCount());
    }

    public void releaseTime()
    {
        if (this.startCountV && startC > 0)
        {
            startC -= Time.deltaTime;
        }
    }

    IEnumerator startGameCount()
    {
        yield return new WaitForSeconds(2.0f);
        this.startCountV = true;
    }
}
