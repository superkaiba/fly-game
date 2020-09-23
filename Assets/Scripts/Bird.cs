using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Scripting.APIUpdating;

public class Bird : MonoBehaviour
{
    // Sounds
    [Header("Sound Settings")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip[] wingFlaps;
    [SerializeField] float volume = 1f;
    
    // Location and Speed
    [Header("Location and Speed Settings")]
    [SerializeField] float speed = 2f;
    [SerializeField] float rotationSpeed = 360f;
    [SerializeField] Vector3 spawnLocation;

    // Cached Variables
    Vector3 cameraPos;
    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;
    GameController gameController;
    GameObject leftEdgeCollider;
    HealthText healthText;
    ParticleSystem pickupParticles;

    // State
    [SerializeField] bool enteringScreen = true;
    [SerializeField] Vector3 currentPosition;
    [SerializeField] int startingHealth = 3;
    bool vulnerable = true;

    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = FindObjectOfType<GameController>();
        cameraPos = Camera.main.transform.position;
        transform.position = spawnLocation - new Vector3(2, 0, 0);
        leftEdgeCollider = GameObject.FindGameObjectWithTag("LeftEdgeCollider");
        healthText = FindObjectOfType<HealthText>();
        pickupParticles = gameObject.transform.Find("Pickup Particles").GetComponent<ParticleSystem>();

        leftEdgeCollider.SetActive(false);
        rigidbody.velocity = new Vector3 (speed/5, 0, 0);

        GetComponent<FadeObject>().TriggerFadeIn();
        currentHealth = startingHealth;
        healthText.UpdateHealthDisplay(currentHealth);
    }

    
    public void PlayWingFlapping()
    {
        AudioSource.PlayClipAtPoint(wingFlaps[Random.Range(0, wingFlaps.Length)], cameraPos, volume);
    }
    public Vector3 GetSpawnLocation()
    {
        return spawnLocation;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= spawnLocation.x)
        {
            enteringScreen = false;
            leftEdgeCollider.SetActive(true);

        }

        if (!enteringScreen)
        {
            Move();
        }
        
    }

    public void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Pickup pickup = otherCollider.GetComponent<Pickup>();
        if (otherCollider.GetComponent<DamageDealer>())
        {
            if (vulnerable)
            {
                Damage();
            }
        }
        else if (pickup)
        {
            pickupParticles.Play();
            if (otherCollider.GetComponent<HealthGiver>())
            {
                AddHealth(1);
            }
            else if (otherCollider.GetComponent<PinkRosePickup>())
            {
                gameController.StartFastScrollPowerUp();
            }
            pickup.TriggerPickup();
            
        }
    }

    public int GetMaxHealth()
    {
        return startingHealth;
    }
    public void AddHealth(int amount)
    {
        if (currentHealth < startingHealth)
        {
            currentHealth += amount;
        }
        healthText.UpdateHealthDisplay(currentHealth);
    }
    private void Die()
    {
        GetComponent<FadeObject>().TriggerFadeOut();
        gameController.EndGame();
    }
    private void Damage()
    {
        AudioSource.PlayClipAtPoint(deathSound, cameraPos);
        currentHealth -= 1;
        healthText.UpdateHealthDisplay(currentHealth);
        if (currentHealth == 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(MakeInvulnerable(1.5f));
            GetComponent<FadeObject>().TriggerFadeIn();
        }
    }
    public void SetVulnerable()
    {
        vulnerable = true;
    }

    public void SetInvulnerable()
    {
        vulnerable = false;
    }
    IEnumerator MakeInvulnerable(float time)
    {
        vulnerable = false;
        yield return new WaitForSeconds(time);
        vulnerable = true;
    }
    
    private void Move()
    {
        var horizontalAxis = Input.GetAxis("Horizontal");
        var verticalAxis = Input.GetAxis("Vertical");

        rigidbody.velocity = new Vector2 (horizontalAxis, verticalAxis) *speed;

    }
    
}
