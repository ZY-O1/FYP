using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSpawn : MonoBehaviour
{
    public GameObject potion;
    public Transform[] potionSpawn;
    Transform targets;
    public int potionC = 0;
    public float potionCheck;

    // Start is called before the first frame update
    void Start()
    {


        if (potionC < 6)
        {
            int potionNum = Random.Range(0, potionSpawn.Length - 1);
            this.targets = potionSpawn[potionNum];
            if (this.targets == null)
            {
                Instantiate(potion, this.targets.position, Quaternion.identity);
            }
            potionC += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        potionCheck += Time.deltaTime;
        if (potionCheck > 30)
        {
            int potionNum2 = Random.Range(0, potionSpawn.Length - 1);
            this.targets = potionSpawn[potionNum2];
            if (this.targets == null)
            {
                StartCoroutine(reloadPotion());
            }
            potionCheck = 0;
        }       
    }

    IEnumerator reloadPotion()
    {
        yield return new WaitForSeconds(1.0f);
        //int potionNum2 = Random.Range(0, potionSpawn.Length - 1);
        //this.targets = potionSpawn[potionNum2];
        //if (this.targets == null)
        //{
            Instantiate(potion, this.targets.position, Quaternion.identity);
        //}
    }

}
