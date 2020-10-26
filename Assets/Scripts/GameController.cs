using System;
using System.Collections;
using UnityEngine;
using System.Reflection;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class GameController : MonoBehaviour
{
    public int passedAsteroids;
    public Score score;

    [SerializeField]
    private AudioController _audioController;

    [Header("Current results")] 
    [SerializeField] private Text currentScoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text currentSecondsText;

    private string scoreText
    {
        set
        {
            if (!value.Equals(currentScoreText.text))
            {
                currentScoreText.text = value;
            }
        }
    }

    [Header("Game over results")] [SerializeField]
    private GameObject crashPanel;

    [SerializeField] private Text endScoreText;
    [SerializeField] private Text endSecondsText;
    [SerializeField] private Text endAsteroidsText;

    [Header("Popup elements")] [SerializeField]
    private Text gameOverText;

    [SerializeField] private Text pressKeyText;
    [SerializeField] private Text congratulationsText;
    [SerializeField] private Button restartButton;

    [Header("Scripts")] 
    [SerializeField] private SpaceshipMovement SM;
    [SerializeField] private RoadSpawner RS;
    
    private int _highScore;
    private bool _isGameLounched = true;

    private bool _isGameStopped = true;
    private float _passedAsteroids;

    private float _timer;

    private void Start()
    {
        StartCoroutine(Stopwatch());

        crashPanel.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        congratulationsText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        score = new Score();
        score = new Score("123");

        passedAsteroids = 0;

        // check stored data
        _highScore = PlayerPrefs.HasKey("High Score") ? PlayerPrefs.GetInt("High Score") : 0;
    }

    private void Update()
    {
        if (Input.anyKeyDown && _isGameLounched)
        {
            StartGame();
            _isGameLounched = false;
        }

        ChangeDifficulty();
        CalculateHighScore();

        scoreText = score.ToString();
        currentSecondsText.text = "Seconds: " + _timer;
        highScoreText.text = "High score: " + _highScore.AddApostrophe();

        endScoreText.text = $"Score: {(int) score}";
        endSecondsText.text = "Seconds: " + _timer;
        endAsteroidsText.text = "Asteroids passed: " + passedAsteroids;
    }
    
    private void ChangeDifficulty()
    {
        if (_timer < 10) AsteroidSpawner.dificultyCoef = 1;
        if (_timer > 10) AsteroidSpawner.dificultyCoef = 2;
        if (_timer > 20) AsteroidSpawner.dificultyCoef = 4;
        if (_timer > 30) AsteroidSpawner.dificultyCoef = 5;
    }

    private void CalculateHighScore()
    {
        _highScore = (int) score > _highScore ? (int) score : _highScore;
    }

    private void StartGame()
    {
        _isGameStopped = false;
        SM.canMove = true;
        pressKeyText.gameObject.SetActive(false);
        
        _audioController.PlayEngineSound();
    }

    public void RestartGame()
    {
        RS.StartGame();
        SM.canMove = true;
        
        // New string
        _audioController.PlayEngineSound();

        gameOverText.gameObject.SetActive(false);
        congratulationsText.gameObject.SetActive(false);
        crashPanel.SetActive(false);
        restartButton.gameObject.SetActive(false);

        _isGameStopped = false;

        score.ResetValue();
        _timer = 0;
        passedAsteroids = 0;
        AsteroidSpawner.dificultyCoef = 0;
    }

    public void StopGame()
    {
        SM.canMove = false;
        _isGameStopped = true;

        gameOverText.gameObject.SetActive(true);
        crashPanel.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

        if ((int) score >= _highScore) congratulationsText.gameObject.SetActive(true);
        // save high score in computer
        PlayerPrefs.SetInt("High Score", _highScore);
        
        // New string
        _audioController.PlayCrashSound();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator Stopwatch()
    {
        while (true)
        {
            if (!_isGameStopped)
            {
                _timer++;
                score++;

                if (Input.GetKey(KeyCode.Space)) score++;
                yield return new WaitForSeconds(1);
            }

            yield return null;
        }
    }
}