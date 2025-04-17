using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;


public class GameController_4 : Singleton<GameController_4>, IGameController
{
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI hiscoreText;
    public bool isGameOver = false;

    [SerializeField] private Tilemap tilemap;

    public int score = 0;
    private void OnEnable()
    {
        UIManager.Instance.GameCurrent = this.gameObject;
        Camera.main.backgroundColor = new Color(0.18f, 0.2f, 0.23f);
        if (isGameOver)
        {
            UIManager.Instance.gameOverPOP.SetActive(true);
            UIManager.Instance.overScoreText.text = "Score: " + score.ToString();
        }
    }

    public void Restart()
    {
        tilemap.ClearAllTiles();
        isGameOver = false;
        UIManager.Instance.GameOverOutro();
    }


}




