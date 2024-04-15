using UnityEngine;
using UnityEngine.UI; // For the built-in UI system
using TMPro; 

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;

    // Reference to the Text UI element
    
    public TextMeshProUGUI scoreText; // For TextMeshPro

    void Awake()
    {
        // Set up the singleton instance
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        // Update the UI Text element to display the new score
        scoreText.text = "Score: " + score;
    }
}
