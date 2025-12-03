using UnityEngine;

public class TestUI : MonoBehaviour
{
    [Header("References")]
    public TypingTest typingTest;
    
    public void StartTest()
    {
        if (typingTest != null)
            typingTest.StartTest();
    }
}