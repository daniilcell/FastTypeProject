using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingTest : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField inputField;
    public TMP_Text currentSentenceText;
    public TMP_Text sentenceCounterText;
    public TMP_Text timerText;
    public Slider progressBar;
    
    [Header("Settings")]
    public int totalSentences = 5;
    
    private List<string> sentences;
    private int currentSentenceIndex = 0;
    private int totalCharsTyped = 0;
    private int totalCharsInTest = 0;
    
    private float startTime;
    private float elapsedTime;
    private bool testActive = false;
    
    public void StartTest()
    {
        sentences = TextData.Instance.GetRandomSentences(
            GameManager.Instance.currentDifficulty, 
            totalSentences
        );
        
        totalCharsInTest = 0;
        foreach (string sentence in sentences)
        {
            totalCharsInTest += sentence.Length;
        }
        
        currentSentenceIndex = 0;
        totalCharsTyped = 0;
        elapsedTime = 0;
        
        if (progressBar != null) progressBar.value = 0;
        if (inputField != null)
        {
            inputField.text = "";
            inputField.onValueChanged.RemoveAllListeners();
            inputField.onValueChanged.AddListener(OnInputChanged);
            inputField.ActivateInputField();
        }
        
        ShowCurrentSentence();
        
        testActive = true;
        startTime = Time.time;
    }
    
    private void Update()
    {
        if (testActive)
        {
            elapsedTime = Time.time - startTime;
            UpdateTimer();
        }
    }
    
    private void ShowCurrentSentence()
    {
        if (currentSentenceIndex < sentences.Count)
        {
            if (currentSentenceText != null)
                currentSentenceText.text = sentences[currentSentenceIndex];
            
            if (sentenceCounterText != null)
                sentenceCounterText.text = $"Предложение {currentSentenceIndex + 1} из {totalSentences}";
        }
    }
    
    private void OnInputChanged(string input)
    {
        if (!testActive || currentSentenceIndex >= sentences.Count) return;
        
        string currentSentence = sentences[currentSentenceIndex];
        
        if (input.Length > currentSentence.Length)
        {
            inputField.text = input.Substring(0, currentSentence.Length);
            return;
        }
        
        UpdateSentenceDisplay(input, currentSentence);
        
        if (input == currentSentence)
        {
            OnSentenceCompleted();
        }
    }
    
    private void UpdateSentenceDisplay(string input, string sentence)
    {
        string displayText = "";
        
        for (int i = 0; i < sentence.Length; i++)
        {
            if (i < input.Length)
            {
                if (input[i] == sentence[i])
                {
                    displayText += $"<u><color=#00FF00>{sentence[i]}</color></u>";
                }
                else
                {
                    displayText += $"<u><color=#FF0000>{sentence[i]}</color></u>";
                }
            }
            else
            {
                displayText += sentence[i];
            }
        }
        
        if (currentSentenceText != null)
            currentSentenceText.text = displayText;
    }
    
    private void OnSentenceCompleted()
    {
        totalCharsTyped += sentences[currentSentenceIndex].Length;
        UpdateProgress();
        
        currentSentenceIndex++;
        
        if (currentSentenceIndex >= sentences.Count)
        {
            EndTest();
        }
        else
        {
            if (inputField != null)
            {
                inputField.text = "";
                inputField.ActivateInputField();
            }
            ShowCurrentSentence();
        }
    }
    
    private void UpdateProgress()
    {
        if (progressBar != null)
        {
            float progress = (float)totalCharsTyped / totalCharsInTest;
            progressBar.value = progress;
        }
    }
    
    private void UpdateTimer()
    {
        if (timerText == null) return;
        
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 100f) % 100f);
        
        timerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }
    
    public void EndTest()
    {
        testActive = false;
        if (inputField != null)
        {
            inputField.onValueChanged.RemoveListener(OnInputChanged);
            inputField.DeactivateInputField();
        }
        
        TestResult result = new TestResult(
            elapsedTime, 
            totalCharsInTest, 
            GameManager.Instance.currentDifficulty
        );
        DataManager.Instance.SaveResult(result);
        
        GameManager.Instance.ShowResults();
    }
}