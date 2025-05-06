// MainManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private bool m_GameOver = false;

    private int m_BestScore;
    private string m_BestPlayerName;

    void Start()
    {
        m_BestScore = PlayerPrefs.GetInt("BestScore", 0);
        m_BestPlayerName = PlayerPrefs.GetString("BestPlayerName", "Unknown");
        UpdateScoreUI();
        UpdateBestScoreUI();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();
                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0); // Load the Menu scene (as suggested)
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        UpdateScoreUI();

        if (m_Points > m_BestScore)
        {
            m_BestScore = m_Points;
            m_BestPlayerName = GameData.PlayerName; // Updated to GameDataX
            UpdateBestScoreUI();
            PlayerPrefs.SetInt("BestScore", m_BestScore);
            PlayerPrefs.SetString("BestPlayerName", m_BestPlayerName);
            PlayerPrefs.Save();
        }
    }

    void UpdateScoreUI()
    {
        ScoreText.text = $"Score: {m_Points}";
    }

    void UpdateBestScoreUI()
    {
        BestScoreText.text = $"Best Score: {m_BestPlayerName} - {m_BestScore}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}