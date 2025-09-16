using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 12f;
    private Rigidbody2D rb;
    private Camera cam;
    private float halfWidthWorld;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Start()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.freezeRotation = true;

        var sr = GetComponent<SpriteRenderer>();
        halfWidthWorld = sr.bounds.extents.x;
    }

    void FixedUpdate()
    {
        float input = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(input * speed, 0f);

        Vector3 left = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 right = cam.ViewportToWorldPoint(new Vector3(1, 0, 0));
        float minX = left.x + halfWidthWorld;
        float maxX = right.x - halfWidthWorld;
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, 0f);
    }
}