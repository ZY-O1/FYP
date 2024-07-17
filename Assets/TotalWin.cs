//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class TotalWin : MonoBehaviour
//{
//    public int totalWin;
//    public Text win;
//    public bool showEnd;
//    result r;
//    public GameObject hiW;
//    public GameObject huW;

//    void Start()
//    {
//        showEnd = false;
//        r = GameObject.FindGameObjectWithTag("Player").GetComponent<result>();

//        if (r.hideWin)
//        {
//            Destroy(r.gameObject);
//            if (GetComponent<characterData>().teamNum== 1)
//            {
//                totalWin += 1;
//            }
//            hiW.SetActive(true);
//        }

//        if (r.huntWin)
//        {
//            Destroy(r.gameObject);
//            if (GetComponent<characterData>().teamNum == 2)
//            {
//                totalWin += 1;
//            }
//            huW.SetActive(true);
//        }
//        StartCoroutine(showResultEnd());
//    }
//    // Update is called once per frame
//    void Update()
//    {
//        win.text = "TotalWin : " + totalWin.ToString();
//    }

//    IEnumerator showResultEnd()
//    {
//        yield return new WaitForSeconds(10.0f);
//        showEnd = true;
//    }

//}
