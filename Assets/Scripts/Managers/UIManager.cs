using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject statsPanel;
    public GameObject testPanel;
    public GameObject resultsPanel;
    
    [Header("UI Components")]
    public MainMenuUI mainMenuUI;
    public StatsUI statsUI;
    public TestUI testUI;
    public ResultsUI resultsUI;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void UpdateUI(GameManager.GameState state)
    {
        mainMenuPanel.SetActive(false);
        statsPanel.SetActive(false);
        testPanel.SetActive(false);
        resultsPanel.SetActive(false);
        
        switch (state)
        {
            case GameManager.GameState.MainMenu:
                mainMenuPanel.SetActive(true);
                mainMenuUI.UpdateBestScore();
                break;
            case GameManager.GameState.Stats:
                statsPanel.SetActive(true);
                statsUI.DisplayStats();
                break;
            case GameManager.GameState.Test:
                testPanel.SetActive(true);
                testUI.StartTest();
                break;
            case GameManager.GameState.Results:
                resultsPanel.SetActive(true);
                resultsUI.DisplayResults();
                break;
        }
    }
}