using UnityEngine;

public class ObstacleRoot : MonoBehaviour
{
    FlappyBird m_flappyBird;

    float m_destroyLine;
    float m_playerLine;
    bool m_added;

    private void Start()
    {
        m_flappyBird = FindObjectOfType<FlappyBird>();

        m_destroyLine = Camera.main.ViewportToWorldPoint(Vector3.zero).x - 1f;
        m_playerLine = FindObjectOfType<Player>().transform.position.x;
        m_added = false;

        float obstacleTop = Camera.main.ViewportToWorldPoint(Vector3.one).y - 2.5f;
        float obstacleBottom = Camera.main.ViewportToWorldPoint(Vector3.zero).y + 3.5f;
        float blankSpace = Random.Range(obstacleTop, obstacleBottom);

        float width = 5f - m_flappyBird.level * 0.1f;

        Transform top = transform.GetChild(0);
        Transform bottom = transform.GetChild(1);
        top.position = new Vector3(top.position.x, width + blankSpace, 0);
        bottom.position = new Vector3(bottom.position.x, -width + blankSpace, 0);
    }

    private void Update()
    {
        if(m_flappyBird.playing == false)
        {
            return;
        }

        transform.position += Vector3.left * Time.deltaTime * 2f;

        if (transform.position.x < m_destroyLine)
        {
            Destroy(gameObject);
        }

        if (m_added == false)
        {
            if (transform.position.x < m_playerLine)
            {
                m_flappyBird.AddScore();
                m_added = true;
            }
        }
    }
}
