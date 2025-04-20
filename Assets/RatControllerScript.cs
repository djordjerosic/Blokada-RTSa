using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatControllerScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float wiggleInterval = 1f;
    [SerializeField] private float wiggleAmount = 1f;

    private float wiggleTimer = 0f;
    private Vector2 wiggleOffset = Vector2.zero;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private int health = 3;
    [SerializeField] private Color originalColor = Color.white;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private GameObject redarPrefab;

    [SerializeField] private float bounceForce = 500f;
    [SerializeField] private int turnChance = 30;

    void Awake()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();
        if (ScoreManagerScript.Instance.Propaganda < 75)
            moveSpeed = 1.2f;
        if (ScoreManagerScript.Instance.Propaganda < 50)
            moveSpeed = 1.4f;
        if (ScoreManagerScript.Instance.Propaganda < 25)
            moveSpeed = 1.6f;
    }

    void FixedUpdate()
    {
        Vector2 position = rb.position;
        Vector2 directionToCenter = -position.normalized;

        wiggleTimer += Time.fixedDeltaTime;
        if (wiggleTimer >= wiggleInterval)
        {
            Vector2 perpendicular = new Vector2(-directionToCenter.y, directionToCenter.x);
            float randomWiggle = UnityEngine.Random.Range(-wiggleAmount, wiggleAmount);
            wiggleOffset = perpendicular * randomWiggle;

            wiggleTimer = 0f;
        }

        Vector2 moveDirection = (directionToCenter + wiggleOffset).normalized;
        rb.MovePosition(position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RTS"))
        {
            ScoreManagerScript.Instance.Podrska-=3;
            ScoreManagerScript.Instance.Propaganda+=3;
            GameObject.Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Redar"))
        {
            rb.AddForce(collision.relativeVelocity * bounceForce);
            health--;
            FlashRed();
            if (health < 1)
            {
                ScoreManagerScript.Instance.Podrska ++;
                int turnRoll = UnityEngine.Random.Range(0, 100);
                if (turnRoll < turnChance)
                    Instantiate(redarPrefab, this.transform.position, Quaternion.identity);
                GameObject.Destroy(this.gameObject); 
            }
        }
    }

    private void FlashRed()
    {
        StartCoroutine(FlashRoutine());
    }

    private System.Collections.IEnumerator FlashRoutine()
    {
        spriteRenderer.color = flashColor;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = originalColor;
    }
}
