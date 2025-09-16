using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class AppCycle: MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
            StartCoroutine(ScenesLoop());
        }

        private IEnumerator ScenesLoop()
        {
            for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                yield return SceneManager.LoadSceneAsync(i);
                var game = new GameCycle();
                FindFirstObjectByType<Bootstrap>().Run(game);
                yield return new WaitUntil(() => game.IsCompleted);
            }
        }
    }
}