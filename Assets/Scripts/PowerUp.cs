using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum powerUpType { Expand, MultiBall, Slow }
    [SerializeField] private powerUpType powerupType;
    private GameManager _gameManager;
    
    void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _gameManager.ActivatePowerUp(powerupType);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("DeathZone"))
        {
            Destroy(gameObject);
        }
    }
    
    private void OnDestroy()
    {
        if (_gameManager != null)
        {
            _gameManager.SetPowerUpActive(false);
        }
    }
}