using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class GameOver_1 : MonoBehaviour
{
    public List<GameObject> fruits = new List<GameObject>();
    [SerializeField] private GameController_1 gameController;
    [SerializeField] private TextMeshProUGUI scoreText;
    Coroutine stayCoroutine;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<InfoFruit>() && other.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
        {
            stayCoroutine = StartCoroutine(Stay(other.gameObject));
        }
        if (fruits.Count >= 3 && !gameController.isOver)
        {
            gameController.gameOverPOP.SetActive(true);
            scoreText.text = "Score: " + gameController.score.ToString();
            fruits.Clear();
            gameController.isOver = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<InfoFruit>())
        {
            if (stayCoroutine != null)
            {
                StopCoroutine(stayCoroutine);
                stayCoroutine = null;
            }
        }
    }

    IEnumerator Stay(GameObject fruit)
    {
        yield return new WaitForSeconds(1f);
        fruits.Add(fruit);
    }
}
