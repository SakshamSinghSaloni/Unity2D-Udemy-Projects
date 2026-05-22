using UnityEngine;

public class CharSelectManager : MonoBehaviour
{
    [SerializeField] GameObject scoreCanvas;
    [SerializeField] GameObject dinoSprite;
    [SerializeField] GameObject froggySprite;
    void Start()
    {
        Time.timeScale = 0;
        scoreCanvas.SetActive(false);
        dinoSprite.SetActive(false);
        froggySprite.SetActive(false);
    }

    void BeginGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        scoreCanvas.SetActive(true);
    }

    public void SelectDino()
    {
        dinoSprite.SetActive(true);
        BeginGame();
    }

    public void SelectFroggy()
    {
        froggySprite.SetActive(true);
        BeginGame();
    } 
}
