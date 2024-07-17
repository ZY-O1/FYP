using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class StaminaAndEnergy : MonoBehaviourPunCallbacks
{
    public Animator staminaBar;
    public Animator energyBar;
    public float energyLeak = 1.0f;
    public ParticleSystem particles;
    public float particleTimer;
    MoveBehaviour mb;
    public float staminaConsume = 1.0f;

    [SerializeField]
    public bool leak;

    public Slider stamina;
    public Slider energy;
    public GameObject exposeSpot;

    characterData cd;
    SwapTeam st;
    public bool energyMinus;

    // Start is called before the first frame update
    void Start()
    {
        stamina.maxValue = 10f;
        stamina.value = 10f;
        energy.maxValue = 100f;
        energy.value = 0f;
        cd = GetComponent<characterData>();
        mb = GetComponent<MoveBehaviour>();
        st = GetComponent<SwapTeam>();
        photonView.RPC(nameof(energyManage), RpcTarget.AllBufferedViaServer);
        photonView.RPC(nameof(staminaManage), RpcTarget.AllBufferedViaServer);
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuManager.PvE)
        {
            energyManage();
            staminaManage();
        }
        else
        {
            if (photonView.IsMine)
            {
                energyManage();
                staminaManage();
            }
        }
    }

    [PunRPC]
    void staminaManage()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            this.staminaBar.Play("FadeINStamina");
            if (this.stamina.value > 0)
            {
                mb.sprintSpeed = 2.5f;
                this.stamina.value -= staminaConsume * Time.deltaTime;
            }
            else
            {
                mb.sprintSpeed = 0.5f;
            }
        }
        else if(!Input.GetKey(KeyCode.LeftShift))
        {
            if(this.stamina.value < 10)
            {
                this.stamina.value += staminaConsume / 2 * Time.deltaTime;
                this.staminaBar.Play("FadeOUTStamina");
            }
        }
    }

    [PunRPC]
    void energyManage()
    {
        if (energyMinus == true && this.energy.value > 0 && st.teamNum == 1)
        {
            this.energy.value -= energyLeak * 2 * Time.deltaTime;
        }

        if (this.energy.value < 100 && cd.dieCountDown > 0 && !energyMinus && st.teamNum == 1)
        {
            Pause();
            this.energyBar.SetBool("isLeak", false);
            this.energy.value += energyLeak * 0.5f * Time.deltaTime;
            leak = false;
        }
        else if (this.energy.value >= 100 && cd.dieCountDown > 0 && st.teamNum == 1)
        {
            leak = true;
            Continue();
            this.energyBar.SetBool("isLeak", true);
        }
        else if (cd.dieCountDown <= 0 && st.teamNum == 1)
        {
            Pause();
            this.energyBar.SetBool("isLeak", false);
            this.energy.value = 0;
        }

        if (st.teamNum == 2)
        {
            Pause();
        }
    }


    public void energyDown(bool down)
    {
        energyMinus = down;
    }

    private void Pause()
    {
        this.particles.Stop();
        this.exposeSpot.SetActive(false);
    }

    private void Continue()
    {
        this.particles.Play();
        this.exposeSpot.SetActive(true);
    }
}
