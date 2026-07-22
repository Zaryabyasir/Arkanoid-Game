using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int playerLives = 3;
    [SerializeField] private BallController ball;
    [SerializeField] private PaddleController paddle;
    [SerializeField] private GameObject[] heartIcons;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text victoryScoreText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject victoryUI;
    [SerializeField] private AudioClip ballFallingSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip victorySound;

    private bool _powerUpActive = false;
    private int _totalBricks;
    private int _currentScore;


  

    public void LoseLife()
    {
        playerLives--;
        UpdateHeartUI(playerLives);
        
        if (playerLives <= 0)
        {
            AudioSource.PlayClipAtPoint(gameOverSound, transform.position);
            gameOverUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            AudioSource.PlayClipAtPoint(ballFallingSound, transform.position);
            
            ball = FindFirstObjectByType<BallController>(); 
            ball.BallReset();
        }
        
        finalScoreText.text = "Score: " + _currentScore.ToString("000");
        UpdateScoreUI();
        
        
    }
    
    

    public void AddScore(int amount)
    {
        _currentScore += amount;
        UpdateScoreUI();
    }

    private void UpdateHeartUI(int playerLives)
    {
        for (int i = 0; i < heartIcons.Length; i++)
            if (i < playerLives)
            {
                heartIcons[i].SetActive(true);
            }
            else
            {
                heartIcons[i].SetActive(false);
            }
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + _currentScore.ToString("000");
    }

    public void RegisteredBricks()
    {
        _totalBricks++;
    }

    public void RemoveBricks()
    {
        _totalBricks--;

        if (_totalBricks <= 0)
        {
            AudioSource.PlayClipAtPoint(victorySound,transform.position);
            victoryUI.SetActive(true);
            victoryScoreText.text = "Score: " + _currentScore.ToString("000");
            Time.timeScale = 0;
        }
    }
    

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public bool IsPowerUpActive()
    {
        return _powerUpActive;
    }

    public void SetPowerUpActive(bool active)
    {
        _powerUpActive = active;
    }
    public void ActivatePowerUp(PowerUp.powerUpType type)
    {
        switch(type)
        {
            case PowerUp.powerUpType.Expand:
                paddle.StartExpandTimer();
                break;
            
            case PowerUp.powerUpType.MultiBall:
                ball.TriggerMultiBall();
                break;
            
            case PowerUp.powerUpType.Slow:
                BallController[] activeBalls = FindObjectsByType<BallController>(FindObjectsSortMode.None);
                foreach(BallController b in activeBalls)
                {
                    b.StartSlowTimer();
                }
                break;
                
        }
    }
}

