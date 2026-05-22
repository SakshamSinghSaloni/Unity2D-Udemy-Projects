using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed = 200f;
    [SerializeField] float regularSpeed = 5f;
    [SerializeField] float boostSpeed = 10f;
    [SerializeField] float currentSpeed = 5f; // starts with regular speed
    [SerializeField] TMP_Text boostText;

    int lives = 5;
    [SerializeField] TMP_Text lifeText;

    [SerializeField] TMP_Text gameOverText;

    bool boostState = false;

    public void GameStop()
    {
        currentSpeed = 0f;
        steerSpeed = 0f;
    }

    void Start()
    {
        boostText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        lifeText.SetText("Lives = " + lives);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpeedBoost") && !boostState)
        {
            currentSpeed = boostSpeed;
            boostText.gameObject.SetActive(true);
            Destroy(collision.gameObject);
            boostState = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        currentSpeed = regularSpeed;
        boostText.gameObject.SetActive(false);
        if (boostState) boostState = false;
        lifeText.SetText("Lives = " + --lives);

        if (lives <= 0)
        {
            gameOverText.gameObject.SetActive(true);
            GameStop();
        }
    }

    void Update()
    {
        float steerDirection = 0;
        float moveDirection = 0;

        if (Keyboard.current.aKey.isPressed)
        {
            steerDirection = 1f;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            steerDirection = -1f;
        }

        if (Keyboard.current.wKey.isPressed)
        {
            moveDirection = 1f;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            moveDirection = -1f;
        }


        float steer = steerDirection * steerSpeed * Time.deltaTime;
        float move = moveDirection * currentSpeed * Time.deltaTime;

        transform.Rotate(0, 0, steer);
        transform.Translate(0, move, 0);
    }
}
