using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float patrolRange = 7f;
    public float detectionRange = 5f;
    public float chaseLimit = 7f;

    private Vector2 startPos;
    private bool chasing = false;
    private bool returning = false;
    private int direction = 1;
    private Rigidbody2D rb;

    [Header("Player Detection")]
    public Transform player;

    private void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(direction * patrolSpeed, rb.linearVelocity.y);
    }

    private void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player not assigned in EnemyAI script!");
            return;
        }

        float playerDist = Vector2.Distance(transform.position, player.position);
        Vector2 directionToPlayer = ((Vector2)player.position - (Vector2)transform.position).normalized;

        float dotProduct = Vector2.Dot(transform.right, directionToPlayer);
        bool playerInFront = dotProduct > 0.8f;
        bool playerInBack = dotProduct < -0.8f;

        if (!chasing && !returning)
        {
            Patrol();
            if (playerDist < detectionRange && (playerInFront || playerInBack))
            {
                chasing = true;
            }
        }
        else if (chasing)
        {
            ChasePlayer();
            if (playerDist > chaseLimit)
            {
                chasing = false;
                returning = true;
            }
        }
        else if (returning)
        {
            ReturnToStart();
            if (Vector2.Distance(transform.position, startPos) < 0.1f)
            {
                returning = false;
            }
        }
    }

    private void Patrol()
    {
        rb.linearVelocity = new Vector2(direction * patrolSpeed, rb.linearVelocity.y);

        if (Mathf.Abs(transform.position.x - startPos.x) >= patrolRange)
        {
            ChangeDirection();
        }
    }

    private void ChasePlayer()
    {
        float moveDirection = Mathf.Sign(player.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(moveDirection * chaseSpeed, rb.linearVelocity.y);
        if (moveDirection != direction)
        {
            direction = (int)moveDirection;
            Flip();
        }
    }

    private void ReturnToStart()
    {
        float moveDirection = Mathf.Sign(startPos.x - transform.position.x);
        rb.linearVelocity = new Vector2(moveDirection * patrolSpeed, rb.linearVelocity.y);
        if (moveDirection != direction)
        {
            direction = (int)moveDirection;
            Flip();
        }
    }

    private void ChangeDirection()
    {
        direction *= -1;
        Flip();
        rb.linearVelocity = new Vector2(direction * patrolSpeed, rb.linearVelocity.y);
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Transform cameraTransform = other.transform.Find("Main Camera");
            if (cameraTransform != null)
            {
                cameraTransform.parent = null;
            }

            Destroy(other.gameObject);
            GameManager.Instance.GameOver();
        }
        else
        {
            ChangeDirection();
        }
    }
}
