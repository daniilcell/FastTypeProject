using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public enum GameState { MainMenu, Stats, Test, Results }
    public GameState currentState;
    
    public enum Difficulty { Easy, Medium, Hard }
    public Difficulty currentDifficulty = Difficulty.Easy;
    
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
    
    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }
    
    public void ChangeState(GameState newState)
    {
        currentState = newState;
        UIManager.Instance.UpdateUI(newState);
    }
    
    public void SetDifficulty(int difficultyIndex)
    {
        currentDifficulty = (Difficulty)difficultyIndex;
    }
    
    public void StartTest()
    {
        ChangeState(GameState.Test);
    }
    
    public void ShowStats()
    {
        ChangeState(GameState.Stats);
    }
    
    public void ShowResults()
    {
        ChangeState(GameState.Results);
    }
    
    public void ReturnToMenu()
    {
        ChangeState(GameState.MainMenu);
    }
    
    public void RestartTest()
    {
        ChangeState(GameState.Test);
    }
}