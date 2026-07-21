using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    [SerializeField] Sprite crackedSprite;
    [SerializeField] int scoreValue = 100;
    [SerializeField] private AudioClip brickBreakSound;
    [SerializeField] private GameObject[] powerupPrefabs;
    [SerializeField] private float dropChance = 0.1f;
    private GameManager _gameManager;
    SpriteRenderer _spriteRenderer;
    
    private int currentHealth;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        _gameManager = FindFirstObjectByType<GameManager>();
        _gameManager.RegisteredBricks();    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            currentHealth--;
            if (currentHealth <= 0)
            {
                _gameManager.AddScore(scoreValue);
                _gameManager.RemoveBricks();
                AudioSource.PlayClipAtPoint(brickBreakSound, transform.localPosition);
                
                if (Random.value <= dropChance && !_gameManager.IsPowerUpActive())
                {
                    _gameManager.SetPowerUpActive(true);
                    int randomIndex = Random.Range(0, powerupPrefabs.Length);
                    Instantiate(powerupPrefabs[randomIndex], transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
            else if (currentHealth == 1) 
            {
                if (crackedSprite != null)
                    _spriteRenderer.sprite = crackedSprite;
            }
        }
    }
}