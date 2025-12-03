using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultsUI : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text totalTimeText;
    public TMP_Text avgTimePerCharText;
    public TMP_Text speedText;
    public TMP_Text percentageText;
    public TMP_Text ageGroupText;
    public TMP_Text comparisonText;
    public Button restartButton;
    public Button menuButton;
    
    private void Start()
    {
        if (restartButton != null)
            restartButton.onClick.AddListener(OnRestartClicked);
        
        if (menuButton != null)
            menuButton.onClick.AddListener(OnMenuClicked);
    }
    
    public void DisplayResults()
    {
        if (DataManager.Instance == null || DataManager.Instance.currentResult == null)
            return;
        
        TestResult result = DataManager.Instance.currentResult;
        
        // Общее время
        if (totalTimeText != null)
        {
            int minutes = Mathf.FloorToInt(result.totalTime / 60f);
            int seconds = Mathf.FloorToInt(result.totalTime % 60f);
            int milliseconds = Mathf.FloorToInt((result.totalTime * 100f) % 100f);
            totalTimeText.text = $"⏱ Общее время: {minutes:00}:{seconds:00}:{milliseconds:00}";
        }
        
        // Среднее время на символ
        if (avgTimePerCharText != null)
        {
            avgTimePerCharText.text = $"⌨ Среднее время/символ: {result.avgTimePerChar:F3} сек";
        }
        
        // Скорость в WPM
        float wpm = result.wpm;
        if (speedText != null)
        {
            speedText.text = $"🚀 Скорость: <size=60><color=#FFD700>{wpm:F1} WPM</color></size>";
        }
        
        // Процент от средней
        if (percentageText != null)
        {
            float percentage = DataManager.Instance.GetPercentageOfAverage(wpm);
            string percentColor = percentage >= 100 ? "#00FF00" : "#FFA500";
            percentageText.text = $"📈 <color={percentColor}>{percentage:F0}% от средней скорости</color>";
        }
        
        // Возрастная группа
        if (ageGroupText != null)
        {
            string ageGroup = DataManager.Instance.GetAgeGroup(wpm);
            ageGroupText.text = $"👤 Ваш уровень: <color=#00FF00>{ageGroup}</color>";
        }
        
        // Сравнение с лучшим результатом
        if (comparisonText != null && DataManager.Instance.bestSessionResult != null)
        {
            if (result == DataManager.Instance.bestSessionResult)
            {
                comparisonText.text = "<size=48><color=#FFD700>🏆 НОВЫЙ РЕКОРД СЕССИИ! 🏆</color></size>";
            }
            else
            {
                float bestWpm = DataManager.Instance.bestSessionResult.wpm;
                float diff = bestWpm - wpm;
                comparisonText.text = $"🥇 Лучший результат сессии: {bestWpm:F1} WPM\n";
                comparisonText.text += $"<color=#FFA500>(на {diff:F1} WPM быстрее)</color>";
            }
        }
    }
    
    private void OnRestartClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.RestartTest();
    }
    
    private void OnMenuClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.ReturnToMenu();
    }
}