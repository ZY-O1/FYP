using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endtuto: MonoBehaviour
{
    public GameObject tutorialShow;
    public bool canExit;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && canExit)
        {
            exitMenu();
        }
    }

    void exitMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        MenuManager.exitTuto = true;
    }

    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        if (other.gameObject.tag == "Player")
        {
            tutorialShow.SetActive(true);
            canExit = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tutorialShow.SetActive(true);
            canExit = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        tutorialShow.SetActive(false);
        canExit = false;
    }
}