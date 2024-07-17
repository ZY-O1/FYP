using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomInstantiate : MonoBehaviour
{
    public GameObject spawner;
    Vector3 stand;
    public float spawnQuantity = 1;
    public GameObject prop;
    public int spawn;
    // Start is called before the first frame update
    //void Start()
    //{
     
    //}

    // Update is called once per frame
    void Update()
    {
        if (this.spawnQuantity > 0)
        {
          for (var  i = 0; i < 1; i++)
          {
            this.spawn = Random.Range(0, 100);
            //Debug.Log(Random.Range(0, 100));
            stand = new Vector3(0, 0.5f, 0);
            if (this.spawn >= 50)
            {
              Instantiate(this.prop);
              this.prop.transform.position = this.spawner.transform.position + stand;
            }
            else
            {
                  //do nothing
            }
           }
           this.spawnQuantity -= 1;
        }
    }
}
