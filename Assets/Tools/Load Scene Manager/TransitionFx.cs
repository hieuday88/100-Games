using UnityEngine;
using UnityEngine.UI;

public class TransitionFx : MonoBehaviour
{
    public static TransitionFx Instance;
    public Image fade;
    public Image loading;

    private void Awake()
    {
        if (Instance)
        {
            return;
        }

        Instance = this;
    }

    public void StartLoadScene()
    {
        loading.fillAmount = 0f;
        fade.transform.localScale = Vector3.one;
    }

    public void EndLoadScene()
    {
        fade.transform.localScale = Vector3.zero;
    }
}