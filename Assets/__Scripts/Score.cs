using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class Score : MonoBehaviour
{
    public static Score instance; // Singleton instance
    public TextMeshProUGUI scoreText; // Reference to UI Text
    private int score = 0; // Current score

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // Assign instance for global access
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}