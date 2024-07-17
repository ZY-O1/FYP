using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class trapOilTank : MonoBehaviourPunCallbacks
{
    public float explodeTimer;
    public AudioClip setSound;
    public AudioClip explodeSound;
    public bool inRange;
    public bool canExplode;
    public bool canSet;
    public bool getDmg;

    //public float explodeForce, radius;
    public bool setBomb;
    Vector3 explodePosition;

    // public float radius;

    public ParticleSystem explosion;
    public ParticleSystem setExplode;
    // Start is called before the first frame update
    void Start()
    {
        canSet = true;
        canExplode = false;
        explodeTimer = 10f;
        explosion.Stop();
        setExplode.Stop();

        if (!MenuManager.PvE)
        {
            photonView.RPC(nameof(trap), RpcTarget.AllBuffered);
        }

    }

    // Update is called once per frame

    void Update()
    {
        trap();
    }

    [PunRPC]
    void trap()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (this.canSet && this.setBomb && this.inRange)
        {
            Debug.Log("LetsGo");
            setExplode.Play();
            audio.clip = setSound;
            if (!this.GetComponent<AudioSource>().isPlaying)
            {
                audio.Play();
            }
            this.canSet = false;
            this.setBomb = false;
        }

        if (!this.canSet && this.explodeTimer > 0)
        {
            this.explodeTimer -= Time.deltaTime;
            //Debug.Log(explodeTimer);
        }

        if (this.inRange && this.explodeTimer <= 0 && this.getDmg)
        {
            audio.clip = explodeSound;
            if (!this.GetComponent<AudioSource>().isPlaying)
            {
                audio.Play();
            }
            this.getDmg = false;
        }

        if (this.explosion.isPlaying)
        {
            this.explodeTimer += 10;
        }

        if (this.explodeTimer > 10)
        {
            this.canSet = true;
            this.explodeTimer = 10;
            this.explosion.Stop();
        }
    }

    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {

        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("enemyTouch");
            this.inRange = true;
        }
        else if (other.gameObject.tag == "Player")
        {
            this.inRange = true;
        }

        if (other.gameObject.tag == "Player" && this.explodeTimer <= 0) //on the object you want to interact
        {
            other.gameObject.GetComponent<characterData>().lossHP(25);
            this.getDmg = true;
        }
        else if (other.gameObject.tag == "Enemy" && this.explodeTimer <= 0) //on the object you want to interact
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(5);
            this.getDmg = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var trap = other.gameObject.GetComponent<interact>();
        if (other.gameObject.tag == "Player" && trap.trapSet)
        {
            this.setBomb = true;
        }

        if (other.gameObject.tag == "Enemy")
        {
            this.setBomb = true;
        }

        if (other.gameObject.tag == "Player" && this.explodeTimer <= 0) //on the object you want to interact
        {
            this.explosion.Play();
            other.gameObject.GetComponent<characterData>().lossHP(25);
            this.getDmg = false;
        }
        else if(other.gameObject.tag == "Enemy" && this.explodeTimer <= 0) //on the object you want to interact
        {
            this.explosion.Play();
            other.gameObject.GetComponent<Enemy>().TakeDamage(25);
            this.getDmg = false;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        this.inRange = false;
        this.getDmg = false;
    }
}

