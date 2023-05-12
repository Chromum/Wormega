using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ItemHolder ItemHolder;
    public MarkVoiceController markVoice;
    public Difficulty currentDifficulty;
    public bool isPaused;
    public GameObject PauseScreen;
    public bool MainMenu;
    public GameObject LoadingSceneArt;
    public AudioClip HitMarker;
    public AudioSource AudioSource;
    public GameAnnouncer Announcer;
    public float GrappleForce = 100;
    public HealthBar Wave1, Wave2, Wave3;

    private bool OptionMenuBool;
    public GameObject OptionMenuUI;
    
    private bool ControlMenuBool;
    public GameObject ControlMenuUI;

    public AudioMixer auMix;
    public bool sceneLoaded;

    public GameObject player;
    public GameObject winScreen;
    public GameObject creditScreen;

    

    public bool hasDied;
    
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
        DebugManager.instance.enableRuntimeUI = false;
        Debug.Log("HA");
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
        sceneLoaded = false;
        StartCoroutine(LoadSceneEnum(scene));
    }

    IEnumerator LoadSceneEnum(int i)
    {
        AsyncOperation o = SceneManager.LoadSceneAsync(i);
        LoadingSceneArt.SetActive(true);

        while (!o.isDone && sceneLoaded == false)
        {
            yield return null;
        }

    }

    public void PlayerDeath()
    {
        foreach(GameObject g in GameObject.FindObjectsOfType<GameObject>())
        {
            if (g != gameObject)
                g.SetActive(false);
        }
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ToggleControlMenu()
    {
        ControlMenuBool = !ControlMenuBool;
        ControlMenuUI.SetActive(ControlMenuBool);
    }
    public void ToggleOptionMenu()
    {
        OptionMenuBool = !OptionMenuBool;
        OptionMenuUI.SetActive(OptionMenuBool);
    }

    public void GameWin()
    {
        if(!hasDied)
        {
            foreach(GameObject g in GameObject.FindObjectsOfType<GameObject>())
            {
                if (g != gameObject)
                    g.SetActive(false);
                
                
            }

            hasDied = true;
            winScreen.SetActive(true);
            creditScreen.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            foreach (var VARIABLE in winScreen.transform.GetComponentsInChildren<Transform>())
            {
                VARIABLE.gameObject.SetActive(true);
            }
            
            foreach (var VARIABLE in creditScreen.transform.GetComponentsInChildren<Transform>())
            {
                VARIABLE.gameObject.SetActive(true);
            }
            creditScreen.SetActive(false);
            
        }



    }

    
}
