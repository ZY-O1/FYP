using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class Loading : MonoBehaviourPunCallbacks
{
    public GameObject loadingScreen;
    public Slider slider;
    public GameObject loading;
    private string levelToLoad;
    int loadCount;
    public static int loadScene;

    sceneNo sn;
    Scene curL;
    string sceneL;

    // Start is called before the first frame update

    private void Start()
    {
        ///
        loading.SetActive(false);
        loadCount = 1;
        loadScene = sceneNo.sceneNum;
        curL = SceneManager.GetActiveScene();
        sceneL = curL.name;
    }

    void Update()
    {
        if (loadScene == 0 && loadCount == 1)
        {
            Debug.Log(loadScene);
            levelToLoad = "CharacterSelect";
            loadCount = 0;
            StartCoroutine(Load());
        }

        if (loadScene == 1 && loadCount == 1)
        {
            Debug.Log(loadScene);
            levelToLoad = "Menu";
            loadCount = 0;
            StartCoroutine(Load());
        }

        if (loadScene == 2 && loadCount == 1)
        {
            Debug.Log(loadScene);
            levelToLoad = "ChooseStage";
            loadCount = 0;
            StartCoroutine(Load());
        }


        if (loadScene == 3 && loadCount == 1)
        {
            Debug.Log(loadScene);
            levelToLoad = "Setting";
            loadCount = 0;
            StartCoroutine(Load());
        }

        if (loadScene == 4 && loadCount == 1)
        {
            Debug.Log(loadScene);
            levelToLoad = "FactoryE";
            loadCount = 0;
            StartCoroutine(Load());
        }

        if (loadScene == 5 && loadCount == 1)
        {
            Debug.Log(loadScene);
            levelToLoad = "LabE";
            loadCount = 0;
            StartCoroutine(Load());
        }

        if (loadScene == 6 && loadCount == 1)
        {
            Debug.Log(loadScene);
            levelToLoad = "SnowLandE";
            loadCount = 0;
            StartCoroutine(Load());
        }


        if (loadScene == 7 && loadCount == 1)
        {
            PhotonNetwork.ConnectUsingSettings();
            //PhotonNetwork.Reconnect();
            loadingScreen.SetActive(false);
            loading.SetActive(true);
            //Debug.Log(loadScene);
            //levelToLoad = "JoinRoom";
            loadCount = 0;
            //StartCoroutine(Load());
        }

        //SceneManager.LoadScene(levelToLoad);    
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(5.0f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelToLoad);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            Debug.Log(progress);
            yield return null;
        }
    }
}
