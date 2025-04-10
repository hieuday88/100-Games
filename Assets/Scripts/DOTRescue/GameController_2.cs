using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController_2 : MonoBehaviour, IGameController
{
    private bool hasGameFinished;
    [SerializeField] private Transform player;
    [SerializeField] private Transform obstacle;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text bestScoreText;

    private float score;
    private float scoreSpeed;
    private int currentLevel;

    [SerializeField] private List<int> _levelSpeed, _levelMax;

    void OnEnable()
    {
        UIManager.Instance.GameCurrent = gameObject;
        Camera.main.backgroundColor = new Color(0.98f, 0.72f, 0.94f);
        scoreText.text = ((int)score).ToString();
        bestScoreText.text = PlayerPrefs.GetInt("BestScore_2", 0).ToString();
        if (hasGameFinished)
        {
            UIManager.Instance.gameOverPOP.SetActive(true);
            UIManager.Instance.overScoreText.text = "Score: " + ((int)score).ToString();
        }
        scoreSpeed = _levelSpeed[currentLevel];
        if (!hasGameFinished)
            UIManager.Instance.CountDown();
    }

    private void Update()
    {
        if (hasGameFinished) return;

        score += scoreSpeed * Time.deltaTime;

        scoreText.text = ((int)score).ToString();

        if (score > _levelMax[Mathf.Clamp(currentLevel, 0, _levelMax.Count - 1)])
        {
            currentLevel = Mathf.Clamp(currentLevel + 1, 0, _levelMax.Count - 1);
            scoreSpeed = _levelSpeed[currentLevel];
        }

        if (score > PlayerPrefs.GetInt("BestScore_2", 0))
        {
            PlayerPrefs.SetInt("BestScore_2", (int)score);
            bestScoreText.text = ((int)score).ToString();
        }
    }

    public void GameEnded()
    {
        hasGameFinished = true;
        StartCoroutine(GameOver());
    }


    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        UIManager.Instance.gameOverPOP.SetActive(true);
        AudioManager.Instance.PlaySFX("GameOver");
        UIManager.Instance.overScoreText.text = "Score: " + ((int)score).ToString();
    }
    public void Restart()
    {
        UIManager.Instance.gameOverPOP.SetActive(false);
        hasGameFinished = false;
        score = 0;
        currentLevel = 0;
        player.gameObject.SetActive(true);
        player.rotation = new Quaternion(0, 0, 0, 0);
        obstacle.rotation = new Quaternion(0, 0, 0, 0);
        UIManager.Instance.CountDown();
    }



}
