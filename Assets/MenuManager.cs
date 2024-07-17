using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MenuManager : MonoBehaviourPunCallbacks
{

    public AudioClip beep;
    Scene cur;
    public string scene;
    public static bool PvE;
    public static bool tuto;
    public static bool exitTuto;

    [SerializeField]
    public int changeScene;
    public bool canvasOff;

    [SerializeField] private static string levelToLoad;
   // result r;
    //TotalWin tw;

    void Start()
    {
        canvasOff = false;
        cur = SceneManager.GetActiveScene();
        scene = cur.name;
        if (scene == "ChooseStage")
        {
            PvE = true;
            Debug.Log("PVE MODE");
            PhotonNetwork.OfflineMode = true;
        }
        else if(scene == "Menu")
        {
            PvE = false;
            PhotonNetwork.OfflineMode = false;
        }
    }

    void Update()
    {
        if (exitTuto)
        {
            tuto = false;
            quitToMenu();
        }
    }

    void playBtnSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = beep;
        audio.Play();
    }

    public void pvpBtn()
    {
        sceneNo.sceneNum = 0;
        //Debug.Log(changeScene);
        playBtnSound();
        StartCoroutine(Wait());
    }

    public void pveBtn()
    {
        PvE = true;
        sceneNo.sceneNum = 2;
        //Debug.Log(changeScene);
        playBtnSound();
        //Debug.Log(changeScene);
        StartCoroutine(Wait());
    }

    public void setBtn()
    {
        sceneNo.sceneNum = 3;
        //Debug.Log(changeScene);
        playBtnSound();
        StartCoroutine(Wait());
    }

    public void charBackBtnClicked()
    {
        sceneNo.sceneNum = 1;
        //Debug.Log(changeScene);
        playBtnSound();
        StartCoroutine(Wait());
    }

    public void backToStage()
    {
        Cursor.lockState = CursorLockMode.None;
        sceneNo.sceneNum = 2;
        canvasOff = true;
        playBtnSound();
        StartCoroutine(Wait());
    }

    public void quitToMenu()
    {
        exitTuto = false;
        tuto = false;
        Cursor.lockState = CursorLockMode.None;
        sceneNo.sceneNum = 1;
        canvasOff = true;
        playBtnSound();
        StartCoroutine(dcRoom());
    }

    public void quitToMenuL()
    {
        Cursor.lockState = CursorLockMode.None;
        sceneNo.sceneNum = 1;
        canvasOff = true;
        playBtnSound();
        StartCoroutine(dcLobby());
    }

    public void SetBackBtnClicked()
    {
        Cursor.lockState = CursorLockMode.None;
        sceneNo.sceneNum = 1;
        canvasOff = true;
        Debug.Log(changeScene);
        playBtnSound();
        StartCoroutine(Wait());
    }

    public void quitBtn()
    {
        levelToLoad = "";
        playBtnSound();
        StartCoroutine(delay());
        Application.Quit();
        Debug.Log("The Quit Success!");
    }

    public void Play()
    {
        if (MapChooseToggle.map == 1)
        {
            levelToLoad = "FactoryE";
        }
        else if (MapChooseToggle.map == 2)
        {
            levelToLoad = "LabE";
        }
        else if (MapChooseToggle.map == 3)
        {
            levelToLoad = "SnowyLandE";
        }
        SceneManager.LoadScene(levelToLoad);
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator dcRoom()
    {
        PhotonNetwork.LeaveRoom();
        while(PhotonNetwork.InRoom)
            yield return null;
        levelToLoad = "Loading";
        SceneManager.LoadScene(levelToLoad);
    }

    IEnumerator dcLobby()
    {
        PhotonNetwork.LeaveLobby();
        while (PhotonNetwork.InLobby)
            yield return null;
        sceneNo.sceneNum = 1;
        levelToLoad = "Loading";
        SceneManager.LoadScene(levelToLoad);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
        levelToLoad = "Loading";
        SceneManager.LoadScene(levelToLoad);
    }
}
