using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject impactEffect;
    public float radius = 3;
    public int damageAmount = 15;
    Enemy e;

    void Start()
    {
        e = GameObject.Find("Army").GetComponent<Enemy>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        FindObjectOfType<AudioManager>().Play("Explosion");
        GameObject impact  = Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(impact, 2);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObject in colliders)
        {
            if (nearbyObject.tag == "Player")
            {
                nearbyObject.GetComponent<characterData>().lossHP(10);
            }
            else
            {
                e.TakeDamage(5);
            }
        }
        this.enabled = false;
    }
}
