using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


[RequireComponent(typeof(AudioSource))]
public class ballDmg : MonoBehaviourPunCallbacks
{
    AudioClip whack;

    [PunRPC]
    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (other.gameObject.tag == "Player" && wreckSmash.spinTimer <= 10) //on the object you want to interact
        {
            Debug.Log("Ouch, That's hurt.");
            audio.clip = whack;
            audio.Play();
            other.gameObject.GetComponent<characterData>().lossHP(25);
        }

        if (other.gameObject.tag == "Enemy" && wreckSmash.spinTimer <= 10) //on the object you want to interact
        {
            Debug.Log("Ouch, That's hurt.");
            audio.clip = whack;
            audio.Play();
            other.gameObject.GetComponent<Enemy>().TakeDamage(25);
        }
    }

    [PunRPC]
    private void OnTriggerExit(Collider other) // to see when the player enters the collider
    {
        if (other.gameObject.tag == "Player") //on the object you want to interact
        {
         //
        }
    }
}
