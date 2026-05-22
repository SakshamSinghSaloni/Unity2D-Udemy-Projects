using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] PlayerController playerController;
    void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");
        if (collision.gameObject.layer == layerIndex)
        {
            crashParticles.Play();
            Invoke("ReloadScene", loadDelay);
            playerController.DisableControls();
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
