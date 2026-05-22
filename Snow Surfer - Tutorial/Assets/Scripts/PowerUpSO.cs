using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUpSO")]
public class PowerUpSO : ScriptableObject
{
    [SerializeField] string powerUpType;
    [SerializeField] float powerUpAmount;
    [SerializeField] float duration;

    public string GetPowerUpType()
    {
        return powerUpType;
    }

    public float GetPowerUpAmount()
    {
        return powerUpAmount;
    }

    public float GetPowerUpDuration()
    {
        return duration;
    }
}
