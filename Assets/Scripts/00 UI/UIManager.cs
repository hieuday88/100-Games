using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject UIGames;
    [SerializeField] private GameObject UIListGames;
    [SerializeField] private GameObject settingPOP;
    [SerializeField] public GameObject gameOverPOP;
    [SerializeField] private RectTransform gameOverRec;
    [SerializeField] public TextMeshProUGUI overScoreText;

    [SerializeField] public Text number;

    [SerializeField] private Coroutine countDownCoroutine;
    [SerializeField] private Transform loading;
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
        Camera.main.orthographicSize = 4.5f;
        Loading();
        DOVirtual.DelayedCall(0.5f, () =>
      {
          UIGames.SetActive(true);
          UIListGames.SetActive(false);
          number.gameObject.SetActive(false);
          if (GameCurrent != null)
          {
              GameCurrent.SetActive(false);
          }
          gameOverPOP.SetActive(false);
          Camera.main.backgroundColor = new Color(0.2f, 0.2f, 0.2f);
          AudioManager.Instance.PlayMusic("MainMenu");
      });
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
        settingPOP.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        settingPOP.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
        Image img = settingPOP.GetComponentInChildren<Image>();
        img.DOFade(0.2f, 0.7f).SetEase(Ease.OutBack);
        settingPOP.SetActive(true);
    }

    public void CloseSetting()
    {
        Image img = settingPOP.GetComponentInChildren<Image>();
        img.DOFade(0f, 0.7f).SetEase(Ease.OutBack);
        settingPOP.transform.DOScale(0f, 0.3f).SetEase(Ease.Linear);
        DOVirtual.DelayedCall(0.5f, () =>
        {
            settingPOP.SetActive(false);
            settingPOP.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        });

    }

    public void CountDown()
    {
        Time.timeScale = 0f;
        number.gameObject.SetActive(true);
        countDownCoroutine = StartCoroutine(CountDownCoroutine());
    }

    void Loading()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(loading.DOScale(20f, 0.5f).SetEase(Ease.Linear)).SetUpdate(true);
        seq.Append(loading.DOScale(0f, 0.5f).SetEase(Ease.Linear)).SetUpdate(true);
    }

    IEnumerator CountDownCoroutine()
    {
        number.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        number.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        number.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        number.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameOverOutro()
    {
        gameOverRec.DOAnchorPosY(-1000, 0.5f).SetUpdate(true).SetEase(Ease.InBack);
        DOVirtual.DelayedCall(0.7f, () =>
        {
            gameOverPOP.SetActive(false);
        });
    }

    public void GameOverIntro()
    {
        gameOverPOP.SetActive(true);
        gameOverRec.DOAnchorPosY(0, 0.5f).SetUpdate(true).SetEase(Ease.OutBack);
    }

    public void PlayGame_1()
    {
        Loading();
        DOVirtual.DelayedCall(0.5f, () =>
       {
           UIGames.SetActive(false);
           UIListGames.SetActive(true);
           Games[0].SetActive(true);
           AudioManager.Instance.PlayMusic("MainMenu", 0);
       });
    }

    public void PlayGame_2()
    {
        Loading();
        DOVirtual.DelayedCall(0.5f, () =>
      {
          UIGames.SetActive(false);
          UIListGames.SetActive(true);
          Games[1].SetActive(true);
          AudioManager.Instance.PlayMusic("MainMenu", 0);
      });
    }

    public void PlayGame_3()
    {
        Loading();
        DOVirtual.DelayedCall(0.5f, () =>
       {
           UIGames.SetActive(false);
           UIListGames.SetActive(true);
           Games[2].SetActive(true);
           AudioManager.Instance.PlayMusic("MainMenu", 0);
       });
    }

    public void PlayGame_4()
    {
        Loading();
        DOVirtual.DelayedCall(0.5f, () =>
       {
           UIGames.SetActive(false);
           UIListGames.SetActive(true);
           Games[3].SetActive(true);
           AudioManager.Instance.PlayMusic("MainMenu", 0);
       });
    }
}
