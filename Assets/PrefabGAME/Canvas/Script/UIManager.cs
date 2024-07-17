using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UIManager : MonoBehaviourPunCallbacks
{
    public Animator transition;
    private string levelToLoad;
    public AudioClip beep;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject canvas;
    public GameObject radar;
    public Slider skill;
    public bool skillU;
    public GameObject warning;

    public GameObject hideW;
    public GameObject huntW;
    public bool warnOnce;

    Scene gameplayScene;
    string sceneP;
    MenuManager mm;
    SwapTeam st;

   // public CinemachineFreeLook cinemachineFL;
    public GameObject cam;

    void Start()
    {
        skill.maxValue = 10;
        skill.value = 10;
        Cursor.lockState = CursorLockMode.Locked;
        st = GetComponent<SwapTeam>();
        gameplayScene = SceneManager.GetActiveScene();
        sceneP = gameplayScene.name;
        transition.SetTrigger("TransitionOpen");
        transition.SetTrigger("health");
        transition.SetTrigger("mission");
        warnOnce = true;
        mm = GameObject.Find("MENUMANAGER").GetComponent<MenuManager>();

        if (MenuManager.PvE)
        {

        }
        else
        {
            if (!photonView.IsMine)
            {
                cam.SetActive(false);
                canvas.SetActive(false);
                radar.SetActive(false);
            }

            if (photonView.IsMine)
            {
                hideW.SetActive(false);
                huntW.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (TIMER.currentMatchTimer < 180 && warnOnce && SwapTeam.team == 1)
        {
            StartCoroutine(warn());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (sceneP == "Factory" || sceneP == "SnowyLand" || sceneP == "Lab")
            {
                if (photonView.IsMine)
                {
                    AudioSource audio = GetComponent<AudioSource>();
                    if (GameIsPaused)
                    {
                        audio.clip = beep;
                        audio.Play();
                        resume();
                    }
                    else
                    {
                        Cursor.lockState = CursorLockMode.None;
                        //cinemachineFL.m_XAxis.m_MaxSpeed = 0.0f;
                        //cinemachineFL.m_YAxis.m_MaxSpeed = 0.0f;
                        audio.clip = beep;
                        audio.Play();
                        pause();
                    }
                }
            }
            else
            {
                AudioSource audio = GetComponent<AudioSource>();
                if (GameIsPaused)
                {
                    audio.clip = beep;
                    audio.Play();
                    resume();
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                    //cinemachineFL.m_XAxis.m_MaxSpeed = 0.0f;
                    //cinemachineFL.m_YAxis.m_MaxSpeed = 0.0f;
                    audio.clip = beep;
                    audio.Play();
                    pause();
                }
            }

            if (mm.canvasOff)
            {
                canvas.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && skill.value == 10)
        {
                st.deactivateMesh();
                skill.value = 0;
        }

        if (skill.value < 10)
        {
            skill.value += Time.deltaTime;
        }
    }

    void playBtnSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = beep;
        audio.Play();
    }

    public void endLogoHide()
    {
        Debug.Log("activated");
        hideW.SetActive(true);
    }


    public void endLogoHunt()
    {
        huntW.SetActive(true);
    }

    public void quitGame()
    {

        if (photonView.IsMine)
        {
            StartCoroutine(quitDelay());
        }
    }

    IEnumerator Wait()
    {
        transition.SetTrigger("TransitionClose");
        yield return new WaitForSeconds(5.0f);
        PhotonNetwork.LoadLevel("result");
    }


    IEnumerator warn()
    {
        warning.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        warning.SetActive(false);
        warnOnce = false;
    }

    IEnumerator quitDelay()
    {
        yield return new WaitForSeconds(1.0f);
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
            yield return null;
        PhotonNetwork.LoadLevel("Menu");
    }

    public void resume()
    {
        //cinemachineFL.m_XAxis.m_MaxSpeed = 275.0f;
        //cinemachineFL.m_YAxis.m_MaxSpeed = 3.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Canset");
        pauseMenuUI.SetActive(false);
        //Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void pause()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        //Time.timeScale = 0f;
        GameIsPaused = true;
    }
}

