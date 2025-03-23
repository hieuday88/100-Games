using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameOver_1 : MonoBehaviour
{
    [SerializeField] private Transform poolObject;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<InfoFruit>() && other.gameObject.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            other.transform.SetParent(gameObject.transform);
        }
        if (gameObject.transform.childCount >= 4)
        {
            Debug.Log("Game Over");
        }
    }

}
