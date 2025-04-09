using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject UIGames;
    [SerializeField] private GameObject UIListGames;
    [SerializeField] private GameObject settingPOP;
    [SerializeField] public GameObject gameOverPOP;
    [SerializeField] public TextMeshProUGUI overScoreText;

    [SerializeField] public Sprite[] numbers;
    [SerializeField] public GameObject number;

    private Coroutine countDownCoroutine;
    [SerializeField] private GameObject[] Games;
    [SerializeField] public GameObject GameCurrent;

    void Start()
    {
        Application.targetFrameRate = 120;
        Camera.main.backgroundColor = new Color(0.2f, 0.2f, 0.2f);
        AudioManager.Instance.PlayMusic("MainMenu");
    }
    public void Back()
    {
        UIGames.SetActive(true);
        UIListGames.SetActive(false);
        number.SetActive(false);
        if (GameCurrent != null)
        {
            GameCurrent.SetActive(false);
        }
        gameOverPOP.SetActive(false);
        Camera.main.backgroundColor = new Color(0.2f, 0.2f, 0.2f);
        AudioManager.Instance.PlayMusic("MainMenu");
    }

    public void Restart()
    {
        if (GameCurrent != null)
        {
            IGameController controller = GameCurrent.GetComponent<IGameController>();
            controller?.Restart();
        }

    }

    public void Setting()
    {
        settingPOP.SetActive(true);
    }

    public void CloseSetting()
    {
        settingPOP.SetActive(false);
    }

    public void CountDown()
    {
        Time.timeScale = 0f;
        number.SetActive(true);
        countDownCoroutine = StartCoroutine(CountDownCoroutine());
    }


    IEnumerator CountDownCoroutine()
    {
        number.GetComponent<SpriteRenderer>().sprite = numbers[2];
        yield return new WaitForSecondsRealtime(1f);
        number.GetComponent<SpriteRenderer>().sprite = numbers[1];
        yield return new WaitForSecondsRealtime(1f);
        number.GetComponent<SpriteRenderer>().sprite = numbers[0];
        yield return new WaitForSecondsRealtime(1f);
        number.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PlayGame_1()
    {
        UIGames.SetActive(false);
        UIListGames.SetActive(true);
        Games[0].SetActive(true);
        AudioManager.Instance.PlayMusic("MainMenu", 0);
    }

    public void PlayGame_2()
    {
        UIGames.SetActive(false);
        UIListGames.SetActive(true);
        Games[1].SetActive(true);
        AudioManager.Instance.PlayMusic("MainMenu", 0);
    }

    public void PlayGame_3()
    {
        UIGames.SetActive(false);
        UIListGames.SetActive(true);
        Games[2].SetActive(true);
        AudioManager.Instance.PlayMusic("MainMenu", 0);
    }

}
