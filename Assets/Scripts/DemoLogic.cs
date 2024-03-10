using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoLogic : MonoBehaviour
{
    public TMPro.TextMeshProUGUI titleText;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        StartCoroutine(FindPlayer());
    }

    IEnumerator FindPlayer()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("Space Invaders");
        while (!async.isDone)
        {
            yield return null;
        }
        GameObject playerObj ;
        playerObj= GameObject.Find("Defender");
        Debug.Log(playerObj);
    }
}
