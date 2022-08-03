using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button startButton;
    [SerializeField] Button tutorialButton;
    [SerializeField] Button settingsButton; // This button is disabled at the start.
    [SerializeField] Button creditsButton;
    [SerializeField] Button exitButton;

    [Header("Pop Ups")]
    [SerializeField] GameObject popUpWindow;
    [SerializeField] GameObject creditsPage;
    [SerializeField] GameObject exitConfirmationPage;

    List<GameObject> popUpPages = new List<GameObject>();

    LevelLoader levelLoader;

    private void OnEnable()
    {
        startButton.onClick.AddListener(StartOpenLevel);
        tutorialButton.onClick.AddListener(StartTutorial);
        creditsButton.onClick.AddListener(ShowCreditsWindow);
        exitButton.onClick.AddListener(ShowExitConfirmationWindow);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveAllListeners();
        tutorialButton.onClick.RemoveAllListeners();
        creditsButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        if(levelLoader == null)
        {
            Debug.LogError("Level Loader is not in the scene.");
        }

        settingsButton.interactable = false;
        HidePopUp();
        popUpPages.Add(creditsPage);
        popUpPages.Add(exitConfirmationPage);
    }

    private void StartOpenLevel()
    {
        Debug.Log("Start Open Level");
        levelLoader.GoToScene("VR Custom");
    }

    private void StartTutorial()
    {
        Debug.Log("Start tutorial");
        levelLoader.GoToScene("02 Bedroom");
    }

    private void ShowCreditsWindow()
    {
        Debug.Log("Credits window");
        ShowSelectedPopUp(creditsPage);
    }

    private void ShowExitConfirmationWindow()
    {
        Debug.Log("Exit confirmation window");
        ShowSelectedPopUp(exitConfirmationPage);
    }

    public void ExitExperience()
    {
        Debug.Log("Exit Application");
        levelLoader.ExitApplication();
    }

    private void ShowSelectedPopUp(GameObject selectedPopUp)
    {
        popUpWindow.SetActive(true);
        foreach(GameObject page in popUpPages)
        {
            if(page == selectedPopUp)
            {
                page.SetActive(true);
                continue;
            }
            page.SetActive(false);
        }
    }

    public void HidePopUp()
    {
        popUpWindow.SetActive(false);
    }

}
