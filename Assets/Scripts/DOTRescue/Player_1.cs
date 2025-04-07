using DG.Tweening;
using UnityEngine;

public class Player_1 : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;

    [SerializeField] private GameController_2 gameController_2;
    [SerializeField] private GameObject _explosionPrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _rotateSpeed *= -1f;
            AudioManager.Instance.PlaySFX("Move_Ball");
        }
    }


    private void FixedUpdate()
    {
        transform.Rotate(0, 0, _rotateSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Obstacle>())
        {
            GameObject effect = Instantiate(_explosionPrefab, transform.GetChild(0).position, Quaternion.identity);
            gameController_2.GameEnded();
            gameObject.SetActive(false);
            DOVirtual.DelayedCall(3f, () =>
            {
                Destroy(effect);
            });
        }
    }
}
