//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using Photon.Pun;

//public class TEAM : MonoBehaviour
//{
//    [SerializeField] public bool huntOrHide;
//    public bool runOnce;
//    [SerializeField] public GameObject akmShoot; //GUN
//    [SerializeField] public GameObject hunter;
//    [SerializeField] public GameObject hider;

//    [SerializeField] public GameObject energyB;
//    [SerializeField] public GameObject missionB;
//    [SerializeField] public GameObject canvas;

//    [SerializeField] public GameObject huntWait;
//    [SerializeField] public GameObject hunterOut;
//    [SerializeField] public GameObject exposeSpot;
//    [SerializeField] public GameObject skill;
//    public bool instantDead;

//    public Animator[] control;
//    public Animator player; //character animation

//    public Animator staminaBar;
//    public Animator blood;
//    public Animator energyBar;

//    public float idleSwapTimer = 20;
//    public bool whacked;

//    PlayerController pc;
//    characterData cd;
//    result r;
//    interact i;


//    public Slider stamina;
//    public Slider energy;
//    public Slider skillCD;
//    public float energyLeak;
//    public ParticleSystem particles;
//    public float particleTimer;

//    void Start()
//    {
//        i = GetComponent<interact>();
//        cd = GetComponent<characterData>();
//        pc = GetComponent<PlayerController>();
//        r = GameObject.Find("Result").GetComponent<result>();
//        skillCD.value = 10;
//        skillCD.maxValue = 10;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        checkTeam();
//        if (huntOrHide && runOnce)
//        {
//            player = control[2];
//            cd.countS = 20f;
//            StartCoroutine(startLoad1());
//            this.huntWait.SetActive(true);
//            this.hider.SetActive(false);
//            this.hunter.SetActive(true);
//            runOnce = false;
//        }
//        else if (!huntOrHide && runOnce)
//        {
//            player = control[2];
//            cd.countS = 10f;
//            StartCoroutine(startLoad2());
//            hunter.SetActive(false);
//            hider.SetActive(true);
//            runOnce = false;
//        }

//        if (huntOrHide && !r.hideWin && !r.huntWin)
//        {
//            instantDead = false;
//            energyB.SetActive(true);
//            missionB.SetActive(true);
//            skill.SetActive(true);
//            player.SetBool("isSWAT", false);
//            teamHide();
//        }
//        else if (huntOrHide && !r.hideWin && !r.huntWin)
//        {
//            instantDead = false;
//            energyB.SetActive(false);
//            missionB.SetActive(false);
//            skill.SetActive(false);
//            player.SetBool("isSWAT", true);
//            teamHunt();
//        }
//    }

//    void checkTeam()
//    {
//        if (cd.teamNum == 1)
//        {
//            huntOrHide = false; //hunt
//        }
//        else if (cd.teamNum == 2)
//        {
//            huntOrHide = true; //hide
//        }
//    }

//    private void teamHunt()
//    {
//        player.SetBool("isIdle", false);
//        AudioSource audio = GetComponent<AudioSource>();
//        //almostDie
//        if (this.pc.injured)
//        {
//            akmShoot.SetActive(false);
//            if (cd.dieTimer == 15)
//            {
//                this.whacked = true;
//                player.Play("injureIdle");
//            }
//            injureAnim();
//            cd.dieTimer -= Time.deltaTime;
//            // Debug.Log(dieTimer);
//        }

//        if (cd.hp > 0 && !pc.dead)
//        {
//            cd.dieTimer = 15f;
//        }

//        //dead
//        if (pc.dead)
//        {
//            this.instantDead = true;
//            this.blood.SetBool("isDying", false);
//            this.energy.value = 0f;
//            deadAnim();
//        }
//        else
//        {
//            pc.pc.dead = false;
//            player.SetBool("isDead", false);
//            player.SetBool("isInjureIdle", false);
//            player.SetBool("isInjureMove", false);
//        }

//        if (this.particleTimer > 0)
//        {
//            this.particleTimer -= Time.deltaTime;
//        }
//        if (this.particleTimer <= 0)
//        {
//            Pause();
//        }

//        if (this.pc.injured) //almost die
//        {
//            this.blood.SetBool("isDying", true);
//            if (!this.isMoving)
//            {
//                player.SetBool("isInjureIdle", true);
//            }
//            else
//            {
//                player.SetBool("isInjureIdle", false);
//            }
//        }
//        else // below is normal move
//        {
//            this.akmShoot.SetActive(true);
//            if (!this.isMoving && !pc.dead)
//            {
//                noMove();
//                player.SetBool("isShoot", true);
//            }
//            else
//            {
//                player.SetBool("isShoot", false);
//                if (Input.GetButton("Fire1") && cd.teamNum == 2 && !Input.GetMouseButton(1) && !pc.dead)
//                {
//                    player.SetBool("isShootMove", true);
//                }
//                else
//                {
//                    player.SetBool("isShootMove", false);
//                }
//                //idleSwapTimer = 20;
//                player.SetBool("isLook", false);
//            }
//        }
//    }


//    private void teamHide()
//    {
//        AudioSource audio = GetComponent<AudioSource>();
//        //almostDie
//        if (this.pc.injured)
//        {
//            if (cd.dieTimer == 15 && pc.controller.enabled == true)
//            {
//                this.whacked = true;
//                this.player.Play("injureIdle");
//            }
//            injureAnim();
//            cd.dieTimer -= Time.deltaTime;
//        }
//        else
//        {
//            objectChanger.propChanger();
//        }

//        if (pc.healthy)
//        {
//            cd.dieTimer = 15f;
//            this.injureMove = false;
//            this.blood.SetBool("isDying", false);
//        }

//        //dead
//        if (pc.dead)
//        {
//            this.blood.SetBool("isDying", false);
//            this.energy.value = 0f;
//            deadAnim();
//            this.injureMove = false;
//            deadAnim();
//        }
//        else
//        {
//            pc.dead = false;
//            //player.SetBool("isDead", false);
//            player.SetBool("isInjureIdle", false);
//            player.SetBool("isInjureMove", false);
//        }

//        //if (rsp.respawn)
//        //{
//        //    avatarSwapper();
//        //    //GetComponent<CharacterController>().enabled = true;
//        //    player.Play("Idle");
//        //    injureMove = false;
//        //}

//        player.SetBool("isMove", false);
//        player.SetBool("isRun", false);
//        if (this.particleTimer > 0)
//        {
//            this.particleTimer -= Time.deltaTime;
//        }
//        if (this.particleTimer <= 0)
//        {
//            Pause();
//        }

//        if (pc.pc.injured && cd.dieTimer > 0) //almost die
//        {
//            this.blood.SetBool("isDying", true);
//            if (!pc.isMoving)
//            {
//                this.speed = 0f;
//                player.SetBool("isInjureIdle", true);
//            }
//            else
//            {
//                player.SetBool("isInjureIdle", false);
//            }
//        }
//        else // below is normal move
//        {
//            if (!this.isMoving && !pc.dead)
//            {

//            }
//            else
//            {
//                this.idleSwapTimer = 20;
//                player.SetBool("isLook", false);
//            }
//        }
//    }


//    void hide()
//    {
//        if (this.idleSwapTimer > 0)
//        {
//            this.idleSwapTimer -= Time.deltaTime;
//        }
//        else if (this.idleSwapTimer <= 0)
//        {
//            this.idleSwapTimer = 20;
//        }

//        if (this.idleSwapTimer >= 5 && this.idleSwapTimer <= 10)
//        {
//            animDo("isIdle");
//            animStop("isLook");
//        }
//        else
//        {
//            animDo("isLook");
//            animStop("isIdle");
//        }
//    }


//    private void energyManage()
//    {
//        if (this.i.energyMinus == true && this.energy.value > 0)
//        {
//            this.energy.value -= 20 * Time.deltaTime;
//        }
//        else
//        {
//            this.i.energyMinus = false;
//            if (this.energy.value < 100 && !pc.dead && this.i.energyMinus == false)
//            {
//                Pause();
//                this.energy.value += energyLeak * Time.deltaTime;
//                this.energyBar.SetBool("isLeak", false);
//                //this.leaked = false;
//            }
//            else if (this.energy.value >= 100 && !pc.dead)
//            {
//                //this.leaked = true;
//                Continue();
//                this.energyBar.SetBool("isLeak", true);
//            }
//        }

//        if (Input.GetKeyDown(KeyCode.E) && this.skillCD.value == 10)
//        {
//            this.hider.SetActive(false);
//            cd.skillToggle(true);
//            this.skillCD.value = 0;
//        }

//        if (this.skillCD.value < 10)
//        {
//            this.skillCD.value += 0.25f * energyLeak * Time.deltaTime;
//        }

//        if (!cd.skill1)
//        {
//            this.hider.SetActive(true);
//        }
//        else
//        {
//            StartCoroutine(skillC());
//        }
//    }

//    public void Pause()
//    {
//        this.particles.Stop();
//        this.exposeSpot.SetActive(false);
//    }

//    public void Continue()
//    {
//        this.particles.Play();
//        this.exposeSpot.SetActive(true);
//    }

//    private void injureAnim()
//    {
//     player.SetBool("isGrounded", false);
//     player.SetBool("isIdle", false);
//     player.SetBool("isShoot", false);
//     player.SetBool("isMove", false);
//     player.SetBool("isRun", false);
//     player.SetBool("isJump", false);
//     player.SetBool("isFall", false);
//    }

//    private void deadAnim()
//    {
//        //if (cd.teamNum == 2)
//        //{
//        //    pc.dead = true;
//        //}
//        //else
//        //{
//        //    this.hiderDie = true;
//        //}
//        player.SetBool("isDead", true);
//        player.SetBool("isInjureIdle", false);
//        player.SetBool("isShoot", false);
//        player.SetBool("isIdle", false);
//        player.SetBool("isFall", false);
//        player.SetBool("isInjureMove", false);
//    }

//    private void idleAnim()
//    {
//        if (cd.teamNum == 1 && !pc.dead)
//        {
//            player.SetBool("isIdle", true);
//        }
//        else if (cd.teamNum == 2 && !pc.dead)
//        {
//            player.SetBool("isShoot", true);
//        }
//        player.SetBool("isGrounded", true);
//        player.SetBool("isJump", false);
//        player.SetBool("isFall", false);
//        player.SetBool("isMove", false);
//        player.SetBool("isRun", false);
//        player.SetBool("isLand", false);
//        player.SetBool("isShootMove", false);
//        player.SetBool("isInjureIdle", false);
//    }


//    //public void hiderSet()
//    //{
//    //    this.a = cd.characterNum;
//    //    player = control[this.a];
//    //    Debug.Log(this.a);
//    //}

//    IEnumerator skillC()
//    {
//        yield return new WaitForSeconds(3.0f);
//        cd.skillToggle(true);
//    }


//    IEnumerator startLoad1()
//    {
//        yield return new WaitForSeconds(22.0f);
//        hunterOut.SetActive(true);
//        huntWait.SetActive(false);
//        Debug.Log(startGame);
//        pc.startGame = true;
//        yield return new WaitForSeconds(3.0f);
//        hunterOut.SetActive(false);
//    }


//    IEnumerator startLoad2()
//    {
//        yield return new WaitForSeconds(12.0f);
//        pc.startGame = true;
//        yield return new WaitForSeconds(10.0f);
//        hunterOut.SetActive(true);
//        yield return new WaitForSeconds(3.0f);
//        hunterOut.SetActive(false);
//    }

//    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//    {
//        if (stream.IsWriting)
//        {
//            //stream.SendNext(hp);
//        }
//        else
//        {
//            //hp = (int)stream.ReceiveNext();
//        }
//    }


//    public void aSound(AudioClip aClip/*string animName,*/ /*GameObject thisDoor*/)
//    {
//        AudioSource audio = GetComponent<AudioSource>();
//        audio.clip = aClip;
//        if (!this.GetComponent<AudioSource>().isPlaying && !pc.dead)
//        {
//            audio.Play();
//        }
//    }


//    public void animDo(string act)
//    {
//        player.SetBool(act, true);
//    }

//    public void animStop(string act)
//    {
//        player.SetBool(act, false);
//    }
//}
