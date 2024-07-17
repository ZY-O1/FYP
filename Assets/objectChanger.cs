//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using Photon.Pun;
//using Photon.Realtime;

//public class objectChanger : MonoBehaviourPun
//{

//    [SerializeField] public GameObject map1v1;
//    [SerializeField] public GameObject map1v2;
//    [SerializeField] public GameObject map1v3;
//    [SerializeField] public GameObject propSet;
//    [SerializeField] public int propNum = 0;
//    [SerializeField] public float morphTimer = 5;

//    public bool canMorph;

//    characterData cd;
//    TEAM t;

//    void Start()
//    {
//        cd = GetComponent<characterData>();
//        t = GetComponent<TEAM>();
//    }


//    public void propChanger()
//    {
//        if (cd.hp > 0 && t.huntOrHide)
//        {
//            propSet.SetActive(true);
//            if (this.propNum == 3 && this.canMorph)
//            {
//                cd.resetProp = true;
//            }
//            else if (this.propNum == 0 && this.canMorph)
//            {
//                cd.resetProp = false;
//            }

//            if (Input.GetKeyDown(KeyCode.C))
//            {
//                if (this.propNum == 0)
//                {
//                    prop1();
//                }
//                else if (this.propNum == 1 && this.canMorph)
//                {
//                    prop2();
//                }
//                else if (this.propNum == 2 && this.canMorph)
//                {
//                    prop3();
//                }
//                else if (this.propNum == 3 && this.canMorph)
//                {
//                    propReset();
//                }

//                if (this.morphTimer <= 0)
//                {
//                    this.morphTimer = 5f;
//                }
//            }

//            if (this.morphTimer <= 0)
//            {
//                this.canMorph = true;
//            }
//            else
//            {
//                this.canMorph = false;
//            }

//            if (this.morphTimer > 0)
//            {
//                this.morphTimer -= Time.deltaTime;
//            }
//        }
//        else
//        {
//            propSet.SetActive(false);
//        }
//    }

//    public void prop1()
//    {
//        cd.resetProp = false;
//        this.map1v1.SetActive(true);
//        this.propNum = 1;
//    }

//    public void prop2()
//    {
//        this.map1v2.SetActive(true);
//        this.map1v1.SetActive(false);
//        this.propNum = 2;
//    }

//    public void prop3()
//    {
//        this.map1v3.SetActive(true);
//        this.map1v2.SetActive(false);
//        this.propNum = 3;
//    }

//    public void propReset()
//    {
//        this.map1v3.SetActive(false);
//        this.propNum = 0;
//    }

//}
