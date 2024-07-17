//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//[RequireComponent(typeof(AudioSource))]
//public class playerMove : MonoBehaviour
//{

//    //public Animation anim;
//    public float speed = 5.0f; //character speed
//    public float walk = 5.0f;
//    public float sprint = 10.0f;
//    public AudioClip walkSound;
//    public Slider stamina;
//    public Slider health;

//    public Animator animator; //character animation
//    public Animator staminaBar;
//    public float staminaConsume = 1.0f;
//    public float idleSwapTime = 0.0f;
//    bool canSprint;
//    public bool playerDie;
//    public Transform cam;

//    //public Rigidbody rb;
//    public float jumpForce = 8;

//    public ParticleSystem particles;
//    public int particleTimer = 0;

//    Vector3 moveDirection;
//    float turnVelocity;
//    public float rotationTime = 0.1f; 
//    //public GameObject player;
//    //public Transform playerObj;
//    private CharacterController characterController;
//    //private Vector3 rotateDirection;
//    public float gravity = 9.8f;

//    public GameObject enemy;
//    //public AudioClip jumpScare;

//    //private bool cantLook = false;  //Player starts out able to look

//    // Start is called before the first frame update
//    void Start()
//    {
//        //anim = GetComponent<Animation>();
//        Cursor.lockState = CursorLockMode.Locked;
//        animator = GetComponent<Animator>();
//        characterController = GetComponent<CharacterController>();
//        //rb = GetComponent<Rigidbody>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        health.value = characterData.healthChar;
//        particleTimer++;
//        if (particleTimer > 35)
//        {
//            Pause();
//        }
//        //anim.CrossFade("idle");
//        //Vector3 forward = transform.TransformDirection(Vector3.forward);
//        //float curSpeed = speed * Input.GetAxis("Vertical");
//        //anim.CrossFade("run");
//        //characterController.SimpleMove(forward * curSpeed);


//        if (Input.GetAxisRaw("Vertical") == 0.0 && Input.GetAxisRaw("Horizontal") == 0.0 && characterController.isGrounded && !Input.GetKeyDown(KeyCode.Space))
//        {
//            animator.SetBool("isMove", false);
//            animator.SetBool("isRun", false);
//            animator.SetBool("isJump", false);
//            idleSwapTime = idleSwapTime + 0.05f;
//            if (idleSwapTime > 200)
//            {
//                idleSwapTime = 0;
//            }

//            if (idleSwapTime < 100)
//            {
//                animator.SetBool("isIdle", true);
//            }
//            else
//            {
//                animator.SetBool("isIdle", false);
//            }
//        }
//        else
//        {
//            idleSwapTime = 0.0f;
//            Move();
//        }

//        //if (moonMove.isJS)
//        //{
//        //    cantLook = true;
//        //    if (cantLook == true)
//        //    {

//        //        Vector3 enemyPosition = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
//        //        transform.LookAt(enemyPosition);
//        //        //You have to figure out what speed you want
//        //        StartCoroutine("CamTimer");
//        //        moonMove.isJS = false;
//        //    }

//        //}

//        if (stamina.value < 10 && !Input.GetKey(KeyCode.LeftShift))
//        {
//            stamina.value += staminaConsume * Time.deltaTime * 2;
//            staminaBar.Play("FadeOUTStamina");
//        }

//        //if (Input.GetKey(KeyCode.Z))
//        //{
//        //    anim.CrossFade("kick");
//        //}

//        //if (Input.GetKey(KeyCode.Space) && OnTheGround)
//        //{
//        //    //animator.SetBool("jump", true);
//        //    //anim.CrossFade("jump");
//        //    //anim.Play("jump");
//        //    //anim.Blend("jump", 1.0f, 0.3f);
//        //    rb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
//        //    OnTheGround = false;
//        //}
//    }
    
//    private void Move()
//    {
//        float moveX = Input.GetAxisRaw("Horizontal");
//        float moveZ = Input.GetAxisRaw("Vertical");
//        Vector3 turnDirection = new Vector3(moveX, 0f, moveZ).normalized;

//         if (turnDirection.magnitude >= 0.1f)
//        {
//            float angle = Mathf.Atan2(turnDirection.x, turnDirection.z) * Mathf.Rad2Deg * cam.eulerAngles.y;
//            float smoothTurn = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnVelocity, rotationTime);
//            //playerObj.localRotation = Quaternion.Euler(rotation);
//            transform.rotation = Quaternion.Euler(0f, smoothTurn, 0f);

//            moveDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
           
//        }
 
//        if (stamina.value <= 0)
//        {
//            canSprint = false;
//        }
//        else
//        {
//            canSprint = true;
//        }

//        if (health.value <= 0)
//        {
//            playerDie = true;
//        }
//        else
//        {
//            playerDie = false;
//        }
    

//        if (characterController.isGrounded)
//        {
           
//            //turnDirection = transform.TransformDirection(turnDirection);
//            //moveDirection = Vector3.Lerp(transform.position, playerObj.position, speed * Time.deltaTime);
//            AudioSource audio = GetComponent<AudioSource>();


//            if (Input.GetKey(KeyCode.LeftShift) && moveZ == 1 && canSprint || Input.GetKey(KeyCode.LeftShift) && moveZ == -1 && canSprint)
//            {

//                staminaBar.Play("FadeINStamina");
//                speed = sprint;
//                stamina.value -= staminaConsume * Time.deltaTime * 4;
//                if (stamina.value > 0)
//                {
//                    animator.SetBool("isMove", false);
//                    animator.SetBool("isRun", true);
//                }
//                else
//                {
//                    animator.SetBool("isRun", false);
//                    animator.SetBool("isMove", true);
//                }
//                jumpForce = 5;
//                audio.clip = walkSound;
//                audio.pitch = 1.6f;
//                if (!this.GetComponent<AudioSource>().isPlaying)
//                {
//                    audio.Play();
//                }
//            }
//            else
//            {
//                animator.SetBool("isRun", false);
//                animator.SetBool("isMove", true);
//                speed = walk;
//                jumpForce = 4;
//                //audio.clip = walkSound;
//                //audio.pitch = 0.8f;
//                if (!this.GetComponent<AudioSource>().isPlaying)
//                {
//                    audio.Play();
//                }
//            }

//            if (Input.GetKeyDown(KeyCode.Space))
//            {
//                animator.SetBool("isMove", false);
//                animator.SetBool("isRun", false);
//                animator.SetBool("isJump", true);
//                if (Time.time >= 1 && !characterController.isGrounded)
//                {
//                    animator.SetBool("isJump", false);
//                    animator.SetBool("isFall", true);
//                }
//                else if (Time.time >= 2 && characterController.isGrounded)
//                {
//                    animator.SetBool("isJump", false);
//                    animator.SetBool("isFall", false);
//                    animator.SetBool("isLand", true);
//                }
//                 moveDirection.y += jumpForce;
//            }


//        }

//        moveDirection.y -= gravity * Time.deltaTime;
//        characterController.Move(moveDirection.normalized * speed * Time.deltaTime);

//    }

//    //private void OnCollisionEnter(Collision collision)
//    //{
//    //    if (collision.gameObject.tag == "Animatronic")
//    //    {

//    //    }
//    //}

//    //private void OnTriggerEnter(Collider other)
//    //{
//    //    if (other.gameObject.tag == "battery")
//    //    {
//    //        AudioSource audio = GetComponent<AudioSource>();
//    //        audio.clip = batteryCollectSound;
//    //        audio.Play();
//    //        batteryCollect.charge++;
//    //        Destroy(other.gameObject);
//    //    }
//    //}

//    //IEnumerator CamTimer()
//    //{
//    //    yield return new WaitForSeconds(5.0f);
//    //    cantLook = false; //We stop moving the camera
//    //    yield return new WaitForSeconds(1.0f);
//    //    //Give player back control with mouse     
//    //}

//    private void Pause()
//    {
//        particles.Stop();
//    }
//}
