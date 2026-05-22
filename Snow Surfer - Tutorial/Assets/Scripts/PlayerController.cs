using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{ 
    [SerializeField] float torqueAmount = 0.5f;
    [SerializeField] float baseSpeed = 15f;
    [SerializeField] float boostSpeed = 30f;
    [SerializeField] ParticleSystem powerUpParticles;
    [SerializeField] ScoreManager scoreManager;

    InputAction moveAction;
    InputAction jumpAction;
    Rigidbody2D rb2d;
    Vector2 moveVector;
    SurfaceEffector2D surfaceEffector2D;
    
    int activePowerUpCount;
    float previousRotation, totalRotation;
    bool canControlPlayer = true;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindAnyObjectByType<SurfaceEffector2D>();
    }

    void Update()
    {
        if (canControlPlayer)
        {
            RotatePlayer();
            BoostPlayer();
            CalculateFlips();
        }
    }

    void RotatePlayer()
    {
        moveVector = moveAction.ReadValue<Vector2>();
        if (moveVector.x < 0)
        {
            rb2d.AddTorque(torqueAmount);
        }

        else if (moveVector.x > 0)
        {
            rb2d.AddTorque(-torqueAmount);
        }
    }

    void BoostPlayer()
    {
        if (moveVector.y > 0)
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    void CalculateFlips()
    {
        float currentRotation = transform.rotation.eulerAngles.z;
        
        totalRotation += Mathf.DeltaAngle(previousRotation, currentRotation);

        if (totalRotation > 340 || totalRotation < -340)
        {
            totalRotation = 0;   
            scoreManager.AddScore(100);
        }

        previousRotation = currentRotation;
    }

    public void DisableControls()
    {
        canControlPlayer = false;
    }

    public void ActivatePowerUp(PowerUpSO powerUp)
    {
        activePowerUpCount++;
        powerUpParticles.Play();
        if (powerUp.GetPowerUpType() == "speed")
        {
            baseSpeed += powerUp.GetPowerUpAmount();
            boostSpeed += powerUp.GetPowerUpAmount();
        }
        else if (powerUp.GetPowerUpType() == "torque")
        {
            torqueAmount += powerUp.GetPowerUpAmount();
        }
    }

    public void DeactivatePowerUp(PowerUpSO powerUp)
    {
        activePowerUpCount--;
        if (activePowerUpCount == 0)
        {
            powerUpParticles.Stop();
        }
        if (powerUp.GetPowerUpType() == "speed")
        {
            baseSpeed -= powerUp.GetPowerUpAmount();
            boostSpeed -= powerUp.GetPowerUpAmount();
        }
        else if (powerUp.GetPowerUpType() == "torque")
        {
            torqueAmount -= powerUp.GetPowerUpAmount();
        }
    }
}