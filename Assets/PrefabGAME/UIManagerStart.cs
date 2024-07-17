using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class UIManagerStart : MonoBehaviour
{
    public Animator transitionStart;
    public static int once = 0;
    public Animator Menu;
    private string levelToLoad;
    public AudioClip beep;

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;




    public void runLoading()
    {
		StartCoroutine(WaitS());
	}

    //IEnumerator Wait()
    //{
    //    levelToLoad = "Testing";
    //    SceneManager.LoadScene(levelToLoad);
    //    yield return new WaitForSeconds(3.0f);
    //}


    IEnumerator WaitS()
    {
        levelToLoad = "Loading";
        SceneManager.LoadScene(levelToLoad);
        yield return new WaitForSeconds(5.0f);
    }
}

