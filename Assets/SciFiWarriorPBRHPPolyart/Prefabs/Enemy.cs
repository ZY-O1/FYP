using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class Enemy : MonoBehaviour
{
    public int enemyHP = 100;
    public int propN;
    public bool change;
    public bool skillProp;
    public bool canSkill;
    public bool move;

    //public GameObject projectile;
    //public Transform projectilePoint;

    private bool beenHit = false;
    public TrailRenderer laserTrailPrefab;
    public ParticleSystem hitParticleSystem;
    public Transform originTransform;
    public bool canShoot;
    result r;
    public int enemyTeam;
    GameObject player;

    public AudioSource laserSoundAudioSource;
    public AudioClip hitSound;
    public Animator animator;
    public float enemyDistance = 25f;

    void Start()
    {
        if (SwapTeam.team == 1)
        {
            this.enemyTeam = 2;
        }
        else
        {
            this.enemyTeam = 1;
        }
        propN = 0;
        canSkill = true;
        change = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }


    //public void Shoot()
    //{
    //    Rigidbody rb = Instantiate(projectile, projectilePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
    //    rb.AddForce(transform.forward * 30f, ForceMode.Impulse);
    //    rb.AddForce(transform.up * 7, ForceMode.Impulse);
    //}
    void OnBecameVisible()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        Debug.Log("Visible " + distance);
        if (SwapTeam.team == 2 && distance < 15)
        {
            StartCoroutine(propGone());
            Debug.Log("Can See");
        }
    }

    void OnBecameInvisible()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        Debug.Log("inVisible " + distance);
        if (SwapTeam.team == 2 && distance < enemyDistance)
        {
            Debug.Log("PROP!!!");
            if (this.change)
            {
                StartCoroutine(propDelay());
                Debug.Log("Can Not See");
            }
        }
    }

    void Update()
    {
        if (!this.change)
        {
            StartCoroutine(changeProp());
        }
    }



    IEnumerator propDelay()
    {
        yield return new WaitForSeconds(1.0f);
        propNumber();
    }

    IEnumerator propGone()
    {
        yield return new WaitForSeconds(1.0f);
        deactivateMesh();
    }

    //PROP
    public void deactivateMesh()
    {
        if (this.canSkill)
        {
            this.transform.GetChild(1).gameObject.SetActive(false);
            StartCoroutine(skillEffectLast());
        }
    }

    public void propNumber()
    {
        if (this.enemyHP > 0)
        {
            if (this.propN == 0 && this.change)
            {
                this.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
                this.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);          
                this.change = false;
            }
            else if (this.propN == 1 && this.change)
            {               
                this.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
                this.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(true);
                this.change = false;
            }
            else if (this.propN == 2 && this.change)
            {
                this.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(false);
                this.transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(true);
                this.change = false;
            }
            else if (this.propN == 3 && this.change)
            {
                this.transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(false);
                this.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
                this.change = false;
            }
        }
    }

    public void resetP()
    {
        this.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(false);
        this.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
    }

    IEnumerator changeProp()
    {
        int randomProp = Random.Range(0, 3);
        yield return new WaitForSeconds(5.0f);
        this.propN = randomProp;
        this.change = true;
    }

    IEnumerator skillEffectLast()
    {
        this.canSkill = false;
        yield return new WaitForSeconds(10.0f);
        this.skillProp = false;
        this.transform.GetChild(1).gameObject.SetActive(true);
        this.canSkill = true;
    }

    //ARMY
    public void Shoot()
    {
        //play the shoot animation
        //animator.SetTrigger("Shoot");
        //playe the sound
        laserSoundAudioSource.Play();
        var trail = Instantiate(laserTrailPrefab, originTransform.position, originTransform.rotation);
        trail.AddPosition(originTransform.position);
        //cast forward to see where we hit
        var ray = new Ray(originTransform.position, originTransform.forward);
        Vector3 hitPoint = originTransform.position + originTransform.forward * 1000f;
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
        {
            hitPoint = hit.point;
            var health = hit.collider.GetComponent<characterData>();
            Instantiate(hitParticleSystem, hit.point, Quaternion.LookRotation(hit.normal));
            if (health && health.GetComponent<characterData>().teamNumC == 1)
            {
                health.lossHP(10);
            }
            else
            {
                TakeDamage(5);
            }
        }
        trail.transform.position = hitPoint;
    }

    //public void checkShoot(bool shoot)
    //{
    //    canShoot = shoot;
    //}

    public void TakeDamage(int damageAmount)
    {
        enemyHP -= damageAmount;
            if (enemyTeam == 2)
            {
                //HUNTER
                if (this.enemyHP <= 0)
                {
                    animator.SetTrigger("death");
                    r = GameObject.Find("Result").GetComponent<result>();
                    r.huntDie();
                    StartCoroutine(delayDie());
                    GetComponent<CapsuleCollider>().enabled = false;
                }
                else
                {
                    animator.SetTrigger("damage");
                }
            }
            else
            {
                //PROP
                if (enemyHP <= 0)
                {
                    animator.SetTrigger("death");
                    r = GameObject.Find("Result").GetComponent<result>();
                    r.hideDie();
                    StartCoroutine(delayDie());
                    GetComponent<CapsuleCollider>().enabled = false;
                }
                else
                {
                    animator.SetTrigger("damage");
                }
            }
    }

    IEnumerator delayDie()
    {
        yield return new WaitForSeconds(5.0f);
        randomSpawnPoint.spawnA = true;
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (beenHit && collision.gameObject.tag == "Potion")
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = hitSound;
            audio.Play();
            TakeDamage(15);
            beenHit = true;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        beenHit = false;
    }


}
