using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigControllerScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private int health = 8;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private Color originalColor = Color.white;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float bounceForce = 400f;
    [SerializeField] private float escapeForce = 5000f;

    void Awake()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();
        if (ScoreManagerScript.Instance.Propaganda < 50)
            moveSpeed = 1.4f;
    }

    void FixedUpdate()
    {
        if (health > 0)
        {
            Vector2 position = rb.position;
            Vector2 directionToCenter = -position.normalized;

            Vector2 moveDirection = (directionToCenter).normalized;
            rb.MovePosition(position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RTS"))
        {
            GameObject.Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Redar"))
        {
            rb.AddForce(collision.relativeVelocity * bounceForce);
            health--;
            FlashRed();
            if (health < 1)
                TurnAround();
        }
    }

    private void TurnAround()
    {
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        bc.enabled = false;
        rb.AddForce(rb.position.normalized * escapeForce);
        spriteRenderer.color = flashColor;
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
