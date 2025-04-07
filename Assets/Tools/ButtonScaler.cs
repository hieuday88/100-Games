using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector3 scale;

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(scale / 10f + scale, 0.15f).SetUpdate(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(scale, 0.15f).SetUpdate(true);
        AudioManager.Instance.PlaySFX("Button_Click", 0.5f);
    }
}