using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(AudioSource))]
public class interact : MonoBehaviourPunCallbacks
{
    public GUIStyle style = new GUIStyle();
    public GUIStyle style2 = new GUIStyle();

    private string timeText;

    [SerializeField]
    public bool energyMinus;
    public bool trapSet;

    // Start is called before the first frame update
    void Start()
    {
        style.normal.textColor = Color.yellow;
        style2.normal.textColor = Color.red;
        energyMinus = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuManager.PvE)
        {
            interacting();
        }
        else
        {
            if (photonView.IsMine)
            {
                interacting();
            }
        }
    }

        
        void interacting()
        {
            RaycastHit hit;
            Debug.DrawRay(this.transform.position + new Vector3(0f, 1f, 0f), this.transform.forward, Color.red);
            if (Physics.Raycast(this.transform.position + new Vector3(0f, 1f, 0f), this.transform.forward, out hit, 5.0f))
            {
                if (hit.collider.gameObject.tag == "trapOilTank" && Input.GetKeyDown(KeyCode.F))
                {
                    interactText.messageF = "              TRAP SET";
                    interactText.textF = true;
                    trapSet = true;
                }
                else
                {
                    trapSet = false;
                }

                if (hit.collider.gameObject.tag == "energyReset" && GetComponent<StaminaAndEnergy>().energy.value > 0)
                {
                    if (GetComponent<StaminaAndEnergy>().energyMinus == false)
                    {
                        Debug.Log("down");
                        GetComponent<StaminaAndEnergy>().energyDown(true);
                    }

                     GetComponent<characterData>().addHP(1);
                }
                else if (GetComponent<StaminaAndEnergy>().energy.value <= 0)
                {
                    GetComponent<StaminaAndEnergy>().energyDown(false);
                }


                if (hit.collider.gameObject.tag == "Player" && hit.collider.gameObject.GetComponent<SwapTeam>().teamNum == 1)
                {
                    if (Input.GetKeyDown(KeyCode.T))
                    {
                        hit.collider.GetComponent<characterData>().resetHP(120);
                    }
                }

                if (hit.collider.gameObject.tag == "Result")
                {
                    if (GameObject.Find("Result").GetComponent<result>().teamHunter < 5 && this.GetComponent<SwapTeam>().teamNum == 1)
                    {
                        interactText.messageF = "Mission Not Yet Complete";
                        interactText.textF = true;
                    }
                }
            }
        }
}
