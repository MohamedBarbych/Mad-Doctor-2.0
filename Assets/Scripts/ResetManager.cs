using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetManager : MonoBehaviour
{
    public static ResetManager Instance { get; private set; }

    private List<IResettable> resettables = new List<IResettable>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterResettable(IResettable resettable)
    {
        if (!resettables.Contains(resettable))
        {
            resettables.Add(resettable);
        }
    }

    public void UnregisterResettable(IResettable resettable)
    {
        if (resettables.Contains(resettable))
        {
            resettables.Remove(resettable);
        }
    }

    public void ReloadScene()
    {
        StartCoroutine(ReloadSceneCoroutine());
    }

    private IEnumerator ReloadSceneCoroutine()
    {
        foreach (var resettable in resettables)
        {
            resettable.Reset();
        }

        yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

        foreach (var resettable in resettables)
        {
            resettable.OnSceneReload();
        }
    }
}

public interface IResettable
{
    void Reset();
    void OnSceneReload();
}
