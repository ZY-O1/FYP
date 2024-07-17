using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionThrow : MonoBehaviour
{
    public GameObject potionUI;
    public bool havePotion;
    public GameObject potion;
    public float throwForce;


    void Start()
    {
        havePotion = false;
    }

    void Update()
    {
        if (havePotion)
        {
            potionUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                ThrowPotion();
                havePotion = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GetComponent<SwapTeam>().teamNum == 1 && collision.gameObject.tag == "potion")
        {
            havePotion = true;
        }
    }

    void ThrowPotion()
    {
        GameObject temp = Instantiate(potion, transform.position, transform.rotation);
        StartCoroutine("Fade");
        temp.name = "Potion";
        Rigidbody rb = temp.GetComponent<Rigidbody>();
        rb.velocity = transform.TransformDirection(new Vector3(0, 0, throwForce));
        potionUI.SetActive(false);
    }
}
