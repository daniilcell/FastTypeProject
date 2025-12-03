using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestResult
{
    public float totalTime;
    public float avgTimePerChar;
    public int totalChars;
    public float wpm;
    public GameManager.Difficulty difficulty;
    
    public TestResult(float time, int chars, GameManager.Difficulty diff)
    {
        totalTime = time;
        totalChars = chars;
        difficulty = diff;
        avgTimePerChar = totalTime / totalChars;
        wpm = (totalChars / 5f) / (totalTime / 60f);
    }
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    
    public TestResult currentResult;
    public TestResult bestSessionResult;
    
    public Dictionary<string, float> averageWPM = new Dictionary<string, float>
    {
        { "10-18 лет (слепая печать)", 40f },
        { "10-18 лет (с клавиатурой)", 25f },
        { "19-35 лет (слепая печать)", 50f },
        { "19-35 лет (с клавиатурой)", 35f },
        { "36-50 лет (слепая печать)", 45f },
        { "36-50 лет (с клавиатурой)", 30f },
        { "50+ лет (слепая печать)", 35f },
        { "50+ лет (с клавиатурой)", 20f }
    };
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SaveResult(TestResult result)
    {
        currentResult = result;
        
        if (bestSessionResult == null || result.wpm > bestSessionResult.wpm)
        {
            bestSessionResult = result;
        }
    }
    
    public string GetAgeGroup(float wpm)
    {
        if (wpm >= 50) return "19-35 лет (профессионал)";
        if (wpm >= 45) return "36-50 лет (отличный результат)";
        if (wpm >= 40) return "10-18 лет (хороший результат)";
        if (wpm >= 35) return "50+ лет (выше среднего)";
        if (wpm >= 25) return "10-18 лет (средний уровень)";
        return "Начинающий";
    }
    
    public float GetPercentageOfAverage(float wpm)
    {
        float avgWPM = 40f;
        return (wpm / avgWPM) * 100f;
    }
}