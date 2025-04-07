
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameController_1 : MonoBehaviour, IGameController
{
    private InfoFruit fruit;
    [SerializeField] private Transform spawnObject;
    [SerializeField] private Transform poolObject;
    [SerializeField] GameObject line;

    public int score;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI OverscoreText;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] ParticleSystem effectMerge;
    public GameObject gameOverPOP;
    public bool isOver = false;
    private ModelFruit modelFruit;
    private int indexNextFruit;
    void OnEnable()
    {
        UIManager.Instance.GameCurrent = gameObject;
        scoreText.text = score.ToString();
        bestScoreText.text = PlayerPrefs.GetInt("BestScore_1", 0).ToString();
        if (isOver)
        {
            gameOverPOP.SetActive(true);
            OverscoreText.text = "Score: " + score.ToString();
        }
        Camera.main.backgroundColor = new Color(0.6f, 0.3f, 0.3f);
    }
    void Start()
    {
        modelFruit = GetComponent<ModelFruit>();
        NextFruit();
        SpawnFruit();
    }

    void Update()
    {
        if (isOver) return;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Moved:
                    OnMove(touch);
                    break;
                case TouchPhase.Ended:
                    if (line.activeSelf)
                        OnDown();
                    break;
            }
        }
    }

    private void OnMove(Touch touch)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
        pos.z = 0;
        if (pos.x < 2f && pos.x > -2f)
            spawnObject.position = new Vector2(pos.x, spawnObject.position.y);
    }

    private void OnDown()
    {
        fruit.transform.SetParent(poolObject);
        fruit.OnFall();
        line.SetActive(false);
        DOVirtual.DelayedCall(0.5f, () =>
        {
            NextFruit();
            SpawnFruit();
            line.SetActive(true);
        });

    }
    public void NextFruit()
    {
        indexNextFruit = Random.Range(0, modelFruit.LimitLevel);
    }

    public void SpawnFruit()
    {
        fruit = PoolingManager.Spawn(modelFruit.DataFruit[indexNextFruit], spawnObject.position, Quaternion.identity);
        fruit.Init(indexNextFruit, MergeFruit);
        fruit.transform.SetParent(spawnObject);
    }

    private void MergeFruit(InfoFruit fruit1, InfoFruit fruit2, int level)
    {
        if (fruit1.IsCollider && fruit2.IsCollider) return;
        fruit1.gameObject.SetActive(false);
        fruit2.gameObject.SetActive(false);
        Vector2 pointSpawn = (fruit1.transform.position + fruit2.transform.position) / 2;
        ParticleSystem effect = PoolingManager.Spawn(effectMerge, pointSpawn, Quaternion.identity);
        effect.transform.localScale = new Vector3(level, level, level) / 2.5f;
        effect.Play();
        effect.transform.SetParent(gameObject.transform);
        InfoFruit newFruit = PoolingManager.Spawn(modelFruit.DataFruit[level], pointSpawn, Quaternion.identity);
        AudioManager.Instance.PlaySFX("LevelUp_Fruit");
        newFruit.transform.SetParent(poolObject);
        newFruit.Init(level, MergeFruit, true);
        score += level;
        scoreText.text = score.ToString();
        if (score > PlayerPrefs.GetInt("BestScore_1", 0))
        {
            PlayerPrefs.SetInt("BestScore_1", score);
            bestScoreText.text = score.ToString();
        }
        DOVirtual.DelayedCall(1.2f, () =>
        {
            PoolingManager.Despawn(effect.gameObject);
        });
    }
    public void Restart()
    {
        score = 0;
        scoreText.text = score.ToString();
        for (int i = 0; i < poolObject.childCount; i++)
        {
            poolObject.GetChild(i).gameObject.SetActive(false);
        }
        isOver = false;
        gameOverPOP.SetActive(false);
    }

}
