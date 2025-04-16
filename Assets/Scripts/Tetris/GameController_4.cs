using UnityEngine;
using TMPro;


public class GameController_4 : Singleton<GameController_4>, IGameController
{
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI hiscoreText;
    public bool isGameOver = false;

    public int score = 0;
    private void OnEnable()
    {
        UIManager.Instance.GameCurrent = this.gameObject;
        Camera.main.orthographicSize = 14.5f;
        Camera.main.backgroundColor = new Color(0.18f, 0.2f, 0.23f);
    }

    public void Restart()
    {
        Debug.Log("Restart Game");
        isGameOver = false;
    }


}




