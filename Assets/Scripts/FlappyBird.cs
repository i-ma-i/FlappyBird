using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlappyBird : MonoBehaviour
{
    [SerializeField]
    ObstacleRoot m_obstacleRoot;

    [SerializeField]
    Text m_scoreText;

    readonly int LEVEL_MAX = 10;
    readonly int LEVEL_UP_COUNT = 5;

    Player m_player;

    int m_obstacleCount;
    float m_deadLine;
    float m_obstacleTimer;
    int m_score;

    public bool playing { get; private set; }
    public int level { get; private set; }

    private void Start()
    {
        m_player = FindObjectOfType<Player>();

        playing = true;
        level = 0;
        m_obstacleCount = 0;
        m_deadLine = Camera.main.ViewportToWorldPoint(Vector2.zero).y;
        m_obstacleTimer = Time.time + 2f;
        m_score = 0;
        m_scoreText.text = m_score.ToString();
    }

    private void Update()
    {
        if(playing == false)
        {
            return;
        }

        if (Time.time > m_obstacleTimer)
        {
            Instantiate(m_obstacleRoot);
            m_obstacleCount++;
            m_obstacleTimer = Time.time + 2f;
        }

        if (m_obstacleCount >= LEVEL_UP_COUNT)
        {
            level = Math.Min(level + 1, LEVEL_MAX);
            m_obstacleCount = 0;
        }
    }

    public void AddScore()
    {
        m_score++;
        m_scoreText.text = m_score.ToString();
    }

    public void GameOver()
    {
        Destroy(m_player.GetComponent<Rigidbody2D>());
        playing = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
