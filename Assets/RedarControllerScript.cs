using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedarControllerScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float maxEnergy = 40f;
    [SerializeField] private float energy = 40f;
    [SerializeField] private Color originalColor = Color.white;
    [SerializeField] private Rigidbody2D rb;

    private bool isJumping = false;
    private float originalY;

    [SerializeField] private float jumpFrequency = 4f;
    [SerializeField] private float jumpAmplitude = 0.2f;

    [SerializeField] private HealthBar healthBarInstance;
    [SerializeField] private float energyBonus = 15;
    [SerializeField] private float jumpingDuration = 10;

    [SerializeField] private float bounceForce = 400;
    [SerializeField] private float energyLossFromPolice = 5;

    void Awake()
    { 
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();
        maxEnergy = DifficultyLevelScript.Instance.redarMaxEnergy;
        energyLossFromPolice = DifficultyLevelScript.Instance.policeDamage;
        energy = maxEnergy;
        healthBarInstance.Initialize(transform);
    }

    void OnEnable()
    {
        KoNeSkaceScript.OnKoNeSkace += AddEnergyAndJump;
    }


    void OnDisable()
    {
        KoNeSkaceScript.OnKoNeSkace -= AddEnergyAndJump;
    }

    private void AddEnergyAndJump()
    {
        energy+= energyBonus;
        StartCoroutine(StartJumping());
    }

    private System.Collections.IEnumerator StartJumping()
    {
        isJumping = true;
        originalY = transform.position.y;
        yield return new WaitForSeconds(jumpingDuration);

        isJumping = false;
    }

    void FixedUpdate()
    {
        if(isJumping)
        {
            float newY = originalY + Mathf.Sin(Time.time * jumpFrequency) * jumpAmplitude;
            Vector2 newPosition = new Vector2(rb.position.x, newY);
            rb.MovePosition(newPosition);
        }

    }


    public void Update()
    {
        energy -= Time.deltaTime;
        if(energy <= 1)
            GameObject.Destroy(this.gameObject);
        healthBarInstance.SetHealth(energy, maxEnergy);
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Interventna")
        {
            rb.AddForce(collision.relativeVelocity * bounceForce);
            energy -= energyLossFromPolice;
            FlashRed();
            if (energy < 1)
            {
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
