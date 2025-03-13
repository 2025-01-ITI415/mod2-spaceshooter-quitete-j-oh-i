using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance; // Singleton instance
    public TextMeshProUGUI scoreText; // Reference to UI Text
    private int score = 0; // Current score

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps the Score object across scenes
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
        }
    }

    void Start()
    {
        if (scoreText == null)
        {
            scoreText = FindObjectOfType<TextMeshProUGUI>(); // Auto-assign if not set
        }
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}