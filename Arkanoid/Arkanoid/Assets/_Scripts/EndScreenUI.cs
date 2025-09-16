using UnityEngine;
using TMPro;

public class EndScreenUI : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        if (finalScoreText != null)
        {
            finalScoreText.text = $"Final Score: {GameManager.Instance.score}";
        }
    }

    public void PlayAgain()
    {
        GameManager.Instance.ResetGameState();
        GameManager.Instance.LoadSceneByName("Level_01");
    }

    public void BackToMenu()
    {
        GameManager.Instance.LoadSceneByName("MainMenu");
    }
}