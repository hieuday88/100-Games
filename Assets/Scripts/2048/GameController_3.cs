using System.Collections;
using TMPro;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameController_3 : MonoBehaviour, IGameController
{
    public static GameController_3 Instance { get; private set; }

    [SerializeField] private TileBoard board;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiscoreText;

    private bool hasGameFinished;

    public int score { get; private set; } = 0;
    void OnEnable()
    {
        UIManager.Instance.GameCurrent = gameObject;
        Camera.main.backgroundColor = new Color(0.9f, 0.7f, 0.7f);
        if (hasGameFinished)
        {
            UIManager.Instance.gameOverPOP.SetActive(true);
            UIManager.Instance.overScoreText.text = "Score: " + score.ToString();
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        // reset score
        SetScore(0);
        hiscoreText.text = LoadHiscore().ToString();

        // update board state
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    public void GameOver()
    {
        hasGameFinished = true;
        UIManager.Instance.gameOverPOP.SetActive(true);
        AudioManager.Instance.PlaySFX("GameOver");
        UIManager.Instance.overScoreText.text = "Score: " + ((int)score).ToString();
    }

    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();

        SaveHiscore();
    }

    private void SaveHiscore()
    {
        int hiscore = LoadHiscore();

        if (score > hiscore)
        {
            PlayerPrefs.SetInt("hiscore", score);
        }
    }

    private int LoadHiscore()
    {
        return PlayerPrefs.GetInt("hiscore", 0);
    }

    public void Restart()
    {
        NewGame();
        UIManager.Instance.gameOverPOP.SetActive(false);
        hasGameFinished = false;
    }
}
