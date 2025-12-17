using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsUI : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text statsText;
    public Button backButton;
    
    private void Start()
    {
        if (backButton != null)
            backButton.onClick.AddListener(OnBackClicked);
    }
    
    public void DisplayStats()
    {
        if (statsText == null) return;
        string stats = "\n\n";
        stats += "<size=40><b>ПОКАЗАТЕЛЬ СКОРОСТИ ПЕЧАТИ</b></size>\n\n";
        
        stats += "<size=36><b><color=#FFD700>Слепая печать (не глядя на клавиатуру):</color></b></size>\n\n";
        stats += "<size=32>";
        stats += " <b>10-18 лет:</b> 40 WPM\n";
        stats += " <b>19-35 лет:</b> 50 WPM\n";
        stats += " <b>36-50 лет:</b> 45 WPM\n";
        stats += " <b>50+ лет:</b> 35 WPM\n";
        stats += "</size>\n";
        
        stats += "<size=36><b><color=#87CEEB>С взглядом на клавиатуру:</color></b></size>\n\n";
        stats += "<size=32>";
        stats += " <b>10-18 лет:</b> 25 WPM\n";
        stats += " <b>19-35 лет:</b> 35 WPM\n";
        stats += " <b>36-50 лет:</b> 30 WPM\n";
        stats += " <b>50+ лет:</b> 20 WPM\n";
        stats += "</size>\n";
        
        stats += "\n<size=28><i></i></size>\n\n";
        stats += "<size=26><i>WPM = Words Per Minute (слов в минуту)</i>\n";
        stats += "<i>Среднее слово = 5 символов</i></size>";
        
        statsText.text = stats;
    }
    
    private void OnBackClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.ReturnToMenu();
    }
}