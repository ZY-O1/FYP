using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LaserGunScript : MonoBehaviourPunCallbacks
{
    public TrailRenderer laserTrailPrefab;
    public ParticleSystem hitParticleSystem;
    public Transform originTransform;
    public bool canShoot;

    public AudioSource laserSoundAudioSource;
    //Vector3 lockP;

    void Start()
    {
        canShoot = false;
        photonView.RPC(nameof(Shoot), RpcTarget.All);
    }

    //void LateUpdate()
    //{
    //    lockP = transform.eulerAngles; // keep current rotation about Y
    //    this.transform.eulerAngles = lockP; // restore original rotation with new Y
    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && SwapTeam.team == 2)
        {
            if (Input.GetButtonDown("Fire1") && canShoot)
            {
                //oL.SetActive(true);
                Shoot();
            }
        }
        else
        {
            //oL.SetActive(false);
        }

        if (MenuManager.PvE && canShoot && SwapTeam.team == 1)
        {
            Shoot();
        }
    }

    [PunRPC]
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
        if (Physics.Raycast(ray, out RaycastHit hit,1000f))
        {
            hitPoint = hit.point;
            //var health = hit.collider.gameObject.tagGetComponent<characterData>();
            var propH = hit.collider.GetComponent<Enemy>();
            Instantiate(hitParticleSystem, hit.point, Quaternion.LookRotation(hit.normal));
            if (/*health && health.teamNumC == 1 || */propH && propH.enemyTeam == 1)
            {
               // health.lossHP(10);
                propH.TakeDamage(10);
            }
            else
            {
                this.GetComponentInParent<characterData>().lossHP(5);
            }
        }
        trail.transform.position = hitPoint;
    }

    public void checkShoot(bool shoot)
    {
        canShoot = shoot;
    }
}
