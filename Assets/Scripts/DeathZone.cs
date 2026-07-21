using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private GameManager gameManager; 
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball")) 
        {
            BallController[] activeBalls = FindObjectsByType<BallController>(FindObjectsSortMode.None);
            if (activeBalls.Length > 1)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                gameManager.LoseLife();
            }
        }
    }
}
