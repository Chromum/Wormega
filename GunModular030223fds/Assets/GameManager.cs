using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Difficulty currentDifficulty;
    public bool isPaused;
    public GameObject PauseScreen;
    public bool MainMenu;
    public GameObject LoadingSceneArt;
    public AudioClip HitMarker;
    public AudioSource AudioSource;
    private void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Time.timeScale = 1f;

        if((!MainMenu))
            TogglePause();
        if (SceneManager.GetActiveScene().name.Contains("MainMenu"))
            MainMenu = true;
    }

    public void Update()
    {
        if(!MainMenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                TogglePause();
        }

    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        Cursor.visible = isPaused;
        PauseScreen.SetActive(isPaused);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void LoadScene(int scene)
    {
        StartCoroutine(LoadSceneEnum(scene));
    }

    IEnumerator LoadSceneEnum(int i)
    {
        AsyncOperation o = SceneManager.LoadSceneAsync(i);
        LoadingSceneArt.SetActive(true);

        while (!o.isDone)
        {
            yield return null;
        }

    }

}
