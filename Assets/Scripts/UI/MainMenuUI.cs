using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Dropdown difficultyDropdown;
    public TMP_Text bestScoreText;
    public Button startButton;
    public Button statsButton;
    
    private void Start()
    {
        if (startButton != null)
            startButton.onClick.AddListener(OnStartClicked);
        
        if (statsButton != null)
            statsButton.onClick.AddListener(OnStatsClicked);
        
        if (difficultyDropdown != null)
            difficultyDropdown.onValueChanged.AddListener(OnDifficultyChanged);
        
        UpdateBestScore();
    }
    
    public void UpdateBestScore()
    {
        if (bestScoreText == null) return;
        
        if (DataManager.Instance != null && DataManager.Instance.bestSessionResult != null)
        {
            float wpm = DataManager.Instance.bestSessionResult.wpm;
            bestScoreText.text = $"Лучший результат: {wpm:F1} WPM";
        }
        else
        {
            bestScoreText.text = "Лучший результат: ---";
        }
    }
    
    private void OnStartClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.StartTest();
    }
    
    private void OnStatsClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.ShowStats();
    }
    
    private void OnDifficultyChanged(int value)
    {
        if (GameManager.Instance != null)
            GameManager.Instance.SetDifficulty(value);
    }
}