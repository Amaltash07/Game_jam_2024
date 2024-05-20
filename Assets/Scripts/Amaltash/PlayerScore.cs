using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore Instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        if(Instance == null)
        {
            Instance=this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);

        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    public bool SpendScore(int amount)
    {
        if (score >= amount)
        {
            score -= amount;
            UpdateScoreText();
            return true;
        }
        return false;
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }
}
