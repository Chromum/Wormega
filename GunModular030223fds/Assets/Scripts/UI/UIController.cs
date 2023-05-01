using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Animator Animator;

    public GameObject StartPageOne;
    public GameObject StartPageTwo;

    public CameraWobble wb;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoBackToStart()
    {
        Animator.SetBool("StartMenu", true);
        Animator.SetBool("SecondStartMenu",false);
        StartPageOne.SetActive(true);
        wb.Sway = false;
    }

    public void DisableStartUI()
    {
        StartPageOne.SetActive(false);
        wb.Sway = false;
    }
    public void DisableSecondStartUI()
    {
        StartPageTwo.SetActive(false);
        wb.Sway = false;
    }
    
    public void StartGameStageOne()
    {
        Animator.SetBool("SecondStartMenu",true);
        Animator.SetBool("StartMenu",false);
        wb.Sway = true;
    }

    public void EnablePageTwo()
    {
        StartPageTwo.SetActive(true);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }


    public void Quit()
    {
        Application.Quit();
    }
}
