//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using Photon.Pun;

//[RequireComponent(typeof(AudioSource))]
//public class TPControl : MonoBehaviour
//{

//    PhotonView view;

//    [SerializeField] Camera cam_;
//    [SerializeField] public CharacterController controller;
//    [SerializeField] public Transform cam;
//    [SerializeField] public Transform target;

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

//    [SerializeField] public GameObject map1v1;
//    [SerializeField] public GameObject map1v2;
//    [SerializeField] public GameObject map1v3;
//    [SerializeField] public int propNum = 0;
//    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

//    //MOVEMENTS
//    public float speed = 0;
//    public float gravity = 9.81f;
//    public float jumpHeight = 10.0f;
//    public float velocity;
//    Vector3 moveDir;

//    Vector3 moveVec;
//    public bool isGrounded;

//    //public Transform groundCheck;
//    //public float groundDistance = 0.4f;
//    //public LayerMask groundMask;

//    float turnSmoothVelocity;
//    public float turnSmoothTime = 0.1f;
//    public bool injureMove;

//    public float walk = 5.0f;
//    public float injureWalk = 2.5f;
//    public float sprint = 8.0f;
//    public AudioClip sprintSound;
//    public AudioClip jumpSound;

//    public float? jumpBtnPressed;
//    public float? lastGroundedTime;
//    public bool isJumping;
//    public float jumpBtnGrace = 0;
//    private float oriStepOffset;

//    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//


//    public Slider stamina;
//    public Slider energy;
//    public Slider skillCD;
//    public float energyLeak;
//    public ParticleSystem particles;
//    public float particleTimer;


//    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
//    //Script Passing Var
//  //  characterSelect cs;
//    randomSpawnPoint rsp;
//    interact i;
//    countDownStart cds;
//    characterData cd;
//    result r;
//    healthDisplay hd;

//    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
//    public Animator[] control;
//    public Animator player;//character animation
//    public int a;

//    public Animator staminaBar;
//    public Animator blood;
//    public Animator energyBar;

//    public float staminaConsume = 1.0f;
//    public float idleSwapTimer;
//    bool canSprint;
//    public bool canMorph;
//    public float morphTimer = 0f;

//    [SerializeField]
//    public bool hiderDie;
//    public bool hunterDie;
//    public bool whacked;
//    public bool startGame;
//    public bool instantDead;
//    public bool runOnce;

//    void Start()
//    {
//        Pause();
//        cam_ = GameObject.Find("MAINCAM").GetComponent<Camera>();
//        controller = GetComponent<CharacterController>();
//        controller.enabled = true;
//        player = GetComponent<Animator>();
//        view = GetComponent<PhotonView>();
//        if (!view.IsMine)
//        {
//            //Destroy(cam);
//            controller.enabled = false;
//            cam_.enabled = false;
//            canvas.SetActive(false);
//        }
//        Cursor.lockState = CursorLockMode.Locked;
      
//        runOnce = true;
//        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
//        r = this.GetComponentInChildren<result>();
//        rsp = GameObject.FindWithTag("spawn").GetComponent<randomSpawnPoint>();
//        i =  GetComponent<interact>();
//        cds = GameObject.FindGameObjectWithTag("startCount").GetComponent<countDownStart>();
//        cd = GetComponent<characterData>();
//        hd = GetComponentInChildren<healthDisplay>();
//       // cs = GetComponentInChildren<characterSelect>();
//        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
//        startGame = false;
//        canMorph = true;
//        skillCD.value = 10;
//        skillCD.maxValue = 10;
//        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

//        oriStepOffset = controller.stepOffset;
//        //rb = GetComponent<Rigidbody>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (!view.IsMine && PhotonNetwork.IsConnected == true)
//        {
//            return;
//        }

//        //if (view.IsMine)
//        //{
//            if (cd.teamNum == 1 && runOnce)
//            {
//                hiderSet();
//                cd.countS = 10f;
//                this.hunter.SetActive(false);
//                this.hider.SetActive(true);
//                StartCoroutine(startLoad2());
//                runOnce = false;
//            }
//            else if (cd.teamNum == 2 && runOnce)
//            {
//                player = control[2];
//                cd.countS = 20f;
//                this.huntWait.SetActive(true);
//                this.hider.SetActive(false);
//                this.hunter.SetActive(true);
//                StartCoroutine(startLoad1());
//                runOnce = false;
//            }

//            if (cd.teamNum == 1 && !r.hideWin && !r.huntWin)
//            {
//                this.instantDead = false;
//                this.energyB.SetActive(true);
//                this.missionB.SetActive(true);
//                this.skill.SetActive(true);
//                player.SetBool("isSWAT", false);
//                teamHide();
//            }
//            else if (cd.teamNum == 2 && !r.hideWin && !r.huntWin)
//            {
//                this.instantDead = false;
//                this.energyB.SetActive(false);
//                this.missionB.SetActive(false);
//                this.skill.SetActive(false);
//                player.SetBool("isSWAT", true);
//                teamHunt();
//            }
//        //}     
//    }

//    private void teamHunt()
//    {

//        player.SetBool("isIdle", false);
//        AudioSource audio = GetComponent<AudioSource>();
//        //almostDie
//        if (cd.hp <= 0 && !this.cd.gameStart && cd.dieTimer > 0 && !this.rsp.respawn && this.target.position.y > -2)
//        {
//            this.akmShoot.SetActive(false);
//            this.injureMove = true;
//            if (cd.dieTimer == 15)
//            {
//                this.whacked = true;
//                player.Play("injureIdle");
//            }
//            injureAnim();
//            cd.dieTimer -= Time.deltaTime;
//            // Debug.Log(dieTimer);
//        }

//        if (cd.hp > 0 && !this.hunterDie)
//        {
//            cd.dieTimer = 15f;
//            this.injureMove = false;
//        }

//        //dead
//        if (cd.dieTimer <= 0 && !this.rsp.respawn && cd.hp <= 0 || cd.hp <= 0 && this.target.position.y < -2 && !cd.gameStart)
//        {
//            this.instantDead = true;
//            this.blood.SetBool("isDying", false);
//            this.energy.value = 0f;
//            deadAnim();
//            this.injureMove = false;       
//        }
//        else
//        {
//            this.hunterDie = false;
//            player.SetBool("isDead", false);
//            player.SetBool("isInjureIdle", false);
//            player.SetBool("isInjureMove", false);
//        }

//        if (!this.hunterDie && cd.dieTimer <= 0)
//        {
//            //avatarSwapper();
//            //this.player.Play("Idle");
//            //player.SetBool("isIdle", true);
//            this.injureMove = false;
//        }

//        if (this.particleTimer > 0)
//        {
//            this.particleTimer -= Time.deltaTime;
//        }
//        if (this.particleTimer <= 0)
//        {
//            Pause();
//        }

//        if (this.injureMove && cd.dieTimer > 0 && !this.instantDead) //almost die
//        {
//            this.blood.SetBool("isDying", true);
//            if (Input.GetAxisRaw("Vertical") == 0.0 && Input.GetAxisRaw("Horizontal") == 0.0 && controller.isGrounded)
//            {
//                this.speed = 0f;
//                player.SetBool("isInjureIdle", true);
//            }
//            else
//            {
//                player.SetBool("isInjureIdle", false);
//                //player.SetBool("isFall", false);
//                //player.SetBool("isLand", false);
//                injureMoving();
//            }
//        }
//        else // below is normal move
//        {
//            this.akmShoot.SetActive(true);
//            if (Input.GetAxisRaw("Vertical") == 0.0 && Input.GetAxisRaw("Horizontal") == 0.0 && controller.isGrounded && !Input.GetKeyDown(KeyCode.Space) && !hunterDie )
//            {
//                noMove();
//                player.SetBool("isShoot", true);
//            }
//            else 
//            {
//                player.SetBool("isShoot", false);
//                if (Input.GetButton("Fire1") && cd.teamNum == 2 && !Input.GetMouseButton(1) && !this.hunterDie)
//                {
//                    player.SetBool("isShootMove", true);
//                }
//                else
//                {
//                    player.SetBool("isShootMove", false);
//                }
//                this.idleSwapTimer = 20;
//                player.SetBool("isLook", false);
//                if (!this.hunterDie)
//                {
//                    //player.SetBool("isFall", false);
//                    //player.SetBool("isLand", false);
//                    Move();
//                }
//            }
//        }


//        staminaManage();
//        moveVec = new Vector3(0, velocity, 0);
//        controller.Move(moveDir.normalized * speed * Time.deltaTime);
//        controller.Move(moveVec * Time.deltaTime);
//    }

//    private void teamHide()
//    {
//        AudioSource audio = GetComponent<AudioSource>();
//        //almostDie
//        if (this.cd.hp <= 0 && !this.cd.gameStart && cd.dieTimer > 0 && !this.rsp.respawn && this.target.position.y > -2)
//        {
//            this.map1v2.SetActive(false);
//            this.map1v1.SetActive(false);
//            this.map1v3.SetActive(false);
//            this.canMorph = false;
//            this.injureMove = true;
//            if (cd.dieTimer == 15 && controller.enabled == true)
//            {
//                this.whacked = true;
//                this.player.Play("injureIdle");
//            }
//            injureAnim();
//            cd.dieTimer -= Time.deltaTime;
//            // Debug.Log(dieTimer);
//        }
//        else
//        {
//            if (cd.hp > 0)
//            {
//                if (this.propNum == 3 && this.canMorph)
//                {
//                    cd.resetProp = true;
//                }
//                else if(this.propNum == 0 && this.canMorph)
//                {
//                    cd.resetProp = false;
//                }

//                if (Input.GetKeyDown(KeyCode.C))
//                {
//                    if (this.propNum == 0)
//                    {
//                        prop1();
//                    }
//                    else if (this.propNum == 1 && this.canMorph)
//                    {
//                        prop2();
//                    }
//                    else if (this.propNum == 2 && this.canMorph)
//                    {
//                        prop3();
//                    }
//                    else if (this.propNum == 3 && this.canMorph)
//                    {
//                        propReset();
//                    }

//                    if (this.morphTimer <= 0)
//                    {
//                        this.morphTimer = 5f;
//                    }
//                }
//            }

//            if (this.morphTimer <= 0)
//            {
//                this.canMorph = true;
//            }
//            else
//            {
//                this.canMorph = false;
//            }

//             if (this.morphTimer > 0)
//            {
//                this.morphTimer -= Time.deltaTime;
//            }
//        }

//        if (cd.hp > 0 && !this.hiderDie)
//        {
//            cd.dieTimer = 15f;
//            this.injureMove = false;
//            this.blood.SetBool("isDying", false);
//        }

//        //dead
//        if (cd.dieTimer <= 0 && !rsp.respawn && cd.hp <= 0 || cd.hp <= 0 && this.target.position.y < -2 && !cd.gameStart)
//        {
//            this.blood.SetBool("isDying", false);
//            this.energy.value = 0f;
//            propOff();
//            deadAnim();
//            this.injureMove = false;
//            deadSet();
//        }
//        else
//        {
//            this.hiderDie = false;
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

//        if (this.injureMove && cd.dieTimer > 0) //almost die
//        {
//            this.blood.SetBool("isDying", true);
//            if (Input.GetAxisRaw("Vertical") == 0.0 && Input.GetAxisRaw("Horizontal") == 0.0 && controller.isGrounded)
//            {
//                this.speed = 0f;
//                player.SetBool("isInjureIdle", true);
//            }
//            else
//            {
//                player.SetBool("isInjureIdle", false);
//                //player.SetBool("isFall", false);
//                //player.SetBool("isLand", false);
//                injureMoving();
//            }
//        }
//        else // below is normal move
//        {
//            if (Input.GetAxisRaw("Vertical") == 0.0 && Input.GetAxisRaw("Horizontal") == 0.0 && controller.isGrounded && !Input.GetKeyDown(KeyCode.Space) && !hiderDie)
//            {
//                noMove();
//            }
//            else
//            {
//                this.idleSwapTimer = 20;
//                player.SetBool("isLook", false);
//                if (!this.hiderDie)
//                {
//                    //player.SetBool("isFall", false);
//                    //player.SetBool("isLand", false);
//                    Move();
//                }
//            }
//        }


//        staminaManage();
//        energyManage();

//        moveVec = new Vector3(0, velocity, 0);
//        controller.Move(moveDir.normalized * speed * Time.deltaTime);
//        controller.Move(moveVec * Time.deltaTime);
//    }

//    private void Move()
//    {
//        AudioSource audio = GetComponent<AudioSource>();
//        //controller.Move(velocity * Time.deltaTime);
//        //walk
//        float horizontal = Input.GetAxisRaw("Horizontal");
//        float vertical = Input.GetAxisRaw("Vertical");
//        velocity += -gravity * Time.deltaTime;
//        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
//        if (controller.isGrounded && Input.GetMouseButton(1) && direction.z != 0 && this.canSprint && direction.x != 0 ||
//            controller.isGrounded && Input.GetMouseButton(1) && direction.x != 0 && this.canSprint ||
//            controller.isGrounded && Input.GetMouseButton(1) && direction.z != 0 && this.canSprint)
//        {
//            this.staminaBar.Play("FadeINStamina");

//            if (this.stamina.value > 0 && this.startGame)
//            {
//                this.speed = sprint;
//                this.stamina.value -= staminaConsume * Time.deltaTime * 4;
//                player.SetBool("isMove", false);
//                player.SetBool("isRun", true);
//                audio.pitch = 1.6f;
//                aSound(sprintSound);
//            }
//            else if (stamina.value <= 0 && this.startGame)
//            {
//                this.speed = walk;
//                this.stamina.value = 0;
//                player.SetBool("isRun", false);
//            }        
//        }
//        else if (direction.x != 0 && controller.isGrounded || controller.isGrounded && direction.z != 0)
//        {
//            player.SetBool("isRun", false);
//            player.SetBool("isMove", true);
//            if (this.startGame)
//            {
//                this.speed = walk;
//                audio.pitch = 0.8f;
//                aSound(sprintSound);
//            }
//        }

//        if (direction.x == 0 && direction.z == 0)
//        {
//            player.SetBool("isMove", false);
//            player.SetBool("isRun", false);

//            if (cd.teamNum == 1 && !this.hiderDie && controller.enabled)
//            {
//                player.SetBool("isIdle", true);
//            }
//            else if (cd.teamNum == 2 && !this.hunterDie && controller.enabled)
//            {
//                player.SetBool("isShoot", true);
//            }
//            this.speed = 0f;
//        }

//        ///
//        if (controller.isGrounded)
//        {
//            this.lastGroundedTime = Time.time;

//            if (Input.GetKeyDown(KeyCode.Space))
//            {
//                player.SetBool("isShoot", false);
//                this.jumpBtnPressed = Time.time;
//                audio.pitch = 1;
//                aSound(jumpSound);
//            }
//        }


//        if (Time.time - this.lastGroundedTime <= jumpBtnGrace)
//        {
//            controller.stepOffset = oriStepOffset;
//            this.velocity = -0.5f;
//            //player.SetBool("isFall", false);
//            player.SetBool("isGrounded", true);
//            player.SetBool("isLand", false);
//            //isGrounded = true;
//            player.SetBool("isJump", false);
//            this.isJumping = false;
//            player.SetBool("isFall", false);

//            if (Time.time - this.jumpBtnPressed <= jumpBtnGrace)
//            {
//                velocity = jumpHeight;
//                player.SetBool("isJump", true);
//                this.isJumping = true;
//                this.jumpBtnPressed = null;
//                this.lastGroundedTime = null;
//            }
//        }
//        else
//        {
//            controller.stepOffset = 0;
//            player.SetBool("isGrounded", false);
//            //isGrounded = false;
//            if (velocity < -2.5f)
//            {
//                player.SetBool("isFall", true);
//                velocity += -0.25f * Time.deltaTime;
//            }
//            if (isJumping && velocity < 0)
//            {
//                player.SetBool("isFall", true);
//                player.SetBool("isLand", true);
//            }
//        }
//        ///

//        if (direction.magnitude >= 0.1f && cd.hp > 0)
//        {
//            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
//            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
//            transform.rotation = Quaternion.Euler(0f, angle, 0f);

//            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
//        }

//        if (this.stamina.value <= 0)
//        {
//            this.canSprint = false;
//        }
//        else
//        {
//            this.canSprint = true;
//        }
//    }

//    private void noMove()
//    {
//        this.speed = 0f;
//        stopAllMove();

//        if (cd.teamNum == 1)
//        {
//            hide();
//        }
//        //else
//        //{
//        //    hunt();
//        //}
       
//    }

//    private void injureMoving()
//    {
//        AudioSource audio = GetComponent<AudioSource>();
//        //controller.Move(velocity * Time.deltaTime);
//        //walk
//        float horizontal = Input.GetAxisRaw("Horizontal");
//        float vertical = Input.GetAxisRaw("Vertical");
//        velocity += -gravity * Time.deltaTime;
//        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
       
//        if (direction.x != 0 && controller.isGrounded || controller.isGrounded && direction.z != 0)
//        {
//            player.SetBool("isInjureMove", true);
//            player.SetBool("isInjureIdle", false);
//            this.speed = injureWalk;
//            audio.pitch = 0.5f;
//            aSound(sprintSound);
  
//        }

//        if (direction.x == 0 && direction.z == 0)
//        {
//            //player.SetBool("isMove", false);
//            //player.SetBool("isRun", false);
//            player.SetBool("isInjureMove", false);
//            player.SetBool("isInjureIdle", true);
//            speed = 0f;
//        }

//        if (direction.magnitude >= 0.1f && !hiderDie)
//        {
//            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
//            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
//            transform.rotation = Quaternion.Euler(0f, angle, 0f);

//            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
//        }
//    }

//    private void Pause()
//    {
//        this.particles.Stop();
//        this.exposeSpot.SetActive(false);
//    }

//    private void Continue()
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
//        if (cd.teamNum == 2)
//        {
//            this.hunterDie = true;
//        }
//        else
//        {
//            this.hiderDie = true;
//        }
//        player.SetBool("isDead", true);
//        player.SetBool("isInjureIdle", false);
//        player.SetBool("isShoot", false);
//        player.SetBool("isIdle", false);
//        player.SetBool("isFall", false);
//        player.SetBool("isInjureMove", false);
//        deadSet();
//    }

//    private void staminaManage()
//    {

//        if (this.stamina.value < 10 && !Input.GetMouseButton(1))
//        {
//            this.stamina.value += staminaConsume * Time.deltaTime * 2;
//            this.staminaBar.Play("FadeOUTStamina");
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
//            if (this.energy.value < 100 && !this.hiderDie && this.i.energyMinus == false)
//            {
//                Pause();
//                this.energy.value += energyLeak * Time.deltaTime;
//                this.energyBar.SetBool("isLeak", false);
//                //this.leaked = false;
//            }
//            else if (this.energy.value >= 100 && !this.hiderDie)
//            {
//                //this.leaked = true;
//                Continue();
//                this.energyBar.SetBool("isLeak", true);
//            }
//        }

//        if (Input.GetKeyDown(KeyCode.E) && this.skillCD.value == 10)
//        {
//            this.hider.SetActive(false);
//            cd.skill1 = true;
//            this.skillCD.value = 0;
//        }

//        if (this.skillCD.value < 10)
//        {
//            this.skillCD.value +=  0.25f * energyLeak * Time.deltaTime;
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

//    private void propOff()
//    {
//        this.map1v1.SetActive(false);
//        this.map1v2.SetActive(false);
//        this.map1v3.SetActive(false);
//    }

//    private void prop1()
//    {
//        cd.resetProp = false;
//        this.map1v1.SetActive(true);
//        this.propNum = 1;
//    }

//    private void prop2()
//    {
//        this.map1v2.SetActive(true);
//        this.map1v1.SetActive(false);
//        this.propNum = 2;
//    }

//    private void prop3()
//    {
//        this.map1v3.SetActive(true);
//        this.map1v2.SetActive(false);
//        this.propNum = 3;
//    }

//    private void propReset()
//    {
//        this.map1v3.SetActive(false);
//        this.propNum = 0;    
//    }

//    private void deadSet()
//    {
//        this.energyB.SetActive(false);
//        this.missionB.SetActive(false);
//        this.rsp.canCount = true;
//        controller.enabled = false;
//    }

//    private void stopAllMove()
//    {
//        if (cd.teamNum == 1 && !this.hiderDie)
//        {
//            player.SetBool("isIdle", true);
//        }
//        else if (cd.teamNum == 2 && !this.hunterDie)
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


//    void aSound(AudioClip aClip/*string animName,*/ /*GameObject thisDoor*/)
//    {
//        AudioSource audio = GetComponent<AudioSource>();
//        audio.clip = aClip;
//        if (!this.GetComponent<AudioSource>().isPlaying && !hiderDie)
//        {
//            audio.Play();
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
//            player.SetBool("isLook", true);
//            player.SetBool("isIdle", false);
//        }
//        else
//        {
//            player.SetBool("isLook", false);
//            player.SetBool("isIdle", true);
//        }
//    }

//    public void hiderSet()
//    {
//        this.a = cd.characterNum;
//        player = control[this.a];
//        Debug.Log(this.a);
//    }

//    IEnumerator skillC()
//    {
//        yield return new WaitForSeconds(3.0f);
//        cd.skill1 = false;       
//    }

//    IEnumerator bulletDmg()
//    {
//        yield return new WaitForSeconds(0.5f);
//        hd.shot = false;
//    }

//    IEnumerator startLoad1()
//    {
//        yield return new WaitForSeconds(22.0f);
//        this.hunterOut.SetActive(true);
//        this.huntWait.SetActive(false);
//        this.startGame = true;
//        yield return new WaitForSeconds(3.0f);
//        this.hunterOut.SetActive(false);
//    }


//    IEnumerator startLoad2()
//    {
//        yield return new WaitForSeconds(12.0f);
//        this.startGame = true;
//        yield return new WaitForSeconds(10.0f);
//        this.hunterOut.SetActive(true);
//        yield return new WaitForSeconds(3.0f);
//        this.hunterOut.SetActive(false);
//    }

//    //void hunt()
//    //{
//    //    if (idleSwapTimer > 0)
//    //    {
//    //        idleSwapTimer -= Time.deltaTime;
//    //    }
//    //    else if (idleSwapTimer <= 0 || Input.GetButton("Fire1"))
//    //    {
//    //        idleSwapTimer = 20;
//    //    }

//    //    if (idleSwapTimer >= 5 && idleSwapTimer <= 10 && !Input.GetButton("Fire1"))
//    //    {
//    //        player.SetBool("isLook", true);
//    //        player.SetBool("rifle", false);
//    //    }
//    //    else
//    //    {
//    //        player.SetBool("isLook", false);
//    //        player.SetBool("rifle", true);
//    //    }
//    //}

//    //private void OnTriggerEnter(Collider other) // to see when the player enters the collider
//    //{
//    //    destroyBullet bullet = other.gameObject.transform.GetComponent<destroyBullet>();
//    //    if (bullet && cd.teamNum == 1) //on the object you want to interact
//    //    {
//    //        Debug.Log("getDMG");
//    //        hd.shot = true;
//    //        StartCoroutine(bulletDmg());
//    //    }
//    //}


//    //private void OnTriggerExit(Collider other)// to see when the player enters the collider
//    //{
//    //   // Debug.Log("StopDMG");
//    //}
////}