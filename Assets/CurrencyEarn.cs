using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class CurrencyEarn : MonoBehaviourPunCallbacks
{
    public static CurrencyEarn instanceC;
    characterData cd;
    result r;
    public bool earnOnce;
    public bool showOnce;
    public Text coinsVal;
    public int cash;

    Scene curR;
    string sceneR;

    void Awake()
    {
        if (instanceC != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instanceC = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        curR = SceneManager.GetActiveScene();
        sceneR = curR.name;
        earnOnce = true;

        if (sceneR == "result" && photonView.IsMine)
        {
            r = GameObject.FindGameObjectWithTag("Player").GetComponent<result>();
            cd = GameObject.FindGameObjectWithTag("Player").GetComponent<characterData>();
        }

        if (sceneR == "Menu")
        {
            showOnce = true;
        }
     }

    // Update is called once per frame
    void Update()
    {
        if (sceneR == "Menu")
        {       
            if (showOnce)
            {
                instanceC.GetVirtualCurrencies();
                showOnce = false;
            }
        }
    }

    public void earnCurrency(int earn)
    {
        var request = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = "CN",
            Amount = earn
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnGrantCoinsSuccess, OnError);
    }

    void OnGrantCoinsSuccess(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("Success");
        instanceC.GetVirtualCurrencies();
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error: " + error.ErrorMessage);
    }

    ///SHOW CURRENCY//
    public void GetVirtualCurrencies()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnErrorGet);
    }

    void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        int coins = result.VirtualCurrency["CN"];
        if (coins == null)
        {
            coins = 0;
        }
        coinsVal.text = coins.ToString();
        cash = coins;
    }


    void OnErrorGet(PlayFabError error)
    {
        Debug.Log("Error On Getting Currencies");
    }

}
