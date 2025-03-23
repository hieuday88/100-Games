using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : Singleton<LoadSceneManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        LoadScenePro(sceneName);
    }

    public void ReloadScene()
    {
        var curSceneName = SceneManager.GetActiveScene().name;
        LoadScenePro(curSceneName);
    }
    
    private void LoadScenePro(string sceneName)
    {
        StartCoroutine(IELoadScene(sceneName));
    }

    private IEnumerator IELoadScene(string sceneName)
    {
        TransitionFx.Instance.StartLoadScene();
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        
        while (!asyncLoad!.isDone)
        {
            float progress = asyncLoad.progress;
            TransitionFx.Instance.loading.fillAmount = progress;
            yield return null;
        }

        yield return new WaitForSeconds(0.25f);
        TransitionFx.Instance.loading.fillAmount = 1f;
        yield return new WaitForSeconds(0.25f);
        TransitionFx.Instance.EndLoadScene();
    }
}