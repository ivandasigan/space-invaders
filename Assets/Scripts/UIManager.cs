using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _finalScore;
    [SerializeField]
    private Text _gameoverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Image _barHealth;
    [SerializeField]
    private Text _reminderText;
    [SerializeField]
    private Image _LivesImage;
    [SerializeField] 
    private Sprite[] _livesSprite;
    [SerializeField]
    private Image _bossEnemyLives;
    [SerializeField]
    private Image _stars;
    [SerializeField]
    private Sprite[] _starSprites;
    [SerializeField]
    private Button _submitScore;
    [SerializeField]
    private Text _submitScoreText;
    [SerializeField]
    private GameObject _pausemenuPanel;
    // Submit to Leaderboard panel
    [SerializeField]
    private GameObject _submitScorePanel;
    [SerializeField]
    private Button _closeScoreBoardButton;
    [SerializeField]
    private Button _submitButton;
    [SerializeField]
    private Text _submitScoreboardText;
    [SerializeField]
    private InputField _inputUsername;


    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private Animator _animGameOverText;
    private Animator _animReminderText;
    private int _finalScoreInt = 0;
    private static bool _canRestart = false;
    public static string level;
    // Start is called before the first frame update
    void Start()
    {   


        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.Log("Game Manager is NULL");
        }

        _animGameOverText = _gameoverText.GetComponent<Animator>();
        if (_animGameOverText == null)
        {
            Debug.LogError("game over text animator is null");
        }
        _animReminderText = _reminderText.GetComponent<Animator>();
        if (_animReminderText == null)
        {
            Debug.LogError("reminder text animator is null");
        }
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("spawn manager in ui manager is null");
        }
        _scoreText.gameObject.SetActive(true);
        _scoreText.text = "Score: " + 0;
        _gameoverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _stars.gameObject.SetActive(false);
        _finalScore.gameObject.SetActive(false);
        _barHealth.gameObject.SetActive(false);
        _reminderText.gameObject.SetActive(false);
        _submitScore.gameObject.SetActive(false);
        _submitScoreText.gameObject.SetActive(false);
        _submitScorePanel.SetActive(false);
        Time.timeScale = 1;
        _canRestart = false;
    }

    private void Update()
    {
        if (_canRestart == true) 
        {
            RestartGame();
        }
       
    }

    public void UpdateScore(int updateScore)
    {
        _scoreText.text = "Score: " + updateScore.ToString();
        _finalScoreInt = updateScore;
    }
    public void UpdateBossLives(float lives) 
    {

        _bossEnemyLives.rectTransform.sizeDelta = new Vector2(lives,11.0f);
    }
    public void AddPointsFromBoss()
    {
        _finalScoreInt+=100;
        _scoreText.text = "Score: " + _finalScoreInt.ToString();
    }
    public void UpdateLives(int currentLives)
    {
        _LivesImage.sprite = _livesSprite[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    public void GameOverSequence()
    {
        _restartText.gameObject.SetActive(true);
        StartCoroutine(UpdateActivateGameOver());
       // StartCoroutine(AddAnimationStyleGameOver());
        _gameManager.GameOver();
        _canRestart = true;
    }
    IEnumerator UpdateActivateGameOver()
    {
        // Game over text
        _gameoverText.gameObject.SetActive(true);
        _animGameOverText.SetTrigger("OnNextFadein");
        yield return new WaitForSeconds(5.0f);  
        _gameoverText.gameObject.SetActive(false);  
        // Stars and final score
        SetStarsAndFinalScore(_finalScoreInt);
        // hide score text
        _scoreText.gameObject.SetActive(false);
    }

    IEnumerator AddAnimationStyleGameOver()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            _gameoverText.text = "";
            yield return new WaitForSeconds(1.0f);
            _gameoverText.text = "Game Over";
        }
    }

    public void RestartGame()
    {
        if (Input.GetKeyDown(KeyCode.R))
         {
            // _gameoverText.gameObject.SetActive(false);
      
            _spawnManager.OnStopSpawnBossEnemy();
            _gameoverText.text = "";
            _restartText.text = "";
            _restartText.gameObject.SetActive(false);
             BossEnemy._isBossShieldActive = true;
            Debug.Log("R is pressed");
        }
    }
    public void ShowBossEnemyLifeBar()
    {
        _barHealth.gameObject.SetActive(true);
    }
    public void ShowReminderText()
    {
        _reminderText.gameObject.SetActive(true);
        _animReminderText.SetTrigger("OnNextFadein");

    }
    public void RemoveReminderText()
    {
        _reminderText.gameObject.SetActive(false);
        Color reminderTextColor = _reminderText.color;
        reminderTextColor.a = 0.0f;

    }

   
    private void SetStarsAndFinalScore(int score)
    {

        if (score == 0) 
        {
            _stars.sprite = _starSprites[0];
        } else if (score < 50) 
        {
            _stars.sprite = _starSprites[1];
        } else if (score < 100)
        {
            _stars.sprite = _starSprites[2];
        } else 
        {
            _stars.sprite = _starSprites[3];
        }

        _stars.gameObject.SetActive(true);
        _finalScore.gameObject.SetActive(true);
        _submitScore.gameObject.SetActive(true);
        _submitScoreText.gameObject.SetActive(true);
        _finalScore.text = "Score is " + _finalScoreInt.ToString();
    }

    // Pause Panel
    
 public void OnPauseGame()
    {
        _pausemenuPanel.SetActive(true);

        Time.timeScale = 0;
    }
    public void OnPauseBackToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void OnPauseResume()
    {
        _pausemenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    // Panel Submit Score to scoreboard
        public void SubmitScore()
    {
        _canRestart = false;
        Debug.Log("can be restart " + _canRestart);
        Debug.Log("LEvel == > " + level);
       _submitScoreboardText.text = _finalScoreInt.ToString();
       _submitScorePanel.SetActive(true);
       Time.timeScale = 0;
        _gameManager.NotGameOver();
    }
    public void SubmitScoreToScoreboard()
    {
        SaveOnDatabase();
        _canRestart = true;
        _gameManager.GameOver();
    }
    public void CloseScoreboardPanel()
    {
        _submitScorePanel.SetActive(false);
        Time.timeScale = 1;
         _gameManager.GameOver();
    }
    
    public void OnTextValueChange()
    {

        var username = _inputUsername.text;
        if (username == "")
        {
            _submitButton.interactable = false;
            _submitButton.image.color = Color.gray;
        } else {
            _submitButton.interactable = true;
            _submitButton.image.color = new Color32(45, 162, 233,255);

        }
    }
    private void SaveOnDatabase()
    {
      var newUser = new User(_inputUsername.text, _finalScoreInt);
      DatabaseHandler.PostUser(newUser, level, () =>
      {
        Debug.Log("Succes saving data!");
         _submitScorePanel.SetActive(false);
        Time.timeScale = 1;
      });
    }
}
