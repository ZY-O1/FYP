using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialSpawn : MonoBehaviour
{
    public GameObject tutorial;
    public bool canSpawnT;
    public GameObject spawnEne;
    public Transform here;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && canSpawnT)
        {
            spawnEn();
        }
    }

    void spawnEn()
    {
        Instantiate(spawnEne, here.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        if (other.gameObject.tag == "Player")
        {
            tutorial.SetActive(true);
            canSpawnT = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tutorial.SetActive(true);
            canSpawnT = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        tutorial.SetActive(false);
        canSpawnT = false;
    }
}
