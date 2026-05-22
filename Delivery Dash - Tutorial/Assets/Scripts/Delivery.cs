using TMPro;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] float delay = 0.2f;

    [SerializeField] TMP_Text packagesText;
    [SerializeField] TMP_Text wellDoneText;
    bool hasPackage;

    [SerializeField] int packagesLeft = 9;

    [SerializeField] Driver driver;

    void Start()
    {
        packagesText.SetText(packagesLeft.ToString() + " packages left");
        packagesText.gameObject.SetActive(true);
        wellDoneText.gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Package") && !hasPackage)
        {
            hasPackage = true;
            GetComponent<ParticleSystem>().Play();
            Destroy(collision.gameObject, delay);
        }

        if (collision.CompareTag("Customer") && hasPackage)
        {
            hasPackage = false;
            GetComponent<ParticleSystem>().Stop();
            Destroy(collision.gameObject);
            packagesText.SetText(--packagesLeft + " packages left");

            if (packagesLeft <= 0)
            {
                wellDoneText.gameObject.SetActive(true); 
                driver.GameStop();
            }
        }
    }
}
