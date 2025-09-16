using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.ResetGameState();
        GameManager.Instance.LoadSceneByName("Level_01");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
