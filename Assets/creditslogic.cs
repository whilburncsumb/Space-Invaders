using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class creditslogic : MonoBehaviour
{
    private void Awake()
    {
        // DontDestroyOnLoad(gameObject);
    }

    public void ReturnToMenu()
    {
        StartCoroutine(FindPlayer());
    }

    IEnumerator FindPlayer()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("TitleScene");
        while (!async.isDone)
        {
            yield return null;
        }
    }
}