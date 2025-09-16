using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public float speed = 8f;
    private Rigidbody2D rb;
    private bool isLaunched = false;
    private Transform paddle;
    private Vector3 offsetFromPaddle = new Vector3(0, 0.6f, 0);

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.freezeRotation = true;

        var paddleObj = GameObject.FindObjectOfType<PaddleController>();
        if (paddleObj != null)
        {
            paddle = paddleObj.transform;
            AttachToPaddle();
        }
    }

    void Update()
    {
        if (!isLaunched && paddle != null)
        {
            transform.position = paddle.position + offsetFromPaddle;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Launch();
            }
        }

        if (isLaunched)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    private void Launch()
    {
        isLaunched = true;
        rb.velocity = new Vector2(0.5f, 1f).normalized * speed;
    }

    public void AttachToPaddle()
    {
        isLaunched = false;
        rb.velocity = Vector2.zero;
        if (paddle != null)
        {
            transform.position = paddle.position + offsetFromPaddle;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.GetComponent<PaddleController>() != null)
        {
            Vector3 paddlePos = coll.transform.position;
            float hitX = (transform.position.x - paddlePos.x);
            Vector2 dir = new Vector2(hitX, 1f).normalized;
            rb.velocity = dir * speed;
        }

        if (coll.gameObject.CompareTag("Brick"))
        {
            Destroy(coll.gameObject);
            GameManager.Instance.AddScore(100);
        }

        if (Mathf.Abs(rb.velocity.y) < 0.2f)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * 0.2f).normalized * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KillZone"))
        {
            GameManager.Instance.OnBallLost();
        }
    }
}