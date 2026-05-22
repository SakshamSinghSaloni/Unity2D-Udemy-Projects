using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem finishParticles;
    [SerializeField] ScoreManager scoreManager;
    bool isFinished = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Player");
        if (collision.gameObject.layer == layerIndex && !isFinished)
        {
            finishParticles.Play();
            Invoke("ReloadScene", loadDelay);
            scoreManager.AddScore(500);
            isFinished = true;
        }
            
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
