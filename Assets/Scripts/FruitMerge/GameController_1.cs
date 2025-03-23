using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class GameController_1 : MonoBehaviour
{
    private InfoFruit fruit;
    [SerializeField] private Transform spawnObject;
    [SerializeField] private Transform poolObject;

    [SerializeField] private Transform line;
    private bool canSwipe;
    private bool isDelay;
    private ModelFruit modelFruit;
    private int indexNextFruit;
    void Start()
    {
        modelFruit = GetComponent<ModelFruit>();
        NextFruit();
        SpawnFruit();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            canSwipe = true;
        }
        if (Input.GetMouseButton(0))
        {
            if (canSwipe)
            {
                OnMove();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (!isDelay)
                OnDown();
        }
    }

    private void OnMove()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        if (pos.x < 4.5f && pos.x > -4.5f)
            spawnObject.position = new Vector2(pos.x, spawnObject.position.y);
    }

    private void OnDown()
    {
        isDelay = true;
        fruit.transform.SetParent(poolObject);
        fruit.OnFall();
        DOVirtual.DelayedCall(0.5f, () =>
        {
            NextFruit();
            SpawnFruit();
            isDelay = false;
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
        InfoFruit newFruit = PoolingManager.Spawn(modelFruit.DataFruit[level], pointSpawn, Quaternion.identity);
        newFruit.transform.SetParent(poolObject);
        newFruit.Init(level, MergeFruit, true);
    }
}
