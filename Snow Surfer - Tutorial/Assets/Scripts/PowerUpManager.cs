using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] PowerUpSO powerUp;
    SpriteRenderer spriteRenderer;
    PlayerController player;


    float timer;

    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = powerUp.GetPowerUpDuration();
    }

    void Update()
    {
        CountDownTimer();
    }

    void CountDownTimer()
    {
        if (!spriteRenderer.enabled && timer > 0)
        {
            timer -= Time.deltaTime;
            
            if (timer <= 0)
            {
                player.DeactivatePowerUp(powerUp);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Player");
        if (collision.gameObject.layer == layerIndex && spriteRenderer.enabled)
        {
            spriteRenderer.enabled = false;
            player.ActivatePowerUp(powerUp);
        }
    }
}
