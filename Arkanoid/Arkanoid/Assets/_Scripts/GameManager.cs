using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int lives = 3;
    public int score = 0;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI levelText;

    private Ball ball;
    private PaddleController paddle;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        HookSceneReferences();
        UpdateUI();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HookSceneReferences();
        UpdateUI();
    }

    private void HookSceneReferences()
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<PaddleController>();

        var texts = FindObjectsOfType<TextMeshProUGUI>();
        foreach (var t in texts)
        {
            if (t.name == "ScoreText") scoreText = t;
            else if (t.name == "LivesText") livesText = t;
            else if (t.name == "LevelText") levelText = t;
        }

        if (ball != null && paddle != null)
        {
            ball.AttachToPaddle();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
        CheckLevelClear();
    }

    public void OnBallLost()
    {
        lives -= 1;
        UpdateUI();

        if (lives > 0)
        {
            if (ball != null && paddle != null)
                ball.AttachToPaddle();
        }
        else
        {
            // Carrega GameOver pelo índice
            int gameOverIndex = SceneManager.sceneCountInBuildSettings - 1; 
            SceneManager.LoadScene(gameOverIndex);
        }
    }

    private void UpdateUI()
    {
        if (scoreText != null) scoreText.text = $"Score: {score}";
        if (livesText != null) livesText.text = $"Lives: {lives}";
        if (levelText != null)
        {
            var s = SceneManager.GetActiveScene().name;
            levelText.text = s;
        }
    }

    private void CheckLevelClear()
    {
        int bricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        if (bricks == 0)
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        int lastIndex = SceneManager.sceneCountInBuildSettings - 1;
        if (index < lastIndex)
        {
            SceneManager.LoadScene(index + 1);
        }
        else
        {
            // Se não houver próxima, carrega Win
            int winIndex = SceneManager.sceneCountInBuildSettings - 2; 
            SceneManager.LoadScene(winIndex);
        }
    }

    public void ResetGameState()
    {
        lives = 3;
        score = 0;
        UpdateUI();
    }
}
