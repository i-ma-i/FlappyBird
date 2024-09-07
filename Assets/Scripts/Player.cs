using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float m_jumpPower = 400;

    float m_maxPositionY;
    readonly float MAX_VELOCITY_Y = 10f;

    FlappyBird m_flappyBird;
    Rigidbody2D m_rigidbody;

    private void Start()
    {
        m_flappyBird = FindObjectOfType<FlappyBird>();

        m_rigidbody = GetComponent<Rigidbody2D>();
        m_rigidbody.gravityScale = 2f;
        m_rigidbody.AddForce(Vector2.up * m_jumpPower);

        m_maxPositionY = Camera.main.ViewportToWorldPoint(Vector2.one).y * 2f;
    }

    private void Update()
    {
        if (m_flappyBird.playing == false)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            m_rigidbody.velocity = Vector2.zero;
            m_rigidbody.AddForce(Vector2.up * m_jumpPower);
        }
    }

    private void FixedUpdate()
    {
        if (m_flappyBird.playing == false)
        {
            return;
        }

        m_rigidbody.velocity = new Vector2(0, Mathf.Clamp(m_rigidbody.velocity.y, -MAX_VELOCITY_Y, MAX_VELOCITY_Y));
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4f, m_maxPositionY), 0);
    }
}
