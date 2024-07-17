using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class characterSelect : MonoBehaviourPunCallbacks
{
    public static characterSelect instancing;
    public GameObject greenFrame;
    //public static int charN;
    public AudioClip beep;
    public Text p;
    //randomSpawnPoint rsp;
    //characterData cd;
    public int buy;
    public int charList;
    public bool[] purchased;
    public GameObject notEnough;
    //public bool skin1;

    [SerializeField]
    public int charIndex;

    public GameObject[] characterList;
    //Player player;
    // Start is called before the first frame update
    //PhotonView view;
    //ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

    void Awake()
    {
        instancing = this;
    }

    private void Start()
    {
        charIndex = PlayerPrefs.GetInt("characterSelected");
        characterList = new GameObject[transform.childCount];
        purchased = new bool[characterList.Length];
 

        //Add in models into the array
        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }
        //deactive toggler
        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }

        //activate current
        if (characterList[charIndex])
        {
            Debug.Log(charIndex);
            characterList[charIndex].SetActive(true);
            purchased[charIndex] = false;
        }
        GetUserInventory();
    }

    //public void SetPlayerInfo(Player player_)
    //{
    //    player = player_;
    //    UpdatePlayerItems(player);
    //}

    public void Update()
    {
        if (charIndex == PlayerPrefs.GetInt("characterSelected") && purchased[charIndex] == true)
        {
            greenFrame.SetActive(true);
        }
        else
        {
            greenFrame.SetActive(false);
        }
        //
        buy = charIndex * 500;
        p.text = buy.ToString();
    }


    void playBtnSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = beep;
        audio.Play();
    }


    public void NextBtnClicked()
    {
        GetUserInventory();
        playBtnSound();
        characterList[charIndex].SetActive(false);
        charIndex++;
        //tc.hiderAdd();
        if (charIndex == characterList.Length)
        {
            charIndex = 0;
            //charIndex = characterList.Length + 1;
        }

        if (charIndex > characterList.Length)
        {
            charIndex = 0;
            PlayerPrefs.SetInt("characterSelected", charIndex);
        }
        Debug.Log(charIndex);
        characterList[charIndex].SetActive(true);
    }

    public void LastBtnClicked()
    {
        GetUserInventory();
        playBtnSound();
        characterList[charIndex].SetActive(false);
        charIndex--;
        //tc.hiderMinus();

        if (charIndex < 0)
        {
            charIndex = characterList.Length - 1;
        }
        characterList[charIndex].SetActive(true);

    }

    public void ChooseBtnClicked()
    {
        //if(!skin1 && 
        playBtnSound();
        GetUserInventory();
        if (CurrencyEarn.instanceC.cash >= buy && purchased[charIndex] == false)
        {
            if (CurrencyEarn.instanceC.cash - buy < 0)
            {
                StartCoroutine(notE());
            }
            else
            {       
                MakePurchase(charIndex.ToString(), buy);
            }
        }
        else if (CurrencyEarn.instanceC.cash < buy && purchased[charIndex] == false)
        {
            StartCoroutine(notE());
        }
        else if (purchased[charIndex] == true)
        {
            PlayerPrefs.SetInt("characterSelected", charIndex);
            SwapTeam.character = charIndex;
        }
        //playerProperties["characterList[charIndex]"] = characterList[charIndex];
        //PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        //charN = charIndex;
    }


    //void deduceCurrency()
    //{
    //    var request = new SubtractUserVirtualCurrencyRequest
    //    {
    //        VirtualCurrency = "CN",
    //        Amount = buy
    //    };
    //    PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnSubtractCoinsSuccess, OnError);
    //}

    //void OnSubtractCoinsSuccess(ModifyUserVirtualCurrencyResult result)
    //{
    //    PlayerPrefs.SetInt("characterSelected", charIndex);
    //    SwapTeam.character = charIndex;
    //    purchased[charIndex] = true;
    //    CurrencyEarn.instanceC.GetVirtualCurrencies();
    //}

    //void OnError(PlayFabError error)
    //{
    //    Debug.Log("Error On Getting Currencies");
    //}


    IEnumerator notE()
    {
        notEnough.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        notEnough.SetActive(false);
    }

    //public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable playerProperties)
    //{
    //    if (player == targetPlayer)
    //    {
    //        UpdatePlayerItems(targetPlayer);
    //    }
    //}

    //void UpdatePlayerItems(Player player)
    //{
    //    if (player.CustomProperties.ContainsKey("charIndex"))
    //    {
    //        characterList[charIndex] = characterList[(int)player.CustomProperties["charIndex"]];
    //        playerProperties["charIndex"] = player.CustomProperties["charIndex"];
    //    }
    //    else
    //    {
    //        playerProperties["charIndex"] = 0;
    //    }
    //}

    void GetUserInventory()
    {
        GetUserInventoryRequest request = new GetUserInventoryRequest();
        PlayFabClientAPI.GetUserInventory(request, OnGetSuccess, OnGetError);
    }

    void OnGetSuccess(GetUserInventoryResult result)
    {
        //deduceCurrency();
        foreach (ItemInstance item in result.Inventory)
        {
            if (item.ItemId == charIndex.ToString())
                purchased[charIndex] = true;
            else
                purchased[charIndex] = false;
        }
    }

    void OnGetError(PlayFabError error)
    {
    
        Debug.Log("Error On Purchase" + error);
    }

    void MakePurchase(string name, int price)
    {
        PurchaseItemRequest request = new PurchaseItemRequest();
        request.StoreId = "c";
        request.CatalogVersion = "1";
        request.VirtualCurrency = "CN";
        request.ItemId = name;
        request.Price = price;

        PlayFabClientAPI.PurchaseItem(request, OnPurchaseSuccess, OnPurchaseError);
    }

    void OnPurchaseSuccess(PurchaseItemResult result)
    {
        //deduceCurrency();
        purchased[charIndex] = true;
        Debug.Log("Purchase Success");
    }

    void OnPurchaseError(PlayFabError error)
    {
        purchased[charIndex] = false;
        Debug.Log("Error On Purchase" + error);
    }


}